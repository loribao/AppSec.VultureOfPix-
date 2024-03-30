using AppSec.Domain.Entities;

namespace AppSec.Domain.Interfaces.IRepository;

public interface ISastRepository
{
    Task<string> CreateIntegrationProject(SastAnalisysEntity project);
    Task RunAnalysis(int id, string token);
    Task SyncAnalysis(int id);
}
