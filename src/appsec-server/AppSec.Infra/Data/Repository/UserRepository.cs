using AppSec.Domain.Entities;
using AppSec.Domain.Interfaces.IRepository;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace AppSec.Infra.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoDatabase mongo;

        public UserRepository(IMongoDatabase mongo)
        {
            this.mongo = mongo ?? throw new ArgumentNullException(nameof(mongo));
        }

        private readonly SHA256 pass = SHA256.Create();
        public string? GetHash(string pass)
        {
            using var hash = SHA256.Create();
            byte[] hashBytes = hash.ComputeHash(Encoding.UTF8.GetBytes(pass));
            string hashstr = Convert.ToHexString(hashBytes);
            return hashstr;
        }
        public async Task<User?> AddAsync(User userObj)
        {
            var passhashstr = this.GetHash($"{userObj.Password}");
            userObj.Password = passhashstr;
            await mongo.GetCollection<User>("users").InsertOneAsync(userObj);
            return userObj;
        }

        public async Task<string?> Authenticate(string user, string pass)
        {
            try
            {
                var envHash = Environment.GetEnvironmentVariable("ADMIN_HASH");
                if (envHash == this.GetHash($"{user.Trim()}{pass.Trim()}"))
                {
                    return await generateJwtToken(new User()
                    {
                        FirstName = "Admin",
                        LastName = "Admin",
                        Id = "0",
                        UserLogin = user,
                        Password = "********",
                        Role="admin"
                    });
                }

                var user_in_db = mongo.GetCollection<User>("users").AsQueryable().Where(x => x.UserLogin.Trim() == user.Trim()).First();
                var passhashstr = this.GetHash(pass.Trim());
          

                if (user_in_db.Password.Equals(passhashstr))
                {
                    return await generateJwtToken(user_in_db);
                }
                
                else
                {
                    return null;
                }
            }
            catch (Exception e)
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
