using AppSec.Domain.DTOs;
using AppSec.Domain.Entities;
using AppSec.Domain.Interfaces.IDrivers;
using AppSec.Domain.Interfaces.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace AppSec.Infra.Data.Repository;

public class SastRepository : ISastRepository
{
    private readonly IMongoDatabase context;
    private readonly IMongoCollection<SastAnalisysEntity> collection;
    private readonly ILogger<SastRepository> _logger;
    private readonly ILanguageDriverSast _languageDriverSast;
    private readonly IConfiguration configuration;
    private readonly IProjectRepository projectRepository;
    public SastRepository(IMongoDatabase context, ILogger<SastRepository> logger, ILanguageDriverSast languageDriverSast, IConfiguration configuration)
    {

        _logger = logger;
        _languageDriverSast = languageDriverSast;
        this.context = context;
        collection = context.GetCollection<SastAnalisysEntity>("sonar");
        this.configuration = configuration;
    }

    public async Task<string> CreateIntegrationProject(SastAnalisysEntity project)
    {

        var baseAddress = new Uri(project.UrlBase);
        var project_name = project.Name;
        var project_key = project.Name;
        var token = project.Token;
        var token_name = project.Name;
        var user = project.User;
        var password = project.Password;
        var webhookName = "your_webhook_name";
        var webhookUrl = "your_webhook_url";
        using (var httpClient = new HttpClient { BaseAddress = baseAddress })
        {
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{user}:{password}")));

            var response = await httpClient.PostAsync($"api/projects/create?name={project_name}&project={project_name}", null);

            if (response.IsSuccessStatusCode)
            {
                var responseWebhook = await httpClient.PostAsync($"/api/webhooks/create?name={webhookName}&url={webhookUrl}&project={project_key}", null);
                var resultWebhook = await responseWebhook.Content.ReadAsStringAsync();
                _logger.LogInformation(resultWebhook);
                _logger.LogInformation("Projeto criado com sucesso");
            }
            else
            {
                _logger.LogError($"Falha ao criar o projeto: ${response.StatusCode}");
            }
        }
        HttpResponseMessage resp;
        using (var httpClient = new HttpClient { BaseAddress = baseAddress })
        {
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{user}:{password}")));

            var response = await httpClient.PostAsync($"api/user_tokens/generate?name={token_name}&projectKey={project_key}", null);

            if (response.IsSuccessStatusCode)
            {
                resp = response;
                token = await resp.Content.ReadAsStringAsync();
                _logger.LogInformation($"Token criado com sucesso: {await resp.Content.ReadAsStringAsync()}");
            }
            else
            {
                _logger.LogError($"Falha ao criar token: {response.ToString()}");
            }
        }
        return token;
    }

    public async Task SyncAnalysis(string id, CancellationToken cancellation = default)
    {
        try
        {
            var sonarurl = configuration.GetSection("sonar:url").Value ?? throw new Exception("not found sonar url");
            var token = configuration.GetSection("sonar:token").Value ?? throw new Exception("not found sonar token");
            var baseAddress = new Uri(sonarurl);
            var project_key = id;
            var metrics = "security_rating,coverage,ncloc,lines,bugs,vulnerabilities,vulnerabilities,functions,complexity,test_execution_time,test_errors,skipped_tests,test_failures,coverage,code_smells";
            var guid = Guid.NewGuid().ToString();
            var dateRun = DateTime.Now;
            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(token)));

                await foreach (var content in interateGet(httpClient, project_key, metrics, cancellation: cancellation))
                {
                    var _colletion = context.GetCollection<SastMesuaresComponentTreeDTO>("SastMeasuresTreeReports");
                    content._guid = guid;
                    content.DateRun = dateRun;
                    await _colletion.InsertOneAsync(content);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }

    }
    public async IAsyncEnumerable<SastMesuaresComponentTreeDTO> interateGet(HttpClient httpClient, string project_key, string metrics, int pageSize = 100, CancellationToken cancellation = default)
    {
        int pageIndex = 1;
        int total = 0;
        var _continue = true;
        do
        {
            if (cancellation.IsCancellationRequested) break;
            var url = $"/api/measures/component_tree?p={pageIndex}&ps={pageSize}&s=qualifier%2Cname&component={project_key}&strategy=leaves&s=name,path,qualifier&metricKeys={metrics}";
            var response = await httpClient.GetAsync(url);
            var contentString = await response.Content.ReadAsStringAsync();
            var opt =  new JsonSerializerOptions
            {
               
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };
            var content = JsonSerializer.Deserialize<SastMesuaresComponentTreeDTO>(contentString, opt);
            if (content == null)
            {
                break;
            }
            total = content.paging.total;
            pageIndex++;
            _continue = (((pageIndex - 1) * pageSize) < total);
            yield return content;
        } while (_continue);
    }

    public List<SastAnalisysEntity> Get()
    {
        throw new NotImplementedException();
    }

    public SastAnalisysEntity Get(string id)
    {
        throw new NotImplementedException();
    }

    public SastAnalisysEntity Create(SastAnalisysEntity entity)
    {
        throw new NotImplementedException();
    }

    public void Update(string id, SastAnalisysEntity entityIn)
    {
        throw new NotImplementedException();
    }

    public void Remove(SastAnalisysEntity entityIn)
    {
        throw new NotImplementedException();
    }

    public void Remove(string id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<SastMesuaresComponentTreeDTO> GetMesuaresQueryable()
    {
        return context.GetCollection<SastMesuaresComponentTreeDTO>("SastMeasuresTreeReports").AsQueryable();
    }
}
