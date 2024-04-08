using AppSec.Domain.Interfaces.IRepository;
using HotChocolate.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppSec.Infra.Web.Graphql
{
    [Authorize]
    public class MutationRoot
    {
        [AllowAnonymous]
        public async Task<string?> Login(string user,string pass, [FromServices] IUserRepository repository)
        {
            return await repository.Authenticate(user, pass); 
        }
    }
}
