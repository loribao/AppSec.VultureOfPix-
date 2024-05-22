namespace AppSec.Domain.Entities;

public class ProjectEntity : EntityBase
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public virtual RepoEntity? Repository { get; set; }
    public string TokenSast { get; set; }
    public string DockerfileMultiStage { get; set; }
    public IList<string> DastApis { get; set; }
    public IList<string> DastGraphql { get; set; }
    public IList<string> DastUIurl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
