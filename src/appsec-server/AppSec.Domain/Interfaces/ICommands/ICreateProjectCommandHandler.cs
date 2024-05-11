using AppSec.Domain.Commands.CreateProjectCommand;

namespace AppSec.Domain.Interfaces.ICommands;

public interface ICreateProjectCommandHandler : IHandlerBase<CreateProjectRequest, CreateProjectResponse>
{
}
