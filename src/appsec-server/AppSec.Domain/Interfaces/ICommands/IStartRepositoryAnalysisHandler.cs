using AppSec.Domain.Commands.StartRepositoryAnalysisCommand;


namespace AppSec.Domain.Interfaces.ICommands
{
    public interface IStartRepositoryAnalysisHandler : IHandlerBase<StartRepositoryAnalysisRequest, StartRepositoryAnalysisResponse>
    {

    }
}
