namespace AppSec.Domain.Commands.LogInCommand
{
    public class LogInAuthRequest
    {
        public string UserLogin { get; set; }
        public string Password { get; set; }
    }
}
