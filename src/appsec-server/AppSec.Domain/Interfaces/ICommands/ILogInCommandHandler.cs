using AppSec.Domain.Commands.LogInCommand;

namespace AppSec.Domain.Interfaces.ICommands
{
    public interface ILogInCommandHandler : IHandlerBase<LogInAuthRequest, LogInAuthReponse>
    {
    }
}
