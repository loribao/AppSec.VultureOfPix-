using Amazon.Runtime.Internal.Util;
using AppSec.Domain.Commands.StartRepositoryAnalysisCommand;
using AppSec.Domain.Commands.StartSastCommand;
using AppSec.Domain.Entities;
using AppSec.Domain.Interfaces.IRepository;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AppSec.Infra.Data.Works
{
    public class GitWork : BackgroundService
    {
        private readonly IProjectRepository projectRepository;
        private readonly IGitRepository gitRepository;
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly IBus bus;
        private readonly ILogger<GitWork> logger;
        public GitWork(ILogger<GitWork> logger,IServiceScopeFactory serviceScopeFactory, IBus bus)
        {
            this.serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = serviceScopeFactory.CreateScope())
                    {
                        var projectRepository = scope.ServiceProvider.GetRequiredService<IProjectRepository>();
                        var gitRepository = scope.ServiceProvider.GetRequiredService<IGitRepository>();
                        var projs = await projectRepository.GetAll();

                        foreach (var project in projs)
                        {
                            if (project.Repository?.Path != null)
                            {
                                var update = gitRepository.Pull(project.Repository.Path, project.Repository.UserName, project.Repository.UserEmail, project.Repository.Token, project.Repository.Branch);
                                if (update)
                                {
                                    await UpdatedRepository(project, stoppingToken);
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {

                    this.logger.LogError(e.Message);
                }

                await Task.Delay(5000);
            }
        }
        private async Task UpdatedRepository(ProjectEntity project, CancellationToken stoppingToken = default)
        {
            var requestSast = new StartSastRequest(project.Name);
            var requestGit = new StartRepositoryAnalysisRequest
            {
                ProjectName = project.Name
            };
            await bus.Publish(requestGit, stoppingToken);
            await bus.Publish(requestSast, stoppingToken);

        }
    }
}
