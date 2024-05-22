using AppSec.Domain.Commands.SyncDastCommand;
using AppSec.Domain.Interfaces.ICommands;
using MassTransit;

namespace AppSec.Infra.Data.Consumers
{
    public class SyncDastConsumer : IConsumer<SyncDastRequest>
    {
        private readonly IBus bus;
        private readonly ISyncDastHandler syncDastHandler;

        public SyncDastConsumer(IBus bus, ISyncDastHandler syncDastHandler)
        {
            this.bus = bus ?? throw new ArgumentNullException(nameof(bus));
            this.syncDastHandler = syncDastHandler ?? throw new ArgumentNullException(nameof(syncDastHandler));
        }

        public async Task Consume(ConsumeContext<SyncDastRequest> context)
        {
            var response = await this.syncDastHandler.Handle(context.Message);
            await context.RespondAsync(response);
        }
    }
}
