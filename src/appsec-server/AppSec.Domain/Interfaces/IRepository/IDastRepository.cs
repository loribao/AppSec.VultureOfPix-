using AppSec.Domain.DTOs;
using AppSec.Domain.Entities;

namespace AppSec.Domain.Interfaces.IRepository;

public interface IDastRepository
{
    Task CreateDastProject(DastAnalysisEntity project);
    Task RunAnalysis(IEnumerable<string> urlTarget);
    Task RunAnalysis(IEnumerable<string> urlUI, IEnumerable<string> apiURL, IEnumerable<string> graphqlURL);
    Task SyncAnalysis(CancellationToken cancellationToken = default);
    IQueryable<OwaspRepotDTO> GetOwaspRepots();
}
