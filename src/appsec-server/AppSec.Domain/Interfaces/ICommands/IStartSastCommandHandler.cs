using AppSec.Domain.Commands.StartSastCommand;

namespace AppSec.Domain.Interfaces.ICommands;

public interface IStartSastCommandHandler : IHandlerBase<StartSastRequest, StartSastResponse>
{

}
