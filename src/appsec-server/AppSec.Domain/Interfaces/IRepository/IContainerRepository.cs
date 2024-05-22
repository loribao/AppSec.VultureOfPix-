namespace AppSec.Domain.Interfaces.IRepository
{
    public interface IContainerRepository
    {
        Task Build(string dockerfilecontet, string target, Dictionary<string, string> buildArgs, string image, string tag, string project_dir, string dockerhost = "host.docker.internal:host-gateway", CancellationToken cancellationToken = default);
    }
}
