using AppSec.Domain.Interfaces.IRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;

namespace AppSec.Infra.Web.Rest.ApiAuthController
{
    public static class ApiAuthController
    {
        public static WebApplication ApiAuthControllerExtend(this WebApplication app)
        {
            app.MapPost("/login", (string user, string pass, [FromServices] IUserRepository repository) =>
            {
                return repository.Authenticate(user, pass);
            })
             .WithName("login")
             .WithOpenApi();

            return app;
        }
    }
}
