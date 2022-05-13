using AngularAPI.Models;
using AngularProject.Models;

namespace AngularAPI.Services
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(string id);
        Task<User> UpdateUserAsync(User user);
        bool IsUserExixtsAsync(string id);
    }
}
