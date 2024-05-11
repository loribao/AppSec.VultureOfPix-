using AppSec.Domain.Commands.CreateUserCommand;
using AppSec.Domain.Interfaces.ICommands;
using MassTransit;

namespace AppSec.Infra.Data.Consumers
{
    public class CreateUserConsumer : IConsumer<CreateuserRequest>
    {
        private readonly ICreateUserCommandHandler handler;

        public CreateUserConsumer(ICreateUserCommandHandler handler)
        {
            this.handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        public async Task Consume(ConsumeContext<CreateuserRequest> context)
        {
            var resp = await handler.Handle(context.Message);
            await context.RespondAsync(resp);
        }
    }
}
