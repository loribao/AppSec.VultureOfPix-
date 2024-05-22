using AppSec.Domain.Commands.CreateProjectCommand;
using AppSec.Domain.Commands.CreateUserCommand;
using AppSec.Domain.Commands.ExcludeProjectCommand;
using AppSec.Domain.Commands.LogInCommand;
using AppSec.Domain.Commands.StartDastCommand;
using AppSec.Domain.Commands.StartRepositoryAnalysisCommand;
using AppSec.Domain.Commands.StartSastCommand;
using AppSec.Domain.Commands.SyncDastCommand;
using AppSec.Domain.Commands.SyncSastCommand;
using AppSec.Domain.Interfaces.ICommands;
using AppSec.Domain.Interfaces.IDrivers;
using AppSec.Domain.Interfaces.IRepository;
using AppSec.Infra.Data.Consumers;
using AppSec.Infra.Data.Drivers;
using AppSec.Infra.Data.Repository;
using AppSec.Infra.Data.Works;
using AppSec.Infra.Web.Graphql;
using AppSec.Infra.Web.Rest;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System.Text;
namespace AppSec.Bootstrap;

public static class Register
{

    public static IServiceCollection ExtendServicesBootStrap(this IServiceCollection Services, IConfiguration configuration)
    {
        var paths = configuration.GetRequiredSection("paths");
        var enckey = Environment.GetEnvironmentVariable("ENCRYPTION_KEY") ?? "";
        if (string.IsNullOrEmpty(enckey))
        {
            enckey = configuration.GetSection("Server:ENCRYPTION_KEY").Value ?? throw new ArgumentNullException("ENCRYPTION_KEY");
        }
        Services.AddElasticApm();
        Services.AddElasticApmForAspNetCore();
        Services.AddAllElasticApm();
        Services.AddAuthentication(
           x =>
           {
               x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
           }
           ).AddJwtBearer(x =>
           {
               x.RequireHttpsMetadata = false;
               x.SaveToken = true;
               x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.ASCII.GetBytes(enckey)),                   
                   ValidateIssuer = false,
                   ValidateAudience = false,
               };
           });
        Services.AddAuthorization(options =>
        {
            options.AddPolicy("default", policy => policy.RequireRole("default"));
            options.AddPolicy("admin", policy => policy.RequireRole("admin"));
        });
        Services.AddScoped<IMongoDatabase>(x => new MongoClient(configuration.GetConnectionString("DefaultConnection")).GetDatabase("AppSec"));
        Services.AddScoped<ILanguageDriverSast, LanguageDriverSast>();
        Services.AddScoped<ISyncExternalRepository, SyncExternalRepository>();
        Services.AddScoped<IContainerRepository, DockerRepository>();
        Services.AddScoped<IUserRepository, UserRepository>();
        Services.AddScoped<IProjectRepository, ProjectRepository>();
        Services.AddScoped<IGitRepository, GitRepository>();
        Services.AddScoped<IDastRepository, DastRepository>();
        Services.AddScoped<ISastRepository, SastRepository>();
        Services.AddScoped<ICreateProjectCommandHandler, CreateProjectCommandHandler>();
        Services.AddScoped<IStartSastCommandHandler, StartSastCommandHandler>();
        Services.AddScoped<IStartDastCommandHandler, StartDastCommandHandler>();
        Services.AddScoped<ILogInCommandHandler, LogInCommandHandler>();
        Services.AddScoped<ICreateUserCommandHandler, CreateUserCommandHandler>();
        Services.AddScoped<IExcludeProjectCommandHandler, ExcludeProjectCommandHandler>();
        Services.AddScoped<IStartRepositoryAnalysisHandler, StartRepositoryAnalysisHandler>();
        Services.AddScoped<ISyncSastHandler, SyncSastHandler>();
        Services.AddScoped<ISyncDastHandler, SyncDastHandler>();
        Services.AddMassTransit(x =>
         {
             x.AddConsumer<LogInConsumer>(cfg =>
             {
                 cfg.UseConcurrentMessageLimit(10);

             });
             x.AddConsumer<CreateProjectConsumer>(cfg =>
                {
                    cfg.UseConcurrentMessageLimit(10);
                });
             x.AddConsumer<CreateUserConsumer>(cfg =>
             {
                 cfg.UseConcurrentMessageLimit(10);
             });
             x.AddConsumer<StartDastConsumer>(cfg =>
             {
                 cfg.UseConcurrentMessageLimit(10);
             });
             x.AddConsumer<StartSastConsumer>(cfg =>
             {
                 cfg.UseConcurrentMessageLimit(10);
             });
             x.AddConsumer<StartRepositoryAnalysisConsumer>(x =>
             {
                 x.UseConcurrentMessageLimit(10);
             });
             x.AddConsumer<SyncSastConsumer>(x =>
             {
                 x.UseConcurrentMessageLimit(10);
             });
             x.AddConsumer<SyncDastConsumer>(x =>
             {
                 x.UseConcurrentMessageLimit(10);
             });
             x.UsingInMemory((context, cfg) =>
             {
                 cfg.ConfigureEndpoints(context);
             });
         });       
        Services
            .AddGraphQLServer()
            .AddAuthorization()
            .AddQueryType<QueryRoot>()
            .AddMutationType<MutationRoot>()
            .AddFiltering();
        Services.AddHostedService<GitWork>();
        Services.AddHostedService<StastWork>();
        Services.AddHostedService<DastWork>();
        Services.AddHostedService<MainWork>();
        return Services;
    }
    public static WebApplication ExtendWebApplicationBootStrap(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapGraphQL();
        app.WebHooks();

        return app;
    }

}
