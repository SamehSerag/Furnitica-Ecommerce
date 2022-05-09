using AngularProject.Data;
using AngularProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AngularAPI.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly ShoppingDbContext _context;
        private readonly UserManager<User> _userManager;

        public UserRepository(ShoppingDbContext context, UserManager<User> userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }
        public async Task<User> GetUserByIdAsync(string id)
        {
            return await _context.Users
                .Include(x => x.Orders)
                .Include(x => x.Image)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public bool IsUserExixtsAsync(string id)
        {
            return _context.Users
                           .Any(u => u.Id == id);
        }
    }
}
