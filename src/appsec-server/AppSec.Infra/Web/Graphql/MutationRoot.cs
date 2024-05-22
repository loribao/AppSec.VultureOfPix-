using AppSec.Domain.Commands.CreateProjectCommand;
using AppSec.Domain.Commands.CreateUserCommand;
using AppSec.Domain.Commands.LogInCommand;
using AppSec.Domain.Commands.StartDastCommand;
using AppSec.Domain.Commands.StartRepositoryAnalysisCommand;
using AppSec.Domain.Commands.StartSastCommand;
using HotChocolate.Authorization;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace AppSec.Infra.Web.Graphql
{
    [Authorize]
    public class MutationRoot
    {
        [AllowAnonymous]
        public async Task<LogInAuthReponse?> Login(LogInAuthRequest logIn, [FromServices] IBus bus, CancellationToken cancellation)
        {
            var resp = await bus.CreateRequestClient<LogInAuthRequest>().GetResponse<LogInAuthReponse>(new LogInAuthRequest()
            {
                Password = logIn.Password,
                UserLogin = logIn.UserLogin,
            }, cancellation);

            return resp.Message;
        }
        [Authorize("admin")]
        public async Task<CreateUserResponse> CreateUser(CreateuserRequest createuser, [FromServices] IBus bus, CancellationToken cancellation)
        {
            var response = await bus.CreateRequestClient<CreateuserRequest>().GetResponse<CreateUserResponse>(createuser, cancellation);
            return response.Message;
        }
        [Authorize("admin")]
        public async Task<string> CreateProject(CreateProjectRequest request, [FromServices] IBus bus, CancellationToken cancellation)
        {
            await bus.Publish(request, cancellation);
            return "await ...";
        }
        [Authorize("admin")]
        public async Task<string> StartDast(StartDastRequest request, [FromServices] IBus bus, CancellationToken cancellation)
        {
            await bus.Publish(request, cancellation);
            return "await ...";
        }
        [Authorize("admin")]
        public async Task<string> StartSast(StartSastRequest request, [FromServices] IBus bus, CancellationToken cancellation)
        {
            await bus.Publish(request, cancellation);
            return "await ...";
        }

        [Authorize("admin")]
        public async Task<string> StartDiffRepository(StartRepositoryAnalysisRequest request, [FromServices] IBus bus, CancellationToken cancellation)
        {
            await bus.Publish(request, cancellation);
            return "await ...";
        }
    }
}
