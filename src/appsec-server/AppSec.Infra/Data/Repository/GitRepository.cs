using AppSec.Domain.Interfaces.IRepository;
using AppSec.Infra.Data.Context;
using LibGit2Sharp;
namespace AppSec.Infra.Data.Repository;

public class GitRepository : IGitRepository
{
    private readonly ContextAppSec db;

    public GitRepository(ContextAppSec db)
    {
        this.db = db;
    }
    public string LastCommit(string path)
    {
        try
        {
            using var repo = new LibGit2Sharp.Repository(path);
            var commit = repo.Commits.FirstOrDefault();
            return commit?.Id.ToString() ?? "";
        }
        catch (Exception e)
        {
            return "";
        }        
    }
    public string Clone(string url, string branch, string path)
    {

        try
        {

            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
            var rep = LibGit2Sharp.Repository.Clone(url, path, new CloneOptions()
            {
                BranchName = branch,                
            });
            return rep;
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    public void Pull(string path, string user, string email)
    {
        using var repo = new LibGit2Sharp.Repository(path);
        Commands.Pull(repo, new Signature(user, email, DateTimeOffset.Now), new PullOptions()
        {
            FetchOptions = new FetchOptions()
            {
                
            }
        });
    }
    public async IAsyncEnumerable<RepoCommitEntity> HistoryCommit(string branch, string path)
    {
        using var repo = new LibGit2Sharp.Repository(path);
        var filter = new CommitFilter
        {
            SortBy = CommitSortStrategies.Time,
            IncludeReachableFrom = repo.Branches[branch]
        };
        foreach (var commit in repo.Commits.QueryBy(filter))
        {
            foreach (var parent in commit.Parents)
            {
                var changes = repo.Diff.Compare<TreeChanges>(parent.Tree, commit.Tree);

                foreach (var change in changes)
                {
                    yield return new RepoCommitEntity()
                    {

                        Author = commit.Author.Name,
                        Email = commit.Author.Email,
                        Date = commit.Author.When.DateTime,
                        Message = commit.MessageShort,
                        Status = change.Status.ToString(),
                        Sha = commit.Sha,
                        Files = commit.Tree.Select(x => x.Path).ToList()
                    };
                }
            }
        }
    }
    
}
