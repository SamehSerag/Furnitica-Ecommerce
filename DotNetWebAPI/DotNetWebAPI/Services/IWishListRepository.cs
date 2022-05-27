
using DotNetWebAPI.Models;

namespace DotNetWebAPI.Services
{
    public interface IWishListRepository
    {
        public Task AddToWishList(int prdId, string userId);
        public Task RemoveFromWishList(int prdId, string userId);
        public Task<IReadOnlyList<WishListProduct>> GetUserWishList(string id);

        public bool WishListProductExists(int prdId, string userId);

    }
}
