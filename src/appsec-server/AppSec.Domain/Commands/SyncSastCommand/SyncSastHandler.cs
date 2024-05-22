using AppSec.Domain.Interfaces.ICommands;
using AppSec.Domain.Interfaces.IRepository;

namespace AppSec.Domain.Commands.SyncSastCommand
{
    public class SyncSastHandler : ISyncSastHandler
    {
        private readonly ISastRepository sastRepository;
        private readonly IProjectRepository projectRepository;
        private readonly ISyncExternalRepository syncExternalRepository;

        public SyncSastHandler(ISastRepository sastRepository, IProjectRepository projectRepository, ISyncExternalRepository syncExternalRepository)
        {
            this.sastRepository = sastRepository ?? throw new ArgumentNullException(nameof(sastRepository));
            this.projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
            this.syncExternalRepository = syncExternalRepository ?? throw new ArgumentNullException(nameof(syncExternalRepository));
        }

        public async Task<SyncSastResponse> Handle(SyncSastRequest request, CancellationToken cancellationToken = default)
        {
            var proj = projectRepository.GetByName(request.projectName);
            if (proj == null)
            {
                throw new Exception("project name not found");
            }
            await this.sastRepository.SyncAnalysis(proj.Name);
            return new SyncSastResponse();
        }
    }
}
