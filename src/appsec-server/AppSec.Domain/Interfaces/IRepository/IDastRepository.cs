using AppSec.Domain.Entities;

namespace AppSec.Domain.Interfaces.IRepository;

public interface IDastRepository
{
    Task CreateDastProject(DastAnalysisEntity project);
    Task RunAnalysis(string urlTarget, string dastApiKey, string dastUrlApi);
    Task SyncAnalysis(string id);
}
