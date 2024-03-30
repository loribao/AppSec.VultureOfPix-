using System.ComponentModel.DataAnnotations;

namespace AppSec.Domain.Entities;

public class ProjectEntity
{
    [Key]
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Path { get; set; }
    public virtual RepoEntity Repository { get; set; }
    public virtual SastAnalisysEntity? Sast { get; set; }
    public virtual DastAnalysisEntity? Dast { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
