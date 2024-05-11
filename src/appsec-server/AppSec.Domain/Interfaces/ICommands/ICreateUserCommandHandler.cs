using AppSec.Domain.Commands.CreateUserCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSec.Domain.Interfaces.ICommands
{
    public interface ICreateUserCommandHandler : IHandlerBase<CreateuserRequest, CreateUserResponse>
    {

    }
}
