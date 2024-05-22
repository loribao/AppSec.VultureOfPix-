using AppSec.Domain.DTOs;
using AppSec.Domain.Entities;
using AppSec.Domain.Interfaces.IRepository;
using HotChocolate.Authorization;
using MongoDB.Driver;

namespace AppSec.Infra.Web.Graphql;

[Authorize]
public class QueryRoot
{
    [UseFiltering]
    public IQueryable<ProjectEntity> GetProjects([Service] IMongoDatabase db) => db.GetCollection<ProjectEntity>("projects").AsQueryable();

    [UseFiltering]
    public IQueryable<DiffRepositoryDTO> GetRepositoryReports([Service] IGitRepository git) => git.GetAnalysisAsQueryable();

    [UseFiltering]
    public IQueryable<SastMesuaresComponentTreeDTO> GetSastMeasuresTreeReports([Service] IMongoDatabase db) => db.GetCollection<SastMesuaresComponentTreeDTO>("SastMeasuresTreeReports").AsQueryable();

    [UseFiltering]
    public IQueryable<OwaspRepotDTO> GetOwaspRepots([Service] IMongoDatabase db) => db.GetCollection<OwaspRepotDTO>("DastReports").AsQueryable();

    [AllowAnonymous]
    public string Ping() => "Pong";
}
