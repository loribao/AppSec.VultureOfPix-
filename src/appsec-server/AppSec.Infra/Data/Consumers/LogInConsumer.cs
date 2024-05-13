using AppSec.Domain.Commands.LogInCommand;
using AppSec.Domain.Interfaces.ICommands;
using MassTransit;

namespace AppSec.Infra.Data.Consumers
{
    public class LogInConsumer : IConsumer<LogInAuthRequest>
    {
        private readonly ILogInCommandHandler handler;

        public LogInConsumer(ILogInCommandHandler handler)
        {
            this.handler = handler;
        }

        public async Task Consume(ConsumeContext<LogInAuthRequest> context)
        {
            var resp = await handler.Handle(new LogInAuthRequest()
            {
                Password = context.Message.Password,
                UserLogin = context.Message.UserLogin,
            });
            await context.RespondAsync(resp);
        }
    }
}
