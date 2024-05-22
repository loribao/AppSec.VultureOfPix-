using AppSec.Domain.Entities;

namespace AppSec.Domain.Interfaces.IRepository;

public interface IProjectRepository
{
    Task Create(ProjectEntity project, CancellationToken cancellationToken = default);
    Task Delete(ProjectEntity project, CancellationToken cancellationToken = default);
    Task Update(ProjectEntity project, CancellationToken cancellationToken = default);
    Task<IEnumerable<ProjectEntity>> GetAll(CancellationToken cancellationToken = default);
    Task<string> CreateSastToken(string project_name, string project_key, string token_name, string branch);
    ProjectEntity? GetById(string id);
    ProjectEntity? GetByName(string name);
}
