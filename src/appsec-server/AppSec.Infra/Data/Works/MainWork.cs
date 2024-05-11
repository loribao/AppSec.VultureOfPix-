using MassTransit;
using Microsoft.Extensions.Hosting;

namespace AppSec.Infra.Data.Works
{
    public class MainWork : BackgroundService
    {
        readonly IBus _bus;

        public MainWork(IBus bus)
        {
            _bus = bus;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //await _bus.CreateRequestClient<LogInAuthRequest>()
                //          .GetResponse<LogInAuthReponse>(new LogInAuthRequest { Password="",UserLogin=""});

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
