using AppSec.Domain.Interfaces.ICommands;
using AppSec.Domain.Interfaces.IRepository;

namespace AppSec.Domain.Commands.SyncDastCommand
{
    public class SyncDastHandler : ISyncDastHandler
    {
        private readonly IDastRepository dastRepository;

        public SyncDastHandler(IDastRepository dastRepository)
        {
            this.dastRepository = dastRepository ?? throw new ArgumentNullException(nameof(dastRepository));
        }

        public async Task<SyncDastResponse> Handle(SyncDastRequest request, CancellationToken cancellationToken = default)
        {
            await dastRepository.SyncAnalysis(cancellationToken);
            return new SyncDastResponse();
        }
    }
}
