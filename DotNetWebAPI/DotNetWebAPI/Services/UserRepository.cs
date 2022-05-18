using AngularProject.Data;
using AngularProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AngularAPI.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly ShoppingDbContext _context;

        public UserRepository(ShoppingDbContext context, UserManager<User> userManager)
        {
            this._context = context;
        }

        public async Task<User?> GetByIdAsync(string id)
        {
            return await _context.Users.Include(u => u.Image).Include(u => u.Orders).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetByEmailAsync(string mail)
        {
            return await _context.Users.Include(u => u.Image).Include(u => u.Orders).FirstOrDefaultAsync(u => u.Email == mail);
        }

        public async Task<User> UpdateAsync(User newUser)
        {
            User user = (await GetByIdAsync(newUser.Id))!;

            user.UserName = newUser.UserName;
            user.Email = newUser.Email;
            user.Address = newUser.Address;
            user.PhoneNumber = newUser.PhoneNumber;
            user.Gender = newUser.Gender;
            //user.ImageId = newUser.ImageId;

            //_context.Users.Update(user!);
            await _context.SaveChangesAsync();
            return user;
        }

        public bool Exists(string id)
        {
            return _context.Users.Any(u => u.Id == id);
        }
    }
}
