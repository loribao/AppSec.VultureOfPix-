using System.ComponentModel.DataAnnotations;

namespace AppSec.Domain.Entities;

public class RepoEntity
{
    public RepoEntity(string name, string url, IEnumerable<RepoCommitEntity> commits)
    {
        Id = 0;
        Name = name;
        Url = url;
        Commits = commits;
    }
    public RepoEntity()
    {

    }
    [Key]
    public int Id { get; set; } = 0;
    public string Name { get; set; } = "";
    public string Url { get; set; } = "";
    public string UserName { get; set; } = "";
    public string UserEmail { get; set; } = "";
    public string Branch { get; set; } = "";
    public IEnumerable<RepoCommitEntity> Commits { get; set; } = new List<RepoCommitEntity>();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
