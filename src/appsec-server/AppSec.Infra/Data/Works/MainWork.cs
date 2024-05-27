using MassTransit;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Microsoft.Extensions.Configuration;
using AppSec.Domain.Entities;
using AppSec.Domain.Interfaces.IRepository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AppSec.Domain.DTOs;
using System.Globalization;

namespace AppSec.Infra.Data.Works
{
    public class MainWork : BackgroundService
    {
        readonly IBus _bus;
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly ILogger<MainWork> logger;
        private readonly IConfiguration configuration;
        public MainWork(ILogger<MainWork> logger, IBus bus, IConfiguration configuration, IServiceScopeFactory serviceScopeFactory)
        {
            _bus = bus ?? throw new ArgumentNullException(nameof(bus));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            var apiKey = new ApiKey(this.configuration.GetSection("ElasticsearchSettings:ApiKey").Value);
            var cloudid = this.configuration.GetSection("ElasticsearchSettings:CloudId").Value;
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = serviceScopeFactory.CreateScope();
                IGitRepository gitRepository = scope.ServiceProvider.GetRequiredService<IGitRepository>();
                ISastRepository sastRepository = scope.ServiceProvider.GetRequiredService<ISastRepository>();
                IDastRepository dastRepository = scope.ServiceProvider.GetRequiredService<IDastRepository>();
                IMongoDatabase mongo = scope.ServiceProvider.GetRequiredService<IMongoDatabase>();
                var client = new ElasticsearchClient(cloudid, apiKey);
                var _projects = mongo.GetCollection<ProjectEntity>("projects").AsQueryable().ToList();
                var index_git = "appsec-projects-git";
                var index_sast = "appsec-projects-sast";
                var index_dast = "appsec-projects-dast";
                var responsegit = await client.Indices.CreateAsync(index_git);
                var responsesast = await client.Indices.CreateAsync(index_sast);
                var responsedast = await client.Indices.CreateAsync(index_dast);

                try
                {
                    foreach (var item in _projects)
                    {
                        this.logger.LogDebug($"Sync Elastic - Git Tree - Projec:{item.Name} Id:{item.Id}");
                        var git = gitRepository.GetAnalysisAsQueryable().Where(x => x.ProjectName.Equals(item.Name)).OrderByDescending(x => x.DateAnalysis).FirstOrDefault();
                        if (git != null)
                        {
                            var guid = $"{item.Id}{git.DateAnalysis.GetHashCode().ToString()}";
                            var searchResponse = await client.SearchAsync<Reports<List<AppSec.Domain.DTOs.Component>>>(s => s
                                                        .Query(q => q
                                                        .Match(m => m
                                                           .Field(f => f.Guid)
                                                           .Query(guid))));
                            var list = gitRepository.GetAnalysisAsQueryable().Where(x => x.ProjectName.Equals(item.Name) && x.DateAnalysis == git.DateAnalysis).ToList();

                            if (!searchResponse.Documents.Any(x=>x.Guid == guid))
                            {
                                var report = new Reports<List<DiffRepositoryDTO>>
                                {
                                    ProjectName = item.Name,
                                    ProjectId = item.Id,
                                    DateAnalysis = git.DateAnalysis,
                                    TypeAnalysis = "git",
                                    Guid = guid,
                                    Report = list,
                                };
                                await client.IndexAsync(report, i => i.Index(index_git), stoppingToken);

                                await mongo.GetCollection<Reports<List<DiffRepositoryDTO>>>("ReportGit").InsertOneAsync(report);
                            }
                            else
                            {
                                var cdif = searchResponse.Documents.Select(x => x.Report.Count).First() != list.Count();
                                var hists = searchResponse.Hits.FirstOrDefault();
                                if (cdif && hists != null)
                                {
                                    var report = new Reports<List<DiffRepositoryDTO>>
                                    {
                                        ProjectName = item.Name,
                                        ProjectId = item.Id,
                                        DateAnalysis = git.DateAnalysis,
                                        TypeAnalysis = "git",
                                        Guid = guid,
                                        Report = list,
                                    };
                                    await client.DeleteAsync<Reports<List<DiffRepositoryDTO>>>(hists.Id, i => i.Index(index_git), stoppingToken);
                                    await client.IndexAsync(report, i => i.Index(index_git), stoppingToken);
                                    await mongo.GetCollection<Reports<List<DiffRepositoryDTO>>>("ReportGit").InsertOneAsync(report);
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {

                    this.logger.LogError($"{nameof(MainWork)} : {e.Message}");
                }
                try
                {
                    foreach (var item in _projects)
                    {
                        this.logger.LogDebug($"Sync Elastic - Git Tree - Projec:{item.Name} Id:{item.Id}");
                        var paging = sastRepository.GetMesuaresQueryable().Where(x => x.baseComponent.key.Equals(item.Name)).OrderBy(x => x.DateRun).Select(x => new { x.paging, x._guid }).FirstOrDefault();
                        if (paging != null)
                        {
                            var totalPages = 1 + (int)Math.Ceiling((decimal)(paging.paging.total / paging.paging.pageSize));

                            foreach (var p in Enumerable.Range(1, totalPages))
                            {
                                var sast = sastRepository.GetMesuaresQueryable().Where(x => x._guid.Equals(paging._guid) && x.paging.pageIndex == p).FirstOrDefault();
                                this.logger.LogDebug($"Sync Elastic - Sast Report - Projec:{item.Name} Id:{item.Id}");
                                if (sast != null)
                                {
                                    var searchResponse = await client.SearchAsync<Reports<List<AppSec.Domain.DTOs.Component>>>(s => s
                                                                                 .Query(q => q
                                                                                 .Match(m => m
                                                                                    .Field(f => f.Guid)
                                                                                    .Query(sast._guid))));
                          
                                    if (!searchResponse.Documents.Any())
                                    {
                                        var report = new Reports<List<AppSec.Domain.DTOs.Component>>
                                        {
                                            ProjectName = item.Name,
                                            ProjectId = item.Id,
                                            DateAnalysis = sast.DateRun,
                                            TypeAnalysis = "sast",
                                            Guid = sast._guid,
                                            Report = sast.components
                                        };
                                        await client.IndexAsync(report, i => i.Index(index_sast), stoppingToken);
                                        await mongo.GetCollection<Reports<List<AppSec.Domain.DTOs.Component>>>("ReportSast").InsertOneAsync(report);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {

                    this.logger.LogError($"{nameof(MainWork)} : {e.Message}");
                }
                try
                {
                    foreach (var item in _projects)
                    {
                        var dast = dastRepository.GetOwaspRepots().Where(x => x.site.Any(x => x.projectName.Equals(item.Name))).OrderBy(x => x.generated).FirstOrDefault();
                        this.logger.LogDebug($"Sync Elastic - Dast Report - Projec:{item.Name} Id:{item.Id}");
                        if (dast != null)
                        {
                            var searchResponse = await client.SearchAsync<Reports<List<AppSec.Domain.DTOs.Site>>>(s => s
                                                                                .Query(q => q
                                                                                .Match(m => m
                                                                                   .Field(f => f.Guid)
                                                                                   .Query(dast.id))));
                            if (!searchResponse.Documents.Any())
                            {
                                var dateAnalysis = DateTime.ParseExact(dast.generated, "ddd, dd MMM yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                                var report = new Reports<List<AppSec.Domain.DTOs.Site>>
                                {
                                    ProjectName = item.Name,
                                    ProjectId = item.Id,
                                    DateAnalysis = dateAnalysis,
                                    TypeAnalysis = "dast",
                                    Guid = dast.id,
                                    Report = dast.site
                                };
                                await client.IndexAsync(report, i => i.Index(index_dast), stoppingToken);
                                await mongo.GetCollection<Reports<List<AppSec.Domain.DTOs.Site>>>("ReportDast").InsertOneAsync(report);
                            }
                        }
                    }
                }
                catch (Exception e)
                {

                    this.logger.LogError($"{nameof(MainWork)} : {e.Message}");
                }
                
                await Task.Delay(30000, stoppingToken);
            }
        }
    }
}
