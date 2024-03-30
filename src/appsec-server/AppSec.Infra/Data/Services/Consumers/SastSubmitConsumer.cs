using AppSec.Domain;
using AppSec.Domain.Commands;
using AppSec.Infra.Data.Services.Commands;
using MassTransit;
using Microsoft.Extensions.Logging;
namespace AppSec.Infra.Data.Services.Consumers;

public class SastSubmitConsumer : IConsumer<SastStartCommand>
{
    private readonly ILogger<SastSubmitConsumer> _logger;
    private readonly IStartSastCommandHandler _startSastCommandHandler;
    public SastSubmitConsumer(ILogger<SastSubmitConsumer> logger, IStartSastCommandHandler startSastCommandHandler)
    {
        _logger = logger;
        _startSastCommandHandler = startSastCommandHandler;
    }

    public async Task Consume(ConsumeContext<SastStartCommand> context)
    {
        var msg = context.Message;
        _logger.LogInformation($"Received SastStartSubmitConsumer: {msg.ProjectId}");
        var request = new StartSastRequest(msg.ProjectId, token: msg.Sonartoken);
        await _startSastCommandHandler.Handle(request);
        await Task.CompletedTask;
    }
}
