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

        public async Task<IEnumerable<WishListProduct>> GetUserWishList(string userId)
        {
            if (_context.WishLists.Any(W => W.UserId == userId))
                _context.WishLists.Add(new WishList() { UserId = userId });

            var wishlist = await _context.WishLists.FirstOrDefaultAsync(W => W.UserId == userId);

            return await _context.WishListProducts
                .Where(W => W.WishListId == wishlist.Id)
                .Include(W => W.product)
                .ThenInclude(P => P.Images)
                .ToListAsync();
        }


        public async void AddToWishList(int prdId, string userId)
        {
            if (_context.WishLists.Any(W => W.UserId == userId))
                await _context.WishLists.AddAsync(new WishList() { UserId = userId });

            var wishlist = _context.WishLists.FirstOrDefault(W => W.UserId == userId);

            await _context.WishListProducts.AddAsync(new WishListProduct() { ProductId = prdId, WishListId = wishlist.Id });

        }
        public void RemoveFromWishList(int prdId, string userId)
        {
            var wishlist = _context.WishLists.FirstOrDefault(W => W.UserId == userId);

            _context.WishListProducts.Remove(new WishListProduct() { ProductId = prdId, WishListId = wishlist.Id });
        }

        public bool WishListProductExists(int prdId, string userId)
        {
            var wishlist = _context.WishLists.FirstOrDefault(W => W.UserId == userId);

            return _context.WishListProducts.Any(W => W.WishListId == wishlist.Id && W.ProductId == prdId);
        }

    }
}
