using AppSec.Domain.Commands.CreateProjectCommand;
using AppSec.Domain.Interfaces.ICommands;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace AppSec.Infra.Data.Consumers
{
    public class CreateProjectConsumer : IConsumer<CreateProjectRequest>
    {
        private readonly ICreateProjectCommandHandler handler;
        private readonly ILogger<CreateProjectConsumer> logger;
        public CreateProjectConsumer(ICreateProjectCommandHandler handler)
        {
            this.handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        public async Task Consume(ConsumeContext<CreateProjectRequest> context)
        {
            this.logger.LogDebug("Call CreateProjectConsumer.Consume");
            var resp = await handler.Handle(context.Message);
            await context.RespondAsync(resp);
        }
    }
}
