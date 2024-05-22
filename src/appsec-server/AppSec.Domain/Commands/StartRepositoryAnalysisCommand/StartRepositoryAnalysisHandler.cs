using AppSec.Domain.Interfaces.ICommands;
using AppSec.Domain.Interfaces.IRepository;

namespace AppSec.Domain.Commands.StartRepositoryAnalysisCommand
{
    public class StartRepositoryAnalysisHandler : IStartRepositoryAnalysisHandler
    {
        private readonly IGitRepository git;
        private readonly IProjectRepository projectRepository;

        public StartRepositoryAnalysisHandler(IGitRepository git, IProjectRepository projectRepository)
        {
            this.git = git ?? throw new ArgumentNullException(nameof(git));
            this.projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
        }

        public async Task<StartRepositoryAnalysisResponse> Handle(StartRepositoryAnalysisRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var project = projectRepository.GetByName(request.ProjectName);
                if (project == null)
                {
                    throw new Exception("Project not found");
                }
                if (project.Repository == null)
                {
                    throw new Exception("Repository not found");
                }
                var diff = git.AllDiff(project.Repository.Path, project.Repository.Branch, project.Name, project.Id).ToList();
                await git.DiffSaveAsync(diff, cancellationToken);
                return new StartRepositoryAnalysisResponse()
                {
                    ProjectName = project.Name,
                    ProjectId = project.Id,
                    diff = diff
                };
            }
            catch (Exception e)
            {
                throw new Exception($"error diff: {e.Message}");
            }
        }
    }
}
