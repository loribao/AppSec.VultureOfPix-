using AppSec.Domain.Commands;
using AppSec.Domain.Interfaces.ICommands;

namespace AppSec.Domain;

public interface IStartSastCommandHandler : IHandlerBase<StartSastRequest, StartSastResponse>
{

}
