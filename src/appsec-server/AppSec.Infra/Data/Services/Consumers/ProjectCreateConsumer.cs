using AppSec.Domain;
using AppSec.Domain.Interfaces.ICommands;
using AppSec.Infra.Data.Services.Commands;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace AppSec.Infra.Data.Services.Consumers;

public class ProjectCreateConsumer : IConsumer<ProjectCreateCommand>
{
    private readonly ILogger<ProjectCreateConsumer> _logger;
    private readonly ICreateProjectCommandHandler commandHandle;
    public ProjectCreateConsumer(ILogger<ProjectCreateConsumer> logger, ICreateProjectCommandHandler commandHandle)
    {
        _logger = logger;
        this.commandHandle = commandHandle;
    }
    public Task Consume(ConsumeContext<ProjectCreateCommand> context)
    {
        var msg = context.Message;
        _logger.LogInformation("ProjectCreateConsumer: {Name} {Description} {UrlGit} {Branch}", msg.Name, msg.Description, msg.UrlGit, msg.BranchGit);
        commandHandle.Handle(new CreateProjectRequest()
        {
            Branch = msg.BranchGit,
            Description = msg.Description,
            Name = msg.Name,
            UrlGit = msg.UrlGit,
            PasswordDast = msg.PasswordDast,
            UserDast = msg.UserDast,
            PasswordSast = msg.PasswordSast,
            UrlDast = msg.UrlDast,
            UrlSast = msg.UrlSast,
            UserSast = msg.UserSast,
            EmailRepository = msg.EmailRepository,
            UserRepository = msg.UserRepository,
            Language = msg.Language
        }, context.CancellationToken);
        return Task.CompletedTask;
    }
}
