using AppSec.Domain.Commands.SyncSastCommand;
using AppSec.Domain.Interfaces.IRepository;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace AppSec.Infra.Data.Works;

public class StastWork : BackgroundService
{
    private readonly IBus bus;

    private readonly IConfiguration configuration;
    private readonly IServiceScopeFactory serviceScopeFactory;
    private readonly ILogger<StastWork> _logger;

    public StastWork(ILogger<StastWork> _logger,IBus bus, IConfiguration configuration, IServiceScopeFactory serviceScopeFactory)
    {
        this.bus = bus ?? throw new ArgumentNullException(nameof(bus));
        this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        this.serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
        this._logger = _logger ?? throw new ArgumentNullException(nameof(_logger));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (stoppingToken.IsCancellationRequested != true)
        {
            try
            {

                using var scope = serviceScopeFactory.CreateScope();
                var _projectRepository = scope.ServiceProvider.GetRequiredService<IProjectRepository>();
                var mongo = scope.ServiceProvider.GetRequiredService<IMongoDatabase>();
                var collection = mongo.GetCollection<TaskSonar>("SonarTasks");
                var token = configuration.GetSection("sonar:token").Value ?? throw new ArgumentNullException("sonar:token");
                var sonarUrl = new Uri(configuration.GetSection("sonar:url").Value ?? throw new ArgumentNullException("sonar:url"));
                using var httpClient = new HttpClient();
                httpClient.BaseAddress = sonarUrl;
                httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(token)));
                var projects = await _projectRepository.GetAll();
                foreach (var project in projects)
                {
                    try
                    {
                        var response = await httpClient.GetAsync($"/api/ce/activity?component={project.Name}&status=SUCCESS&onlyCurrents=true&type=REPORT&p={1}");

                        if (response.IsSuccessStatusCode)
                        {
                            var _content = await response.Content.ReadAsStringAsync();
                            var options = new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true,
                            };
                            options.Converters.Add(new DateTimeConverter());
                            var content = JsonSerializer.Deserialize<SonarTasksSuccess>(_content, options);
                            if (content != null)
                            {
                                var task = content.tasks.FirstOrDefault();
                                if (task != null)
                                {
                                    var filter = Builders<TaskSonar>.Filter.Eq(x => x.id, task.id);
                                    var taskDb = await collection.Find(filter).FirstOrDefaultAsync(stoppingToken);
                                    if (taskDb == null)
                                    {
                                        await bus.Publish(new SyncSastRequest()
                                        {
                                            projectName = project.Name
                                        });
                                        await collection.InsertOneAsync(task, cancellationToken: stoppingToken);
                                    }
                                }
                            }
                        }
                        else
                        {
                            _logger.LogError($"Erro ao obter status das análises do SonarQube: {response.StatusCode}");
                        }
                    }
                    catch (Exception _ex)
                    {
                        _logger.LogError(_ex.Message);
                    }

                }
            }
            catch (Exception _ex)
            {
                _logger.LogError(_ex.Message);
                await Task.Delay(5000);
            }
            await Task.Delay(5000);
        }
    }
}


// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
public class Paging
{
    [JsonPropertyName("pageIndex")]
    public int pageIndex { get; set; }

    [JsonPropertyName("pageSize")]
    public int pageSize { get; set; }

    [JsonPropertyName("total")]
    public int total { get; set; }
}

public class SonarTasksSuccess
{
    [JsonPropertyName("tasks")]
    public List<TaskSonar> tasks { get; set; }

    [JsonPropertyName("paging")]
    public Paging paging { get; set; }
}

public class TaskSonar
{
    [JsonPropertyName("id")]
    public string id { get; set; }

    [JsonPropertyName("type")]
    public string type { get; set; }

    [JsonPropertyName("componentId")]
    public string componentId { get; set; }

    [JsonPropertyName("componentKey")]
    public string componentKey { get; set; }

    [JsonPropertyName("componentName")]
    public string componentName { get; set; }

    [JsonPropertyName("componentQualifier")]
    public string componentQualifier { get; set; }

    [JsonPropertyName("analysisId")]
    public string analysisId { get; set; }

    [JsonPropertyName("status")]
    public string status { get; set; }

    [JsonPropertyName("submittedAt")]
    public DateTime submittedAt { get; set; }

    [JsonPropertyName("submitterLogin")]
    public string submitterLogin { get; set; }

    [JsonPropertyName("startedAt")]
    public DateTime startedAt { get; set; }

    [JsonPropertyName("executedAt")]
    public DateTime executedAt { get; set; }

    [JsonPropertyName("executionTimeMs")]
    public int executionTimeMs { get; set; }

    [JsonPropertyName("hasScannerContext")]
    public bool hasScannerContext { get; set; }

    [JsonPropertyName("warningCount")]
    public int warningCount { get; set; }

    [JsonPropertyName("warnings")]
    public List<object> warnings { get; set; }

    [JsonPropertyName("infoMessages")]
    public List<object> infoMessages { get; set; }
}

enum ActivityStatus
{
    SUCCESS,
    FAILED,
    CANCELED,
    PENDING,
    IN_PROGRESS
}

public class DateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        Debug.Assert(typeToConvert == typeof(DateTime));
        return DateTime.Parse(reader.GetString());
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssZ"));
    }
}
