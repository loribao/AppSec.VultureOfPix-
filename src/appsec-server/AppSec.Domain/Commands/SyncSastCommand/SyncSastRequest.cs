namespace AppSec.Domain.Commands.SyncSastCommand
{
    public record SyncSastRequest
    {
        public string projectName { get; set; }
    }
}
