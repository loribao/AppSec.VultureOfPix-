using AppSec.Domain.Commands.SyncSastCommand;
using AppSec.Domain.Interfaces.ICommands;
using MassTransit;

namespace AppSec.Infra.Data.Consumers
{
    public class SyncSastConsumer : IConsumer<SyncSastRequest>
    {
        private readonly ISyncSastHandler handler;

        public SyncSastConsumer(ISyncSastHandler handler)
        {
            this.handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        public Task Consume(ConsumeContext<SyncSastRequest> context)
        {
            handler.Handle(context.Message);
            return Task.CompletedTask;
        }
    }
}
