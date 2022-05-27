using AngularProject.Data;
using AngularProject.Enums;
using AngularProject.Models;
using DotNetWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetWebAPI.Services
{
    public class OrderRepository : IOrderRepository
    {

        private readonly ShoppingDbContext _context;

        public OrderRepository(ShoppingDbContext context)
        {

            _context = context;
            
        }
        public async Task<ActionResult<IEnumerable<Order>>> ReturnAllOrders(OrderSearchModel orderSearchModel)
        {
            IQueryable<Order> query = _context.Orders.Where(w => w.UserID == "2dec20bc-7da6-411b-999c-2f45e40c9e16")
                .Include(o => o.OrderProducts).ThenInclude(P => P.Product);
            if (orderSearchModel != null)
            {
                if (!string.IsNullOrEmpty(orderSearchModel.OrdeState))
                {
                    switch (orderSearchModel.OrdeState)
                    {
                        //Pending, Accepted, Rejected
                        case "Pending"://LastWeek
                            query = query.Where(x => x.State == OrderState.Pending);
                            break;
                        case "Accepted"://LastMonth
                            query = query.Where(x => x.State == OrderState.Accepted);
                            break;
                        case "Rejected":
                            query = query.Where(x => x.State == OrderState.Rejected);
                            break;
                    }
                }
                if (!string.IsNullOrEmpty(orderSearchModel.orderDate))
                {
                    switch (orderSearchModel.OrdeState)
                    {
                        case "Newest":// newest Descending
                            query = query.OrderByDescending(x => x.Date);
                            break;
                        case "Oldest":// oldest
                            query = query.OrderBy(x => x.Date);
                            break;
                    }
                }

            }
            return await query.ToListAsync();
        }
        /******************************************
         * GET ALL ADMIN ORDERS
         * ***************************************/
        public async Task<ActionResult<IEnumerable<Order>>> ReturnAllAdminOrders(OrderSearchModel orderSearchModel)
        {
            IQueryable<Order> query = _context.Orders.Where(x => x.OrderProducts.Any(y => y.Product.OwnerId == "2dec20bc-7da6-411b-999c-2f45e40c9e16"))
                .Include(o => o.OrderProducts).ThenInclude(P => P.Product);

            if (orderSearchModel != null)
            {
                if (!string.IsNullOrEmpty(orderSearchModel.OrdeState))
                {
                    switch (orderSearchModel.OrdeState)
                    {
                        //Pending, Accepted, Rejected
                        case "Pending"://LastWeek
                            query = query.Where(x => x.State == OrderState.Pending);
                            break;
                        case "Accepted"://LastMonth
                            query = query.Where(x => x.State == OrderState.Accepted);
                            break;
                        case "Rejected":
                            query = query.Where(x => x.State == OrderState.Rejected);
                            break;
                    }
                }
                if (!string.IsNullOrEmpty(orderSearchModel.orderDate))
                {
                    switch (orderSearchModel.OrdeState)
                    {
                        case "Newest":// newest Descending
                            query = query.OrderByDescending(x => x.Date);
                            break;
                        case "Oldest":// oldest
                            query = query.OrderBy(x => x.Date);
                            break;
                    }
                }

            }
            return await query.ToListAsync();
        }
        /// <summary>
        /// ////////
        /// </summary>
        /// <param></param>
        /// <returns></returns>

        public async Task<ActionResult<Order>> ReturnOrderById(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);


            return order;
        }
        public async Task<Order> PutOrder(int orderId, Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            return order;

        }
        public async Task CreateOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

        }
        public async Task DeleteOrderAsync(int OrderId)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == OrderId);
            _context.Remove(order);
            await _context.SaveChangesAsync();
        }
        public bool IsOrderExixtsAsync(int id)
        {
            return _context.Orders.Any(p => p.Id == id);
        }

        public async Task<IReadOnlyList<Order>> ReturnPendingOrders()
        {


            return await _context.Orders.Where(x => x.State == OrderState.Pending).ToListAsync();
        }
        public async Task AcceptOrder(int orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
            order.State = OrderState.Accepted;

            await _context.SaveChangesAsync();
        }
        public async Task PendingOrder(int orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
            order.State = OrderState.Pending;
            _context.SaveChangesAsync();
        }
        public async Task RejectOrder(int orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
            order.State = OrderState.Rejected;
            _context.SaveChangesAsync();
        }

    }
}
