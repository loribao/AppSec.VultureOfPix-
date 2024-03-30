namespace AppSec.Domain.Interfaces.ICommands;

public interface IHandlerBase<TRequest, TResponse>
{
    Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken = default);
}
