using AppSec.Domain.Entities;
using AppSec.Domain.Interfaces.ICommands;
using AppSec.Domain.Interfaces.IRepository;
namespace AppSec.Domain.Commands.Base;

public class CreateProjectCommandHandler : ICreateProjectCommandHandler
{
    private readonly IProjectRepository _projectRepository;
    private readonly IGitRepository _gitRepository;
    private readonly IDastRepository _dastRepository;
    private readonly ISastRepository _sastRepository;

    public CreateProjectCommandHandler(IProjectRepository projectRepository, IGitRepository gitRepository, IDastRepository dastRepository, ISastRepository sastRepository)
    {
        _projectRepository = projectRepository;
        _gitRepository = gitRepository;
        _dastRepository = dastRepository;
        _sastRepository = sastRepository;
    }
    public async Task<CreateProjectResponse> Handle(CreateProjectRequest request, CancellationToken cancellationToken = default)
    {
        try
        {

            var sast = new SastAnalisysEntity()
            {
                Id = 0,
                Name = request.Name,
                UrlBase = request.UrlSast,
                User = request.UserSast,
                Password = request.PasswordSast,
                Token = "",
                CreatedAt = DateTime.Now,
                Languages = request.Language,
                Description = request.Description
            };
            var dast = new DastAnalysisEntity()
            {
                Id = 0,
                Name = request.Name,
                UrlDast = request.UrlDast,
                UserDast = request.UserDast
            };
            var repo = new RepoEntity()
            {
                Branch = request.Branch,
                Url = request.UrlGit,
                Id = 0,
                UserEmail = request.EmailRepository,
                Name = request.Name,
                UserName = request.UserRepository,
                Commits = new List<RepoCommitEntity>()
            };
            var project = new ProjectEntity()
            {
                Id = 0,
                Name = request.Name,
                Description = request.Description,
                Path = Path.Combine(Path.GetTempPath(), request.Name),
                Repository = repo,
                Dast = dast,
                Sast = sast
            };


            var path = Path.Combine(Path.GetTempPath(), request.Name);
            var pathrepository = _gitRepository.Clone(request.UrlGit, request.Branch, path);
            _gitRepository.Pull(pathrepository, request.UserRepository, request.EmailRepository);
            var t = _gitRepository.HistoryCommit(request.Branch, path).GetAsyncEnumerator();
            var commits = new List<RepoCommitEntity>();
            while (await t.MoveNextAsync())
            {
                commits.Add(new RepoCommitEntity()
                {
                    Id = 0,
                    Message = t.Current.Message,
                    Date = t.Current.Date,
                    Author = t.Current.Author,
                    Email = t.Current.Email,
                    Files = t.Current.Files,
                    Sha = t.Current.Sha,
                    Status = t.Current.Status
                });
            }
            project.Repository.Commits = commits;
            var token = await _sastRepository.CreateIntegrationProject(sast);
            project.Sast.Token = token;
            await _projectRepository.Create(project);
            return new CreateProjectResponse();
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
