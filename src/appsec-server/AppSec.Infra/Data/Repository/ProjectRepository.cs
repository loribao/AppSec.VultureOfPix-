using AppSec.Domain.Entities;
using AppSec.Domain.Interfaces.IRepository;
using AppSec.Infra.Data.Context;
using Microsoft.Extensions.Logging;

namespace AppSec.Infra.Data.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ContextAppSec context;
        private readonly ILogger<ProjectRepository> _logger;

        public ProjectRepository(ContextAppSec _context, ILogger<ProjectRepository> _logger)
        {
            context = _context;
            this._logger = _logger;
        }

        public async Task Create(ProjectEntity project, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation($"Creating project: {project.Name}");
                await context.AddAsync(project);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task Delete(ProjectEntity project, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"Deleting project: {project.Name}");
            context.Projects.Remove(project);
            context.SaveChanges();
        }
        public async Task Update(ProjectEntity project, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation($"Updating project: {project.Name}");
                context.Projects.Update(project);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }
        public ProjectEntity? GetById(int id)
        {
            _logger.LogInformation($"Getting project by id: {id}");
            return context.Projects.FirstOrDefault(p => p.Id == id);
        }
    }
}
