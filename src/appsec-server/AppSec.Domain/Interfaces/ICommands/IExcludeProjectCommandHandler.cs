using AppSec.Domain.Commands.ExcludeProjectCommand;

namespace AppSec.Domain.Interfaces.ICommands
{
    public interface IExcludeProjectCommandHandler : IHandlerBase<ExcludeProjectCommandRequest, ExcludeProjectCommandResponse>
    {
    }
}
