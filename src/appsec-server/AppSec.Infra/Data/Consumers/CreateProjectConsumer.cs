using AppSec.Domain.Commands.CreateProjectCommand;
using AppSec.Domain.Commands.LogInCommand;
using AppSec.Domain.Interfaces.ICommands;
using MassTransit;

namespace AppSec.Infra.Data.Consumers
{
    public class CreateProjectConsumer : IConsumer<CreateProjectRequest>
    {
        private readonly ICreateProjectCommandHandler handler;

        public CreateProjectConsumer(ICreateProjectCommandHandler handler)
        {
            this.handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        public async Task Consume(ConsumeContext<CreateProjectRequest> context)
        {
            var resp = await handler.Handle(context.Message);
            await context.RespondAsync(resp);
        }
    }
}
