using Microsoft.AspNetCore.Builder;
using System.Security.Claims;

namespace AppSec.Infra.Web.Rest.ApiPingController
{
    public static class ApiPingController
    {
        public static WebApplication ApiPingControllerExtend(this WebApplication app)
        {
            app.MapGet("/ping", (ClaimsPrincipal context) =>
            {
                Console.WriteLine($"is default ? {context.IsInRole("default")}");
                return "pong";
            })
                 .WithName("ping")
                 .WithOpenApi()
                 .RequireAuthorization(["default"]);

            return app;
        }
    }
}
