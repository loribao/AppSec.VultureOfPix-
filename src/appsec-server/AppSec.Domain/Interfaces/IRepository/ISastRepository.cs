using AppSec.Domain.DTOs;
using AppSec.Domain.Entities;

namespace AppSec.Domain.Interfaces.IRepository;

public interface ISastRepository : IRepositoryBase<SastAnalisysEntity>
{
    Task SyncAnalysis(string projectName, CancellationToken cancellation = default);
    IQueryable<SastMesuaresComponentTreeDTO> GetMesuaresQueryable();
}
