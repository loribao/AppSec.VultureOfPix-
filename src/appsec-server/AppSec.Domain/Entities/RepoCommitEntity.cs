using System.ComponentModel.DataAnnotations;

public class RepoCommitEntity
{
    [Key]
    public int Id { get; set; }
    public RepoCommitEntity(string sha, string message, string author, string email, DateTime date, string status, IEnumerable<string> files)
    {
        Sha = sha;
        Message = message;
        Author = author;
        Email = email;
        Date = date;
        Status = status;
        Files = files;
    }
    public RepoCommitEntity()
    {
    }

    public string Sha { get; set; }
    public string Message { get; set; }
    public string Author { get; set; }
    public string Email { get; set; }
    public string Status { get; set; }
    public DateTime Date { get; set; }
    public IEnumerable<string> Files { get; set; }
}
