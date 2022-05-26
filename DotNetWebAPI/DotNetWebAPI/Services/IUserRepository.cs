using AngularAPI.Models;
using AngularProject.Models;

namespace AngularAPI.Services
{
    public interface IUserRepository
    {
        public Task<User?> GetByIdAsync(string id);
        public Task<User?> GetByEmailAsync(string mail);
        public Task<User> UpdateAsync(User user);
        public bool Exists(string id);
    }
}
