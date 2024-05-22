using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace AppSec.Infra.Web.Rest
{
    public static class SonarListening
    {
        public static WebApplication WebHooks(this WebApplication app)
        {

            app.MapGet("/listening/sonar", async (res) =>
            {
                res.Response.StatusCode = 200;
                Results.Ok();
            });
            app.MapGet("/listening/ping", () => "pong");
            return app;
        }
    }
}
