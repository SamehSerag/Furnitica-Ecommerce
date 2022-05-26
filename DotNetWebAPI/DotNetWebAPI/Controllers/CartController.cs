using AngularAPI.Repository;
using AngularAPI.Services;
using AngularProject.Models;
using DotNetWebAPI.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AngularAPI.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        // Get the list of products in the shopping cart
        [HttpGet("userId")]
        public async Task<List<CartProductDto>> Get(string userId)
        {
            string cartid = _cartRepository.GetCart(userId);
            return await Task.FromResult(_cartRepository.GetProductsAvailableInCart(cartid)).ConfigureAwait(true);
        }

        // Add a single product into the shopping cart or Increase the quantity of the product by ONE
        [HttpPost]

        [Route("AddToCart/{productId}")]
        public int Post(int productId)
        {
            User? user = HttpContext.Items["User"] as User;
            _cartRepository.AddProductToCart(user.Id, productId);
            return _cartRepository.GetCartItemCount(user.Id);

        [Route("AddToCart/{userId}/{productId}")]
        public int Post(string userId, int productId)
        {
            _cartRepository.AddProductToCart(userId, productId);
            return _cartRepository.GetCartItemCount(userId);

        }

        // Reduces the quantity by one for an item in shopping cart
        [HttpPut("{userId}/{productId}")]
        public int Put(string userId, int productId)
        {
            _cartRepository.DeleteOneCartItem(userId, productId);
            return _cartRepository.GetCartItemCount(userId);
        }

        // Delete a single item from the cart 
        [HttpDelete("{userId}/{productId}")]
        public int Delete(string userId, int productId)
        {
            _cartRepository.RemoveCartItem(userId, productId);
            return _cartRepository.GetCartItemCount(userId);
        }

        // Clear the shopping cart
        [HttpDelete("{userId}")]
        public int Delete(string userId)
        {
            return _cartRepository.ClearCart(userId);
        }
    }
}
