using AppSec.Domain.Commands.CreateProjectCommand;
using AppSec.Domain.Commands.CreateUserCommand;
using AppSec.Domain.Commands.LogInCommand;
using AppSec.Domain.Commands.StartDastCommand;
using AppSec.Domain.Commands.StartSastCommand;
using AppSec.Domain.Interfaces.ICommands;
using AppSec.Domain.Interfaces.IDrivers;
using AppSec.Domain.Interfaces.IRepository;
using AppSec.Infra.Data.Consumers;
using AppSec.Infra.Data.Context;
using AppSec.Infra.Data.Drivers;
using AppSec.Infra.Data.Repository;
using AppSec.Infra.Web.Graphql;
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
                   IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.ASCII.GetBytes("testeasdfasdfasdfasdfasdfasdfadsfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfadsasfds")),
                   ValidateIssuer = false,
                   ValidateAudience = false,
               };
           });
        Services.AddAuthorization(options =>
        {
            options.AddPolicy("default", policy => policy.RequireRole("default"));
        });
        Services.AddSingleton<ContextAppSec>(options =>
        {
            var optionsBuilder = new DbContextOptionsBuilder<ContextAppSec>();
            var paths = configuration.GetRequiredSection("paths");
            optionsBuilder.UseSqlite($"Data Source={GetDatabasePath(paths.GetSection("database").Value ?? "appsec.sqlite")}", b =>
            {
                b.MigrationsAssembly("AppSec.Infra");
            });
            return new ContextAppSec(optionsBuilder.Options);
        });
        Services.AddScoped<IMongoDatabase>(x => new MongoClient(configuration.GetConnectionString("DefaultConnection")).GetDatabase("AppSec"));
        Services.AddScoped<ILanguageDriverSast, LanguageDriverSast>();
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
        return Services;
    }
    public static WebApplication ExtendWebApplicationBootStrap(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapGraphQL();
        return app;
    }
    private static string GetDatabasePath(string _path, string name = "appsec.sqlite")
    {
        var fullpath = Path.GetFullPath(_path);
        if (!Directory.Exists(fullpath))
        {
            Directory.CreateDirectory(fullpath);
        }
        return Path.GetFullPath(Path.Combine(fullpath, name));
    }
}
