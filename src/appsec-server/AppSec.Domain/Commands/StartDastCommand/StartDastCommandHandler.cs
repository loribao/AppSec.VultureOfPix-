using AppSec.Domain.Interfaces.ICommands;
using AppSec.Domain.Interfaces.IRepository;

namespace AppSec.Domain.Commands.StartDastCommand;

public class StartDastCommandHandler : IStartDastCommandHandler
{
    private readonly IDastRepository _dastRepository;
    private readonly IProjectRepository _projectRepository;

    public StartDastCommandHandler(IDastRepository dastRepository, IProjectRepository projectRepository)
    {
        _dastRepository = dastRepository ?? throw new ArgumentNullException(nameof(dastRepository));
        _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
    }

    public async Task<StartDastResponse> Handle(StartDastRequest request, CancellationToken cancellationToken = default)
    {
        try
        {

            var project = this._projectRepository.GetByName(request.projectName);
            if (project == null)
            {
                throw new Exception("Project not found");
            }

            await this._dastRepository.RunAnalysis(project.DastUIurl);

            return new StartDastResponse(project.Id);
        }
        catch (Exception e)
        {
            return new StartDastResponse("");
        }
    }
}
