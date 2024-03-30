using AppSec.Infra.Data.Services.Commands;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace AppSec.Infra.Web.Rest.ApiSastController
{
    public static class ApiSastController
    {
        public static WebApplication ApiSastControllerExtend(this WebApplication app)
        {
            app.MapPost("/api/sast/start", async (SastStartCommand command, IPublishEndpoint publishEndpoint) =>
            {
                await publishEndpoint.Publish(command);
                return Results.Ok(command);
            })
               .WithName("api/sast/start")
               .WithOpenApi();
            return app;
        }
    }
}
