using AppSec.Domain.Entities;
using AppSec.Domain.Interfaces.IRepository;
using AppSec.Infra.Data.Context;
using AppSec.Infra.Data.Drivers;
using Microsoft.Extensions.Logging;
using System.Text.Json;


namespace AppSec.Infra.Data.Repository;

public class DastRepository : IDastRepository
{
    private readonly ContextAppSec context;
    private readonly ILogger<DastRepository> _logger;
    public DastRepository(ContextAppSec context, ILogger<DastRepository> logger)
    {
        this.context = context;
        _logger = logger;
    }

    public async Task CreateDastProject(DastAnalysisEntity project)
    {
        throw new NotImplementedException();
    }
    public Task<byte[]?> RunAnalysis(string urlTarget, string dastApiKey, string dastUrlApi)
    {
        var ret = Task.Run(() =>
        {
            var dastApiURI = new Uri(dastUrlApi);
            var _ZapDriver = new ZapDriver(urlTarget, dastApiKey, $"{dastApiURI.Scheme}://{dastApiURI.Host}", dastApiURI.Port);
            _ZapDriver.InstallDependencies();
            var report = _ZapDriver.Run();
            return report;
        });
        return ret;
    }
    public async Task SyncAnalysis(int id, byte[] report)
    {
        try
        {
            var project = context.Projects.Select(x => new ProjectEntity()
            {
                Id = x.Id,
                Description = x.Description,
                Dast = x.Dast,
                CreatedAt = x.CreatedAt,
                Name = x.Name,
                Path = x.Path,
                Repository = x.Repository,
            }).FirstOrDefault(x => x.Id == id);
            if (project is null)
            {
                var strReport = report.ToString();
                var reportEntity = JsonSerializer.Deserialize<DastReport>(strReport);
                _ = project.Dast?.DastReports.Append(reportEntity);
                context.Update(project);
                context.SaveChanges();
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error on SyncAnalysis");
            throw;
        }
    }
}
