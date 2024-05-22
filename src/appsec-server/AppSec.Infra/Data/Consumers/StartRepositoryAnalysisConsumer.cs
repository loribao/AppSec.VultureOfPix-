using AppSec.Domain.Commands.StartRepositoryAnalysisCommand;
using AppSec.Domain.Interfaces.ICommands;
using MassTransit;

namespace AppSec.Infra.Data.Consumers
{
    public class StartRepositoryAnalysisConsumer : IConsumer<StartRepositoryAnalysisRequest>
    {
        private readonly IStartRepositoryAnalysisHandler handler;

        public StartRepositoryAnalysisConsumer(IStartRepositoryAnalysisHandler handler)
        {
            this.handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        public async Task Consume(ConsumeContext<StartRepositoryAnalysisRequest> context)
        {
            await handler.Handle(context.Message);
        }
    }
}
