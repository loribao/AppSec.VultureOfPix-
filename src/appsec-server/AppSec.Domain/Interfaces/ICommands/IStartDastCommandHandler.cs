using AppSec.Domain.Commands.StartDastCommand;

namespace AppSec.Domain.Interfaces.ICommands;

public interface IStartDastCommandHandler : IHandlerBase<StartDastRequest, StartDastResponse>
{

}
