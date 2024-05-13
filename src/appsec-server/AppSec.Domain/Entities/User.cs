namespace AppSec.Domain.Entities
{
    public class User
    {
        public string Id { get; set; }
        public string UserLogin { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Role { get; set; }
    }
}
