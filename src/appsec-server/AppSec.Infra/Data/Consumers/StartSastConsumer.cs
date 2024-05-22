using AppSec.Domain.Commands.StartSastCommand;
using AppSec.Domain.Interfaces.ICommands;
using MassTransit;

namespace AppSec.Infra.Data.Consumers
{
    public class StartSastConsumer : IConsumer<StartSastRequest>
    {
        private readonly IStartSastCommandHandler handler;

        public StartSastConsumer(IStartSastCommandHandler handler)
        {
            this.handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        public async Task Consume(ConsumeContext<StartSastRequest> context)
        {
            var resp = await handler.Handle(context.Message);
            await context.RespondAsync(resp);
        }
    }
}
