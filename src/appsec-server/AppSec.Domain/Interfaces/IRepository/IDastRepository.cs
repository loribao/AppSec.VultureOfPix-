using AppSec.Domain.Entities;

namespace AppSec.Domain.Interfaces.IRepository;

public interface IDastRepository
{
    Task CreateDastProject(DastAnalysisEntity project);
    Task<byte[]?> RunAnalysis(string urlTarget, string dastApiKey, string dastUrlApi);
    Task SyncAnalysis(int id, byte[] report);
}
