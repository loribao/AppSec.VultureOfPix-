using AppSec.Domain.Entities;
using AppSec.Domain.Interfaces.IRepository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AppSec.Infra.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        public Task<User?> AddAndUpdateUser(User userObj)
        {
            throw new NotImplementedException();
        }

        public async Task<string?> Authenticate(string user, string pass)
        {
            if (user == "admin" && pass == "admin" || user == "admin@admin.com" && pass == "admin")
            {
                return await generateJwtToken(new User()
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    Id = 1,
                    UserLogin = user,
                    Password = "admin"
                });
            }
            else
            {
                return null;
            }
        }

        public Task<IEnumerable<User>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetById(int id)
        {
            throw new NotImplementedException();
        }
        private async Task<string?> generateJwtToken(User user)
        {
            //Generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = await Task.Run(() =>
            {

                var key = Encoding.ASCII.GetBytes("testeasdfasdfasdfasdfasdfasdfadsfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfasdfadsasfds");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] {
                        new Claim("id", user.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserLogin),
                        new Claim(ClaimTypes.Name, user.UserLogin),
                        new Claim(ClaimTypes.Role, user.Role??"default")
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                };
                return tokenHandler.CreateToken(tokenDescriptor);
            });

            return tokenHandler.WriteToken(token);
        }
    }
}
