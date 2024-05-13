using AppSec.Domain.Entities;

namespace AppSec.Domain.Interfaces.IRepository
{
    public interface IUserRepository
    {
        Task<string?> Authenticate(string user, string pass);
        Task<IEnumerable<User>> GetAll();
        Task<User?> GetById(int id);
        Task<User?> AddAsync(User userObj);
    }
}
