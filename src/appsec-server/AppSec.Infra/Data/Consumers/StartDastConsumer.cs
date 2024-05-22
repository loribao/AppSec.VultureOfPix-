using AppSec.Domain.Commands.StartDastCommand;
using AppSec.Domain.Interfaces.ICommands;
using MassTransit;

namespace AppSec.Infra.Data.Consumers
{
    public class StartDastConsumer : IConsumer<StartDastRequest>
    {
        private readonly IStartDastCommandHandler handler;

        public StartDastConsumer(IStartDastCommandHandler handler)
        {
            this.handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        public async Task Consume(ConsumeContext<StartDastRequest> context)
        {
            var resp = await handler.Handle(context.Message);
            await context.RespondAsync(resp);
        }
    }
}
