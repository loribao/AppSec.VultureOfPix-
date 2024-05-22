using AppSec.Domain.Enums;

namespace AppSec.Domain.Commands.CreateProjectCommand;
namespace AppSec.Domain.Commands.CreateProjectCommand;

public record CreateProjectRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string UrlGit { get; set; }
    public string BranchGit { get; set; }
    public string BranchGit { get; set; }
    public string UserRepository { get; set; }
    public string EmailRepository { get; set; }
    public string TokenRepository { get; set; }
    public Languages Language { get; set; } = Languages.CSharp;
    public string version { get; set; }
    public string DockerContextPath { get; set; }
    public string DockerfileMultiStage { get; set; }
    public IList<string>? DastApis { get; set; } = new List<string>();
    public IList<string>? DastGraphql { get; set; } = new List<string>();
    public IList<string> DastUIurl { get; set; }
}
