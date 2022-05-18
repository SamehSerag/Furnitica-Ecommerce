using System.Security.Claims;
using System.Threading.Tasks;
using AngularProject.Models;
using Microsoft.AspNetCore.Identity;

namespace AngularAPI.Services
{
    public interface ICartRepository
    {
        //UserManager<AppUser> userManager

        Task<Cart> CreateCartAsync(User user);
        Task<Cart> GetCartAsync(UserManager<User> userManager, ClaimsPrincipal user);
        Task<Cart> UpdateCartAsync(Cart cart);
        Task<bool> DeleteCartAsync(int CartId);
    }
}
