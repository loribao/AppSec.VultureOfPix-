using AppSec.Domain.Interfaces.ICommands;
using AppSec.Domain.Interfaces.IRepository;

namespace AppSec.Domain.Commands.LogInCommand
{
    public class LogInCommandHandler : ILogInCommandHandler

    {
        private readonly IUserRepository repository;

        public LogInCommandHandler(IUserRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<LogInAuthReponse> Handle(LogInAuthRequest request, CancellationToken cancellationToken = default)
        {
            var response = await repository.Authenticate(request.UserLogin, request.Password) ?? "";
            return new LogInAuthReponse()
            {
                Token = response
            };
        }
    }
}
