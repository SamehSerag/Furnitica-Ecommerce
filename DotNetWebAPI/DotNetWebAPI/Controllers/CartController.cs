using AngularAPI.Services;
using AngularProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AngularAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        private readonly UserManager<User> _userManager;

        public CartController(ICartRepository cartRepository, UserManager<User> userManager)
        {
            _cartRepository = cartRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult> GetCart()
        {
            // To retrieve at the client side --> UserManager<AppUser> userManager

            var cart = await _cartRepository.GetCartAsync(_userManager, User);
            return Ok(cart);
        }

        [HttpPost]
        public async Task<ActionResult<Cart>> UpdateCart(Cart cart)
        {
            var updatedCart = await _cartRepository.UpdateCartAsync(cart);
            return Ok(updatedCart);
        }
        [HttpDelete]
        public async Task DeleteCartAsync(int id)
        {
            // Id -> Sent from the Client side.
            await _cartRepository.DeleteCartAsync(id);
        }
    }
}
