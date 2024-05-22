using AppSec.Domain.Commands.CreateUserCommand;

namespace AppSec.Domain.Interfaces.ICommands
{
    public interface ICreateUserCommandHandler : IHandlerBase<CreateuserRequest, CreateUserResponse>
    {

    }
}
