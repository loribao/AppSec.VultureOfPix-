using Amazon.Runtime.Internal.Util;
using AppSec.Domain.Commands.SyncDastCommand;
using AppSec.Domain.DTOs;
using AppSec.Domain.Interfaces.IRepository;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AppSec.Infra.Data.Works
{
    public class DastWork : BackgroundService
    {
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly IBus bus;
        private readonly IConfiguration configuration;
        private readonly ILogger<DastWork> logger;
        private Task taskRunScan;
        public DastWork(ILogger<DastWork> logger, IServiceScopeFactory serviceScopeFactory, IBus bus, IConfiguration configuration)
        {
            this.serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
            this.bus = bus ?? throw new ArgumentNullException(nameof(bus));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            this.taskRunScan = RunScan(stoppingToken);
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var urlbase = configuration.GetSection("zap:url").Value ?? throw new ArgumentNullException("zap:url");
                    using var scope = serviceScopeFactory.CreateScope();
                    using var http = new HttpClient();
                    var mongo = scope.ServiceProvider.GetRequiredService<IMongoDatabase>();
                    var zapdb = mongo.GetCollection<OwaspZapScan>("zapScansDto");
                    var project = scope.ServiceProvider.GetRequiredService<IProjectRepository>();
                    http.BaseAddress = new Uri(urlbase);
                    var response = await http.GetAsync("JSON/ascan/view/scans/");

                    if (response.IsSuccessStatusCode)
                    {
                        var publishEnvent = false;
                        var content = await response.Content.ReadAsStringAsync();
                        if (string.IsNullOrEmpty(content))
                        {
                            await Task.Delay(10000);
                            continue;
                        }


                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                        };
                        var scans = JsonSerializer.Deserialize<ScansDto>(content, options);
                        if (scans == null)
                        {
                            await Task.Delay(10000);
                            continue;
                        };
                        if (scans.scans.Where(x => x.state == "FINISHED").Count() != scans.scans.Count())
                        {
                            await Task.Delay(10000);
                            continue;
                        }
                        var indbcount = zapdb.EstimatedDocumentCount();
                        var collection = mongo.GetCollection<OwaspRepotDTO>("DastReports");
                        if (indbcount != scans.scans.Count() || collection.EstimatedDocumentCount() == 0)
                        {
                            publishEnvent = true;
                            await zapdb.DeleteManyAsync(x => true);
                            await zapdb.InsertManyAsync(scans.scans);
                        }
                        if (publishEnvent)
                        {
                            await bus.Publish(new SyncDastRequest());
                            publishEnvent = false;
                        }
                    }
                }
                catch (Exception e)
                {
                    this.logger.LogError($"ExecuteAsync: {e.Message}, trace: {e.Message}");
                }
                await Task.Delay(10000);
            }
        }
        private async Task RunScan(CancellationToken cancellation)
        {
            var period = configuration.GetRequiredSection("zap:period").Value ?? "60";
            int _time = 60;
            while (!cancellation.IsCancellationRequested)
            {
                try
                {
                    var _TimeSpan = Convert.ChangeType(period, typeof(int));
                    _time = (int)TimeSpan.FromMinutes((int)_TimeSpan).TotalMilliseconds;
                    await Task.Delay(_time);

                    using var scope = serviceScopeFactory.CreateScope();
                    using var http = new HttpClient();
                    var project = scope.ServiceProvider.GetRequiredService<IProjectRepository>();
                    var repository = scope.ServiceProvider.GetRequiredService<IDastRepository>();

                    foreach (var item in await project.GetAll())
                    {
                        await repository.RunAnalysis(item.DastUIurl);
                    }

                }
                catch (Exception e)
                {
                    this.logger.LogError($"Dast RunScan: {e.Message}, trace: {e.Message}");                   
                }
            }
        }
    }
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class ScansDto
    {
        [JsonPropertyName("scans")]
        public List<OwaspZapScan> scans { get; set; }
    }

    public class OwaspZapScan
    {
        [Key]
        public string _Id { get; set; } = Guid.NewGuid().ToString();

        [JsonPropertyName("reqCount")]
        public string reqCount { get; set; }

        [JsonPropertyName("alertCount")]
        public string alertCount { get; set; }

        [JsonPropertyName("progress")]
        public string progress { get; set; }

        [JsonPropertyName("newAlertCount")]
        public string newAlertCount { get; set; }

        [JsonPropertyName("id")]
        public string scanId { get; set; }

        [JsonPropertyName("state")]
        public string state { get; set; }
    }
}
