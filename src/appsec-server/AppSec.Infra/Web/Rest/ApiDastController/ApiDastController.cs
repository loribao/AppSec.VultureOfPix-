using AppSec.Infra.Data.Services.Commands;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace AppSec.Infra.Web.Rest.ApiDastController
{
    public static class ApiDastController
    {
        public static WebApplication ApiDastControllerExtend(this WebApplication app)
        {
            app.MapPost("/api/dast/start", async (DastStartCommand command, IPublishEndpoint publishEndpoint) =>
            {
                await publishEndpoint.Publish(command);
                return Results.Ok(command);
            })
            .WithName("api/dast/start")
            .WithOpenApi();
            return app;
        }
    }
}
