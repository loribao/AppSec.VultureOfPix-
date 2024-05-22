using AppSec.Domain.DTOs;
using AppSec.Domain.Entities;
using AppSec.Domain.Interfaces.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AppSec.Infra.Data.Repository;

public class DastRepository : IDastRepository
{
    private readonly ILogger<DastRepository> _logger;
    private readonly IConfiguration configuration;
    private readonly IMongoDatabase mongo;
    private readonly JsonSerializerOptions opt =  new JsonSerializerOptions
            {
               
                NumberHandling = JsonNumberHandling.AllowReadingFromString,                
};
public DastRepository(ILogger<DastRepository> logger, IConfiguration configuration, IMongoDatabase mongo)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        this.mongo = mongo ?? throw new ArgumentNullException(nameof(mongo));
    }

    public async Task CreateDastProject(DastAnalysisEntity project)
    {
        throw new NotImplementedException();
    }

    public async Task RunAnalysis(IEnumerable<string> urlTarget)
    {
        using (var client = new HttpClient())
        {
            try
            {
                var dastUrlApi = configuration.GetSection("zap:url").Value ?? throw new ArgumentNullException("zap:url");
                var dastApiKey = configuration.GetSection("zap:api_key").Value ?? throw new ArgumentNullException("zap:api_key");
                foreach (var url in urlTarget)
                {
                    if (!string.IsNullOrEmpty(dastApiKey))
                    {
                        client.DefaultRequestHeaders.Add("api_key", dastApiKey);
                    }
                    client.BaseAddress = new Uri(dastUrlApi);
                    await client.GetAsync("/JSON/pscan/action/enableAllScanners/?");
                    await client.GetAsync($"JSON/ajaxSpider/action/scan/?inScope=false&contextName=&subtreeOnly=false&zapapiformat=JSON&url={url}");

                    while (true)
                    {
                        var resp = await client.GetAsync("/JSON/ajaxSpider/view/status/?");
                        var content = await resp.Content.ReadAsStringAsync();
                        if (content.Contains("stopped"))
                        {
                            await client.GetAsync($"/JSON/ascan/action/scan/?url={url}&recurse=&inScopeOnly=&scanPolicyName=&method=&postData=&contextId=");

                            break;
                        }
                        await Task.Delay(5000);
                    }
                }
            }
            catch (HttpRequestException e)
            {
                _logger.LogError($"RunAnalysis {e.Message}, StackTrace:{e.StackTrace}");
            }
        }
    }

    public Task RunAnalysis(IEnumerable<string> urlUI, IEnumerable<string> apiURL, IEnumerable<string> graphqlURL)
    {
        throw new NotImplementedException();
    }

    public async Task SyncAnalysis(CancellationToken cancellationToken = default)
    {


        using (var client = new HttpClient())
        {
            try
            {
                var baseurl = configuration.GetSection("zap:url").Value ?? throw new ArgumentNullException("zap:url");
                client.BaseAddress = new Uri(baseurl);
                string apiUrl = $"/OTHER/core/other/jsonreport/?";

                HttpResponseMessage response = await client.GetAsync(apiUrl, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(responseBody))
                    {
                        var content = JsonSerializer.Deserialize<OwaspRepotDTO>(responseBody, opt);
                        if (content == null)
                        {
                            throw new Exception("report not found");
                        }
                        var projects = mongo.GetCollection<ProjectEntity>("projects");
                        foreach (var item in content.site)
                        {
                            var project = projects.AsQueryable().Where(x => x.DastUIurl.Any(x => x.Contains(item.name)) || x.DastApis.Any(x => x.Contains(item.name)) || x.DastGraphql.Any(x => x.Contains(item.name))).ToList()?.FirstOrDefault();
                            if (project != null)
                            {
                                item.projectId = project.Id;
                                item.projectName = project.Name;
                            }
                        }

                        var collection = mongo.GetCollection<OwaspRepotDTO>("DastReports");
                        await collection.InsertOneAsync(content, new InsertOneOptions()
                        {
                            BypassDocumentValidation = true
                        }, cancellationToken);
                    }
                }
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e.Message);
            }
        }
    }
    public IQueryable<OwaspRepotDTO> GetOwaspRepots()
    {
        return mongo.GetCollection<OwaspRepotDTO>("DastReports").AsQueryable();
    }

}
