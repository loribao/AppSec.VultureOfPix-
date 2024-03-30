using AppSec.Domain.Interfaces.IRepository;

namespace AppSec.Domain.Commands;

public class StartSastCommandHandler : IStartSastCommandHandler
{
    private readonly ISastRepository _sastRepository;

    public StartSastCommandHandler(ISastRepository sastRepository)
    {
        _sastRepository = sastRepository;
    }

    public async Task<StartSastResponse> Handle(StartSastRequest request, CancellationToken cancellationToken = default)
    {

        await _sastRepository.RunAnalysis(request.Id, request.token);
        await _sastRepository.SyncAnalysis(request.Id);
        return new StartSastResponse(Id: request.Id);
    }
}
