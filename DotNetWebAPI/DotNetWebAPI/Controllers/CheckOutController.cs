using AngularAPI.Dtos;
using AngularAPI.Services;
using AngularProject.Enums;
using AngularProject.Models;
using DotNetWebAPI.DTOs;
using DotNetWebAPI.Models;
using DotNetWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class CheckOutController : Controller
    {
        readonly IOrderRepository _orderRepository;
        readonly ICartRepository _cartRepository;

        public CheckOutController(IOrderRepository orderRepository, ICartRepository cartRepository)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
        }


        /// <summary>
        /// Checkout from shopping cart
        /// </summary>
        /// <param name="checkedOutItems"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<Order> Post()
        {
           // User? user = HttpContext.Items["User"] as User;
            var cartId = _cartRepository.GetCart("2dec20bc-7da6-411b-999c-2f45e40c9e16");
            var myCartItems = _cartRepository.GetProductsAvailableInCart(cartId); // List of cart Items
            List<OrderProducts> orderProducts = new();

            decimal Total = 0;

            foreach (var item in myCartItems)
            {
                item.Product.Quantity -= item.Quantity;
                orderProducts.Add(new OrderProducts()
                {
                    ProductId = item.Product.Id,
                    Quantity = item.Quantity,
                });
                Total += (item.Quantity * item.Product.price);
            }


            var myOrder = _orderRepository.CreateOrderAsync(new Order()
            {
                UserID = "2dec20bc-7da6-411b-999c-2f45e40c9e16",
                Date = DateTime.Now,
                State = OrderState.Pending,
                TotalPrice = Total,
                OrderProducts = orderProducts
            });
            //_cartRepository.ClearCart(user.Id);


            return (myOrder);

        }

    }
}
