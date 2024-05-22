namespace AppSec.Domain.Entities;

public class RepoEntity : EntityBase
{
    public RepoEntity(string name, string url, IEnumerable<RepoCommitEntity> commits)
    {
        Id = "";
        Name = name;
        Url = url;
    }
    public RepoEntity()
    {

    }
    public string Name { get; set; } = "";
    public required string Url { get; set; } = "";
    public required string UserName { get; set; } = "";
    public string UserEmail { get; set; } = "";
    public required string Branch { get; set; } = "";
    public required string Token { get; set; } = "";
    public required string Path { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
