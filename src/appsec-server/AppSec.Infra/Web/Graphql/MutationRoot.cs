using AppSec.Domain.Commands.CreateProjectCommand;
using AppSec.Domain.Commands.CreateUserCommand;
using AppSec.Domain.Commands.LogInCommand;
using AppSec.Domain.Commands.StartDastCommand;
using AppSec.Domain.Commands.StartSastCommand;
using AppSec.Domain.Interfaces.IRepository;
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
        [AllowAnonymous]
        public async Task<CreateUserResponse> CreateUser(CreateuserRequest createuser, [FromServices] IBus bus, CancellationToken cancellation)
        {
            var response = await bus.CreateRequestClient<CreateuserRequest>().GetResponse<CreateUserResponse>(createuser, cancellation);
            return response.Message;
        }

        [Authorize("admin")]
        public async Task<CreateProjectResponse> CreateProject(CreateProjectRequest project, [FromServices] IBus bus, CancellationToken cancellation)
        {
            var response = await bus.CreateRequestClient<CreateProjectRequest>().GetResponse<CreateProjectResponse>(project, cancellation);
            return response.Message;
        }
        [Authorize("admin")]
        public async Task<StartDastResponse> StartDast(StartDastRequest project, [FromServices] IBus bus, CancellationToken cancellation)
        {
            var response = await bus.CreateRequestClient<StartDastRequest>().GetResponse<StartDastResponse>(project, cancellation);
            return response.Message;
        }
        [Authorize("admin")]
        public async Task<StartSastResponse> StartSast(StartSastRequest project, [FromServices] IBus bus, CancellationToken cancellation)
        {
            var response = await bus.CreateRequestClient<StartSastRequest>().GetResponse<StartSastResponse>(project, cancellation);
            return response.Message;
        }
    }
}
