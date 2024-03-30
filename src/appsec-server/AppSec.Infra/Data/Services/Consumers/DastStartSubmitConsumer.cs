using AppSec.Domain.Interfaces.IRepository;
using AppSec.Infra.Data.Services.Commands;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace AppSec.Infra.Data.Services.Consumers;

public class DastStartSubmitConsumer : IConsumer<DastStartCommand>
{
    private readonly ILogger<DastStartSubmitConsumer> _logger;
    private readonly IDastRepository _dastRepository;
    public DastStartSubmitConsumer(ILogger<DastStartSubmitConsumer> logger, IDastRepository dastRepository)
    {
        _dastRepository = dastRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<DastStartCommand> context)
    {
        var msg = context.Message;
        _logger.LogInformation($"Received DastStartSubmitConsumer: {msg.ProjectId} {msg.DastUrlApi} {msg.TargetUrl}");

        var data = await _dastRepository.RunAnalysis(msg.TargetUrl, msg.DastToken, msg.DastUrlApi);
        if (data is null || data.Length == 0)
        {
            _logger.LogError("Error on Dast Analysis: is null or empty");
            return;
        }
        await _dastRepository.SyncAnalysis(msg.ProjectId, data);
        await Task.CompletedTask;
    }
}
