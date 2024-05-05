using AppSec.Domain.Entities;
using AppSec.Infra.Data.Context;
using HotChocolate.Authorization;

namespace AppSec.Infra.Web.Graphql;

[Authorize]
public class QueryRoot
{
    [UseFiltering]
    public IQueryable<ProjectEntity> GetProjects([Service] ContextAppSec db ) => db.Projects.AsQueryable();
    [UseFiltering]
    public IQueryable<DastAnalysisEntity> GetDastAnalysis([Service] ContextAppSec db) => db.DastAnalysis.AsQueryable();
    [UseFiltering]
    public IQueryable<SastAnalisysEntity> GetSastAnalysis([Service] ContextAppSec db) => db.SastAnalisys.AsQueryable();
    [UseFiltering]
    public IQueryable<User> GetUsers([Service] ContextAppSec db) => db.Users.AsQueryable();
    [UseFiltering]
    public IQueryable<RepoEntity> GetRepositorys([Service] ContextAppSec db) => db.Repos.AsQueryable();
    [UseFiltering]
    public IQueryable<RepoCommitEntity> GetRepoCommits([Service] ContextAppSec db) => db.RepoCommits.AsQueryable();

    [AllowAnonymous]
    public string Ping() => "Pong";
}
