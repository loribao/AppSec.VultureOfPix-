using AppSec.Domain.Commands.SyncSastCommand;

namespace AppSec.Domain.Interfaces.ICommands
{
    public interface ISyncSastHandler : IHandlerBase<SyncSastRequest, SyncSastResponse>
    {
    }
}
