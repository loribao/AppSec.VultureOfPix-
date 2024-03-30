namespace AppSec.Domain.Commands;

public class StartDastCommandHandler : IStartDastCommandHandler
{
    public Task<StartDastResponse> Handle(StartDastRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
