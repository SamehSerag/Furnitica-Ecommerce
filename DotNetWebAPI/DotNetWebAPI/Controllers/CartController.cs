using AngularAPI.Services;
using AngularProject.Models;
using DotNetWebAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AngularAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        // Get the list of products in the shopping cart 
        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<List<CartProductDto>> Get()
        {
            User? user = HttpContext.Items["User"] as User;
            string cartid = _cartRepository.GetCart(user!.Id);
            return await Task.FromResult(_cartRepository.GetProductsAvailableInCart(cartid)).ConfigureAwait(true);
        }

        // Add a single product into the shopping cart or Increase the quantity of the product by ONE (Tested)
        [HttpPost]
        [Route("AddToCart/{productId}")]
        public List<CartProductDto> Post(int productId)
        {
            User? user = HttpContext.Items["User"] as User;
            _cartRepository.AddProductToCart(user!.Id, productId);
            return _cartRepository.GetCartItemCount(user.Id);
        }

        // Reduces the quantity by one for an item in shopping cart (Tested)
        [HttpPut("{productId}")]
        public  List<CartProductDto> Put(int productId)
        {
            User? user = HttpContext.Items["User"] as User;
            _cartRepository.DeleteOneCartItem(user.Id, productId);
            return _cartRepository.GetCartItemCount(user.Id);
        }

        // Delete a single item from the cart (Tested)
        [HttpDelete("{productId}")]
        public List<CartProductDto> Delete(int productId)
        {
            User? user = HttpContext.Items["User"] as User;
            _cartRepository.RemoveCartItem(user.Id, productId);
            return _cartRepository.GetCartItemCount(user.Id);
        }

        // Clear the shopping cart (Tested)
        [HttpDelete]
        public List<CartProductDto> Delete()
        {
            User? user = HttpContext.Items["User"] as User;
            return  _cartRepository.ClearCart(user.Id);
        }
    }
}
