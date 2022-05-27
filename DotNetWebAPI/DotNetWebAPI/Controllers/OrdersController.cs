using AngularProject.Models;
using DotNetWebAPI.Models;
using DotNetWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity.Infrastructure;

namespace DotNetWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private readonly IOrderRepository _orderRepo;

        public OrdersController(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }
        //Doone
        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders([FromQuery] OrderSearchModel orderSearchModel)
        {
            User? user = HttpContext.Items["User"] as User;
            return await _orderRepo.ReturnAllOrders(user.Id,orderSearchModel);
        }    
        [HttpGet("AdminOrders")]
        public async Task<ActionResult<IEnumerable<Order>>> ReturnAdminOrders([FromQuery] OrderSearchModel orderSearchModel)
        {
            User? user = HttpContext.Items["User"] as User;
            return await _orderRepo.ReturnAllAdminOrders(user.Id,orderSearchModel);
        }
        //Done
        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = _orderRepo.ReturnOrderById(id);

            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
        //Done
        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }
            try
            {
                await _orderRepo.PutOrder(id, order);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// Done
        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {

            await _orderRepo.CreateOrderAsync(order);

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }
        //Done
        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {

            //// Find Order By ID
            await _orderRepo.DeleteOrderAsync(id);

            return NoContent();
        }
        //get All pending Errors
        [HttpGet("PendingOrders")]
        public async Task<IReadOnlyList<Order>> GetPendingOrders()
        {
            return await _orderRepo.ReturnPendingOrders();
        }
        private bool OrderExists(int id)
        {
            return _orderRepo.IsOrderExixtsAsync(id);
        }
        [HttpPut("acceptOrder/{id}")]
        public async Task<IActionResult> AcceptOrder(int id)
        {

            //// Find Order By ID
            await _orderRepo.AcceptOrder(id);


            return Ok();
        }

        [HttpPut("pendingOrder/{id}")]
        public async Task<IActionResult> PendingOrder(int id)
        {

            //// Find Order By ID
            await _orderRepo.PendingOrder(id);


            return Ok();
        }
        [HttpPut("rejectOrder/{id}")]
        public async Task<IActionResult> RejectOrder(int id)
        {

            //// Find Order By ID
            await _orderRepo.RejectOrder(id);


            return Ok();
        }






    }

}
