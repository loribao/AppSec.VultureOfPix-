using Amazon.Runtime.Internal.Util;
using AppSec.Domain.DTOs;
using AppSec.Domain.Interfaces.IRepository;
using LibGit2Sharp;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace AppSec.Infra.Data.Repository;

public class GitRepository : IGitRepository
{
    private readonly IMongoDatabase context;
    private readonly IMongoCollection<DiffRepositoryDTO> collection;
    private readonly ILogger<GitRepository> logger;
    public GitRepository(ILogger<GitRepository> logger, IMongoDatabase context)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
        this.collection = context.GetCollection<DiffRepositoryDTO>("DiffRepositorys");
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public string Clone(string url, string branch, string path, string user, string token)
    {
        try
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
            var co = new CloneOptions()
            {
                BranchName = branch,
                Checkout = true,
                OnCheckoutProgress = (path, steps, totalSteps) =>
                {
                    logger.LogInformation($"{path} {steps}/{totalSteps}");
                }
            };
            co.FetchOptions.CredentialsProvider = (_url, _user, _cred) => new UsernamePasswordCredentials()
            {
                Username = user,
                Password = token
            };
            var rep = LibGit2Sharp.Repository.Clone(url, path, co);
            return rep;
        }
        catch (Exception e)
        {
            throw new Exception($"Clone error {e.Message}");
        }
    }
    public bool Pull(string path, string user, string email, string token, string branch)
    {
        var po = new PullOptions()
        {
            MergeOptions = new MergeOptions()
            {
                CommitOnSuccess = true
            },
            FetchOptions = new FetchOptions()
            {
                CredentialsProvider = (_url, _user, _cred) => new UsernamePasswordCredentials()
                {
                    Username = user,
                    Password = token
                }
            }
        };
        using var repo = new LibGit2Sharp.Repository(path);
        var af = repo.Commits.Count();
        var result = Commands.Pull(repo, new Signature(user, email, DateTimeOffset.Now), po);
        var nf = repo.Commits.Count();
        return af != nf;
    }
    public IEnumerable<DiffRepositoryDTO> AllDiff(string path, string branch, string projectName, string projectId)
    {
        string repositoryPath = path;
        using (var repo = new LibGit2Sharp.Repository(repositoryPath))
        {
            var filter = new CommitFilter { IncludeReachableFrom = repo.Branches[branch] };
            var dateAnalysis = DateTime.Now;
            foreach (var commit in repo.Commits.QueryBy(filter))
            {
                if (commit.Parents.Count() > 0)
                {
                    var parent = commit.Parents.First();
                    var changes = repo.Diff.Compare<TreeChanges>(parent.Tree, commit.Tree);
                    var patches = repo.Diff.Compare<Patch>(parent.Tree, commit.Tree);
                    var _commit = new DiffRepositoryDTO()
                    {
                        
                        ProjectId = projectId,
                        ProjectName = projectName,
                        OId = commit.Id.ToString(),
                        DateAuthor = commit.Author.When.DateTime.ToString(),
                        EmailAuthor = commit.Author.Email,
                        MsgCommit = commit.MessageShort,
                        NameAuthor = commit.Author.Name,
                        DateAnalysis = dateAnalysis,                        
                    };

                    foreach (var change in changes)
                    {
                        var _change = new ChangesRepository()
                        {
                            Path = change.Path,
                            OId = change.Oid.ToString(),
                            OldOId = change.OldOid.ToString(),
                            Status = change.Status.ToString(),
                            OldExists = change.OldExists,
                            OldMode = change.OldMode.ToString(),
                            Mode = change.Mode.ToString(),
                            OldFile = change.OldPath.ToString(),
                        };
                        _commit.FilesChanges.Add(_change);
                    }
                    foreach (var patch in patches)
                    {
                        var _patch = new DiffRepository()
                        {
                            diff = patch.Patch,
                            Path = patch.Path,
                            
                            LinesAdd = patch.LinesAdded,
                            LinesRemove = patch.LinesDeleted,
                            OldPath = patch.OldPath.ToString(),
                            Oid = patch.Oid.ToString(),
                            OldOid = patch.OldOid.ToString(),
                        };
                        _commit.diff.Add(_patch);
                    }
                    logger.LogInformation($"Id: {_commit.Id},Oid {_commit.OId}, ProjectName: {_commit.ProjectName}, Author: {_commit.NameAuthor}  {_commit.EmailAuthor} ");
                    Console.WriteLine($"Id: {_commit.Id},Oid {_commit.OId}, ProjectName: {_commit.ProjectName}, Author: {_commit.NameAuthor}  {_commit.EmailAuthor} ");
                    yield return _commit;
                }
            }
        }
    }
    public async Task DiffSaveAsync(IEnumerable<DiffRepositoryDTO> diff, CancellationToken cancellation = default)
    {
        try
        {
            await collection.InsertManyAsync(diff, cancellationToken: cancellation);
        }
        catch (Exception e)
        {
            throw e;
        }

    }
    public IQueryable<DiffRepositoryDTO> GetAnalysisAsQueryable() => collection.AsQueryable(new AggregateOptions()
    {
        BatchSize = 1000,
        MaxAwaitTime = TimeSpan.FromHours(1),
        BypassDocumentValidation = true,
        
    });

}
