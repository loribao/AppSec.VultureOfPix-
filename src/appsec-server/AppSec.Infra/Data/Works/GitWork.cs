using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;

namespace AppSec.Infra.Data.Works
{
    public class GitWork : BackgroundService
    {        
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
 
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(5000);
            }
        }
    }
}
