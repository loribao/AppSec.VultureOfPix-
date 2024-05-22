using AppSec.Domain.Entities;
using AppSec.Domain.Interfaces.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AppSec.Infra.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoDatabase mongo;
        private readonly IConfiguration configuration;
        private readonly string envHash = Environment.GetEnvironmentVariable("ADMIN_HASH") ?? "";
        private readonly string enckey = Environment.GetEnvironmentVariable("ENCRYPTION_KEY") ?? "";
        public UserRepository(IMongoDatabase mongo, IConfiguration configuration)
        {
            this.mongo = mongo ?? throw new ArgumentNullException(nameof(mongo));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            if (string.IsNullOrEmpty(envHash))
            {
                this.envHash = configuration.GetSection("Server:ADMIN_HASH").Value ?? throw new ArgumentNullException("ADMIN_HASH");               
            }
            if (string.IsNullOrEmpty(enckey))
            {
                this.enckey = configuration.GetSection("Server:ENCRYPTION_KEY").Value ?? throw new ArgumentNullException("ENCRYPTION_KEY");
            }
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
                var hash = this.GetHash($"{user.Trim()}{pass.Trim()}") ?? "";
                if (envHash?.ToUpper() == hash?.ToUpper())
                {
                    return await generateJwtToken(new User()
                    {
                        FirstName = "Admin",
                        LastName = "Admin",
                        Id = "0",
                        UserLogin = user,
                        Password = "********",
                        Role = "admin"
                    });
                }

                var user_in_db = mongo.GetCollection<User>("users").AsQueryable().Where(x => x.UserLogin.Trim() == user.Trim()).First();
                var passhashstr = this.GetHash(pass.Trim()) ?? "";


                if (user_in_db.Password.ToUpper().Equals(passhashstr.ToUpper()))
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
                var key = Encoding.ASCII.GetBytes(enckey);
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
