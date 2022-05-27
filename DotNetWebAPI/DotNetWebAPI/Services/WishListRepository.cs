using AngularProject.Data;
using DotNetWebAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace DotNetWebAPI.Services
{
    public class WishListRepository : IWishListRepository
    {
        private readonly ShoppingDbContext _context;

        public WishListRepository(ShoppingDbContext context)
        {
            this._context = context;
        }

        public async Task<IReadOnlyList<WishListProduct>> GetUserWishList(string userId)
        {

            return await _context.WishListProducts
                .Where(W => W.UserId == userId)
                .Include(W => W.product)
                .ThenInclude(P => P.Images)
                .ToListAsync();
        }


        public async Task AddToWishList(int prdId, string userId)
        {
            await _context.WishListProducts.AddAsync(new WishListProduct() { ProductId = prdId, UserId = userId });
            await _context.SaveChangesAsync();

        }
        public async Task RemoveFromWishList(int prdId, string userId)
        {
            _context.WishListProducts.Remove(new WishListProduct() { ProductId = prdId, UserId = userId });
            await _context.SaveChangesAsync();

        }

        public bool WishListProductExists(int prdId, string userId)
        {
            return _context.WishListProducts.Any(W => W.UserId == userId && W.ProductId == prdId);
        }

    }
}
