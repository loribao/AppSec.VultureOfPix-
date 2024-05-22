using AppSec.Domain.Entities;
using AppSec.Domain.Interfaces.ICommands;
using AppSec.Domain.Interfaces.IRepository;

namespace AppSec.Domain.Commands.CreateProjectCommand;

public class CreateProjectCommandHandler : ICreateProjectCommandHandler
{
    private readonly IProjectRepository _projectRepository;
    private readonly IGitRepository _gitRepository;

    public CreateProjectCommandHandler(IProjectRepository projectRepository, IGitRepository gitRepository)
    {
        _projectRepository = projectRepository;
        _gitRepository = gitRepository;

    }
    public async Task<CreateProjectResponse> Handle(CreateProjectRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            var existis = this._projectRepository.GetByName(request.Name);

            var project = new ProjectEntity()
            {
                Id = Guid.NewGuid().ToString(),
                Name = request.Name,
                Description = request.Description,
                CreatedAt = DateTime.Now,
                LastUpdate = DateTime.Now,
                DockerfileMultiStage = request.DockerfileMultiStage,
                DastApis = request.DastApis,
                DastGraphql = request.DastGraphql,
                DastUIurl = request.DastUIurl,
                Repository = new RepoEntity()
                {
                    Url = request.UrlGit,
                    Branch = request.BranchGit,
                    CreatedAt = DateTime.Now,
                    LastUpdate = DateTime.Now,
                    Id = Guid.NewGuid().ToString(),
                    Name = request.Name,
                    Token = request.TokenRepository,
                    UserEmail = request.EmailRepository,
                    UserName = request.UserRepository,
                    Path = Path.Combine(Environment.CurrentDirectory, "data/repositories", request.Name),
                }
            };
            var tokenSast = await this._projectRepository.CreateSastToken(project.Name, project.Name, project.Name, project.Repository.Branch);
            project.TokenSast = tokenSast ?? "";

            if (existis != null)
            {
                project.Id = existis.Id;
                project.Repository.Id = existis.Repository?.Id ?? project.Repository.Id;
                await this._projectRepository.Update(project, cancellationToken);
            }
            else
            {
                await this._projectRepository.Create(project, cancellationToken);
            }
            await CloneRepository(project, request, cancellationToken);
            await rundiff(project, request, cancellationToken);
            return new CreateProjectResponse(project.Id);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    private async Task CloneRepository(ProjectEntity project, CreateProjectRequest request, CancellationToken cancellationToken)
    {

        if (project.Repository == null)
        {
            throw new Exception("entity repository not exists");
        }
        var path = project.Repository.Path;
        var pathrepository = _gitRepository.Clone(request.UrlGit, request.BranchGit, path, request.UserRepository, request.TokenRepository);
    }
    private async Task rundiff(ProjectEntity project, CreateProjectRequest request, CancellationToken cancellationToken)
    {
        if (project.Repository == null)
        {
            throw new Exception("entity repository not exists");
        }
        var diff = this._gitRepository.AllDiff(project.Repository.Path, request.BranchGit, project.Name, project.Id).ToList();
        await _gitRepository.DiffSaveAsync(diff, cancellationToken);
    }
}
