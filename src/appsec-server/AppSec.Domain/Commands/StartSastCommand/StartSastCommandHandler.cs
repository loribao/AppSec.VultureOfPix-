using AppSec.Domain.Interfaces.ICommands;
using AppSec.Domain.Interfaces.IRepository;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AppSec.Domain.Commands.StartSastCommand;

public class TokenSastDto
{
    [JsonPropertyName("login")]
    public string login { get; set; }

    [JsonPropertyName("name")]
    public string name { get; set; }

    [JsonPropertyName("token")]
    public string token { get; set; }

    [JsonPropertyName("createdAt")]
    public string createdAt { get; set; }

    [JsonPropertyName("type")]
    public string type { get; set; }
}
public class StartSastCommandHandler : IStartSastCommandHandler
{
    private readonly ISastRepository _sastRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IContainerRepository containerRepository;

    public StartSastCommandHandler(ISastRepository sastRepository, IProjectRepository projectRepository, IContainerRepository containerRepository)
    {
        _sastRepository = sastRepository ?? throw new ArgumentNullException(nameof(sastRepository));
        _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
        this.containerRepository = containerRepository ?? throw new ArgumentNullException(nameof(containerRepository));
    }

    public async Task<StartSastResponse> Handle(StartSastRequest request, CancellationToken cancellationToken = default)
    {
        var project = this._projectRepository.GetByName(request.NameProject);
        if (project == null)
        {
            throw new Exception("Project not found");
        }
        if (string.IsNullOrEmpty(project.DockerfileMultiStage))
        {
            return new StartSastResponse(project.Id);
        }
        var token = JsonSerializer.Deserialize<TokenSastDto>(project.TokenSast);
        var target = new Dictionary<string, string>();

        if (token == null || String.IsNullOrEmpty(token?.token))
        {
            throw new Exception("token SONARQUBE not found");
        }
        target.Add("SONARQUBE_TOKEN", token.token);
        await this.containerRepository.Build(project.DockerfileMultiStage, "sonar", target, $"sonar_{project.Name}", DateTime.Now.Millisecond.ToString(), project.Repository?.Path ?? "", cancellationToken: cancellationToken);
        return new StartSastResponse(project.Id);
    }
}
