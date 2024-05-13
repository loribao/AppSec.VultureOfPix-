using AppSec.Domain.Entities;
using AppSec.Domain.Interfaces.IRepository;
using AppSec.Infra.Data.Context;
using Microsoft.Extensions.Logging;


namespace AppSec.Infra.Data.Repository;

public class DastRepository : IDastRepository
{
    private readonly ContextAppSec context;
    private readonly ILogger<DastRepository> _logger;
    private HttpClient _client = new HttpClient();
    public DastRepository(ContextAppSec context, ILogger<DastRepository> logger)
    {

        this.context = context;
        _logger = logger;
    }

    public async Task CreateDastProject(DastAnalysisEntity project)
    {
        throw new NotImplementedException();
    }
    public async Task RunAnalysis(string urlTarget, string dastApiKey, string dastUrlApi)
    {
        using (var client = new HttpClient())
        {
            try
            {
                client.BaseAddress = new Uri(dastUrlApi);
                string apiUrl = $"JSON/ajaxSpider/action/scan/?inScope=false&contextName=&subtreeOnly=false&zapapiformat=JSON&url={urlTarget}";

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    return;
                }
                else
                {
                    throw new HttpRequestException($"error: response {response.StatusCode} ");
                }
            }
            catch (HttpRequestException e)
            {
                throw new HttpRequestException(e.Message);
            }
        }
    }
    public async Task SyncAnalysis(string id)
    {


        using (var client = new HttpClient())
        {
            try
            {
                client.BaseAddress = new Uri("http://localhost:8090");
                string apiUrl = $"/JSON/ajaxSpider/view/status/?";

                HttpResponseMessage response = await client.GetAsync(apiUrl);

                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsByteArrayAsync();

            }
            catch (HttpRequestException e)
            {
                throw new HttpRequestException(e.Message);
            }
        }
    }

}
