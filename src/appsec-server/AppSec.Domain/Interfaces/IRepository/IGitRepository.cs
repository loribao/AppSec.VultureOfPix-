namespace AppSec.Domain.Interfaces.IRepository;

public interface IGitRepository
{
    string Clone(string url, string branch, string path);
    IAsyncEnumerable<RepoCommitEntity> HistoryCommit(string branch, string path);
    void Pull(string path, string user, string email);
}
