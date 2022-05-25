
using DotNetWebAPI.Models;

namespace DotNetWebAPI.Services
{
    public interface IWishListRepository
    {
        public void AddToWishList(int prdId, string userId);
        public void RemoveFromWishList(int prdId, string userId);
        public Task<IEnumerable<WishListProduct>> GetUserWishList(string id);

        public bool WishListProductExists(int prdId, string userId);

    }
}
