using AppSec.Domain.Entities;
using AppSec.Domain.Interfaces.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace AppSec.Infra.Data.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IMongoDatabase context;
        private readonly IMongoCollection<ProjectEntity> collection;
        private readonly ILogger<ProjectRepository> _logger;
        private readonly IConfiguration configuration;
        public ProjectRepository(IMongoDatabase _context, ILogger<ProjectRepository> _logger, IConfiguration configuration)
        {
            context = _context;
            this.configuration = configuration;
            collection = context.GetCollection<ProjectEntity>("projects");
            this._logger = _logger;
        }

        public async Task Create(ProjectEntity project, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation($"Creating project: {project.Name}");
                collection.InsertOne(project);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task Delete(ProjectEntity project, CancellationToken cancellationToken = default)
        {
            await collection.DeleteOneAsync(x => x.Id == project.Id);
        }

        public ProjectEntity? GetById(string id)
        {
            try
            {
                return collection.Find(x => x.Id == id).First();
            }
            catch (Exception e)
            {

                throw e;
            }

        }
        public ProjectEntity? GetByName(string name)
        {
            try
            {
                return collection.Find(x => x.Name == name).FirstOrDefault();
            }
            catch (Exception e)
            {

                throw e;
            }

        }
        public async Task Update(ProjectEntity project, CancellationToken cancellationToken = default)
        {
            try
            {
                var filter = Builders<ProjectEntity>.Filter.Eq(x => x.Name, project.Name);
                var update = Builders<ProjectEntity>.Update
                    .Set(x => x.Name, project.Name)
                    .Set(x => x.Description, project.Description)
                    .Set(x => x.DockerfileMultiStage, project.DockerfileMultiStage)
                    .Set(x => x.DastApis, project.DastApis)
                    .Set(x => x.DastGraphql, project.DastGraphql)
                    .Set(x => x.DastUIurl, project.DastUIurl)
                    .Set(x => x.Repository, project.Repository)
                    .Set(x => x.TokenSast, project.TokenSast)
                    .Set(x => x.LastUpdate, DateTime.Now);
                await collection.UpdateOneAsync(filter, update);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public async Task<string> CreateSastToken(string project_name, string project_key, string token_name, string branch)
        {
            try
            {
                var projectToken = "";
                string webhookName = project_name;
                Uri webhookUrl = new Uri(configuration.GetSection("external_access:http").Value ?? throw new Exception("not find external url"));
                webhookUrl = new Uri(webhookUrl, "/listening/sonar");
                var baseAddress = new Uri(configuration.GetSection("sonar:url").Value ?? throw new Exception("not find sonar url"));
                string token = configuration.GetSection("sonar:token").Value ?? throw new Exception("not find sonar url");
                var defaultHeaders = new Dictionary<string, string>();
                var authHeader = new Dictionary<string, string>();
                var Authorization = "Basic " + Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(token));
                try
                {
                    using (var httpClient = new HttpClient { BaseAddress = baseAddress })
                    {
                        httpClient.DefaultRequestHeaders.Add("Authorization", Authorization);

                        var response = await httpClient.PostAsync($"/api/user_tokens/revoke?name={token_name}", null);

                        if (response.IsSuccessStatusCode)
                        {
                            _logger.LogInformation($"Token deletado com sucesso");
                        }
                    }

                }
                catch (Exception _e)
                {

                }
                try
                {
                    using (var httpClient = new HttpClient { BaseAddress = baseAddress })
                    {
                        httpClient.DefaultRequestHeaders.Add("Authorization", Authorization);

                        var response = await httpClient.PostAsync($"/api/projects/delete?project={project_name}", null);

                        if (response.IsSuccessStatusCode)
                        {
                            _logger.LogInformation($"Token deletado com sucesso");
                        }
                    }
                }
                catch (Exception e)
                {

                }

                HttpResponseMessage resp;
                using (var httpClient = new HttpClient { BaseAddress = baseAddress })
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", Authorization);

                    var response = await httpClient.PostAsync($"api/user_tokens/generate?name={token_name}&projectKey={project_key}", null);

                    if (response.IsSuccessStatusCode)
                    {
                        resp = response;
                        projectToken = await resp.Content.ReadAsStringAsync();
                        _logger.LogInformation($"Token criado com sucesso: {await resp.Content.ReadAsStringAsync()}");
                    }
                    else
                    {
                        _logger.LogError($"Falha ao criar token: {response.ToString()}");
                    }
                }
                using (var httpClient = new HttpClient { BaseAddress = baseAddress })
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", Authorization);

                    var response = await httpClient.PostAsync($"/api/projects/create?name={project_name}&project={project_name}&mainBranch={branch}", null);
                }
                return projectToken;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception($"Filed create token {e.Message}");
            }
        }
        public async Task<IEnumerable<ProjectEntity>> GetAll(CancellationToken cancellationToken = default)
        {
            var list = await collection.Find(_ => true).ToListAsync(cancellationToken);
            return list;
        }
    }
}
