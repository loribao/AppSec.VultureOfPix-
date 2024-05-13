using AppSec.Domain.Interfaces.ICommands;
using AppSec.Domain.Interfaces.IRepository;

namespace AppSec.Domain.Commands.StartDastCommand;

public class StartDastCommandHandler : IStartDastCommandHandler
{
    private readonly IDastRepository _dastRepository;

    public async Task<StartDastResponse> Handle(StartDastRequest request, CancellationToken cancellationToken = default)
    {
        await _dastRepository.RunAnalysis(request.urlTarget, request.dastApiKey, request.dastUrlApi);
        await _dastRepository.SyncAnalysis(request.Id);
        var response = new StartDastResponse(Id: request.Id);
        throw new NotImplementedException();
    }
}
