using AppSec.Domain.Entities;

namespace AppSec.Domain.Interfaces.IRepository;

public interface IProjectRepository
{
    Task Create(ProjectEntity project, CancellationToken cancellationToken = default);
    Task Delete(ProjectEntity project, CancellationToken cancellationToken = default);
    Task Update(ProjectEntity project, CancellationToken cancellationToken = default);
    ProjectEntity? GetById(int id);
}
