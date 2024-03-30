using AppSec.Domain.Entities;
using AppSec.Infra.Data.Context;
using AppSec.Infra.Data.Services.Commands;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AppSec.Infra.Web.Rest.ApiProjectController
{
    public static class ApiProjectController
    {
        public static WebApplication ApiProjectControllerExtend(this WebApplication app)
        {

            app.MapPost("/api/project", async (ProjectCreateCommand command, IPublishEndpoint publishEndpoint) =>
            {
                await publishEndpoint.Publish(command);
                return Results.Ok(command);
            })
              .WithName("Create a new project")
              .WithOpenApi();

            app.MapGet("/api/project/", async (ContextAppSec db) =>
            {
                var table = await db.Projects.Select(x => new ProjectEntity
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    CreatedAt = x.CreatedAt,
                    Path = x.Path,
                    Dast = x.Dast,
                    Sast = x.Sast,
                    Repository = x.Repository
                }).ToListAsync();
                return Results.Ok(table);
            }).WithName("List all projects")
              .WithOpenApi();

            return app;
        }
    }
}
