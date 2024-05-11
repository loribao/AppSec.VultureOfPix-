using AppSec.Domain.Entities;

namespace AppSec.Domain.Commands.CreateUserCommand
{
    public class CreateuserRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
    }
}
