using AppSec.Domain.Commands.SyncDastCommand;

namespace AppSec.Domain.Interfaces.ICommands
{
    public interface ISyncDastHandler : IHandlerBase<SyncDastRequest, SyncDastResponse>
    {

    }
}
