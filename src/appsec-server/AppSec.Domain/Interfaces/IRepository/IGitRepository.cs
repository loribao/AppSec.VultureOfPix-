using AppSec.Domain.DTOs;

namespace AppSec.Domain.Interfaces.IRepository;

public interface IGitRepository
{
    string Clone(string url, string branch, string path, string user, string token);
    bool Pull(string path, string user, string email, string token, string branch);
    IEnumerable<DiffRepositoryDTO> AllDiff(string path, string branch, string projectName, string projectId);
    Task DiffSaveAsync(IEnumerable<DiffRepositoryDTO> diff, CancellationToken cancellation = default);
    IQueryable<DiffRepositoryDTO> GetAnalysisAsQueryable();
}
