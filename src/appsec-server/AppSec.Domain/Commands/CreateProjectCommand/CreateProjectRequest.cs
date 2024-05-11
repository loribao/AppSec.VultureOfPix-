using AppSec.Domain.Enums;

namespace AppSec.Domain.Commands.CreateProjectCommand;

public record CreateProjectRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string UrlGit { get; set; }
    public string BranchGit { get; set; }
    public string UserRepository { get; set; }
    public string EmailRepository { get; set; }
    public string ApiSast { get; set; }
    public string UserSast { get; set; }
    public string PasswordSast { get; set; }
    public string UrlDast { get; set; }
    public string UserDast { get; set; }
    public string PasswordDast { get; set; }
    public Languages Language { get; set; } = Languages.CSharp;
}
