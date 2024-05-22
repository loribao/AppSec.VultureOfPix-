using AppSec.Domain.Entities;
using AppSec.Domain.Interfaces.ICommands;
using AppSec.Domain.Interfaces.IRepository;

namespace AppSec.Domain.Commands.CreateUserCommand
{
    public class CreateUserCommandHandler : ICreateUserCommandHandler
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<CreateUserResponse> Handle(CreateuserRequest request, CancellationToken cancellationToken = default)
        {

            var usr = new User()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserLogin = request.Login,
                Password = request.Password,
                Role = request.Role,
                Id = Guid.NewGuid().ToString()
            };
            await _userRepository.AddAsync(usr);
            return new CreateUserResponse()
            {
                Login = usr.UserLogin,
                Role = usr.Role
            };
        }
    }
}
