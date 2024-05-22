using AppSec.Domain.Interfaces.ICommands;

namespace AppSec.Domain.Commands.ExcludeProjectCommand
{
    public class ExcludeProjectCommandHandler : IExcludeProjectCommandHandler
    {
        public Task<ExcludeProjectCommandResponse> Handle(ExcludeProjectCommandRequest request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
