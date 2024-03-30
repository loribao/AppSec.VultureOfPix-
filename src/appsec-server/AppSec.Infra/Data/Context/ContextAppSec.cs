using AppSec.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace AppSec.Infra.Data.Context;

public class ContextAppSec : DbContext
{
    public ContextAppSec(DbContextOptions<ContextAppSec> options)
        : base(options)
    {

    }

    public DbSet<DastAnalysisEntity> DastAnalysis { get; set; }
    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<SastAnalisysEntity> SastAnalisys { get; set; }
    public DbSet<RepoCommitEntity> RepoCommits { get; set; }
    public DbSet<RepoEntity> Repos { get; set; }
    public DbSet<User> Users { get; set; }
}
