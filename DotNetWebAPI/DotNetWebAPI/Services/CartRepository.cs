using AngularProject.Data;
using AngularProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
namespace AngularAPI.Services
{
    public class CartRepository : ICartRepository
    {
        private readonly ShoppingDbContext _context;
        public CartRepository(ShoppingDbContext context)
        {
            _context = context;
        }
        public async Task<Cart> CreateCartAsync(User user)
        {
            //Create Cart
            var cart = new Cart() { UserId = user.Id, User = user, CartProducts = new List<CartProduct>() };
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task<bool> DeleteCartAsync(int CartId)
        {
            var cart = _context.Carts.FirstOrDefault(c => c.Id == CartId);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Cart> GetCartAsync(UserManager<User> userManager, ClaimsPrincipal _user)
        {
            var email = _user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var user = await userManager.Users.SingleOrDefaultAsync(u => u.Email == email);

            return await _context.Carts.FirstOrDefaultAsync(c => c.Id == user.CartID)
                ?? await CreateCartAsync(user);
        }

        public async Task<Cart> UpdateCartAsync(Cart cart)
        {
            var _cart = await _context.Carts.FirstOrDefaultAsync(c => c.Id == cart.Id);
            _cart.CartProducts = cart.CartProducts;
            await _context.SaveChangesAsync();
            return _cart;
        }
    }
}
