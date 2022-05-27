using System.Security.Claims;
using System.Threading.Tasks;
using AngularProject.Models;
using DotNetWebAPI.DTOs;
using Microsoft.AspNetCore.Identity;

namespace AngularAPI.Services
{
    public interface ICartRepository
    {
        //UserManager<AppUser> userManager

        public void AddProductToCart(string userId, int ProductId);
        public string GetCart(string userId);
        public void RemoveCartItem(string userId, int productId);
        public void DeleteOneCartItem(string userId, int productId);
        public List<CartProductDto> GetCartItemCount(string userId);
        public List<CartProductDto> ClearCart(string userId);
        public void DeleteCart(string cartId);
        public List<CartProductDto> GetProductsAvailableInCart(string cartID);
        public Product GetProductData(int productId);
    }
}
