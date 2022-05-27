using AngularProject.Models;
using DotNetWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetWebAPI.Services
{
    public interface IOrderRepository
    {
        Task<ActionResult<IEnumerable<Order>>> ReturnAllOrders(string userId,OrderSearchModel orderSearchModel);
        Task<ActionResult<IEnumerable<Order>>> ReturnAllAdminOrders(string userId, OrderSearchModel orderSearchModel);
        Task<ActionResult<Order>> ReturnOrderById(int orderId);

        Task<Order> CreateOrderAsync(Order order);
        Task DeleteOrderAsync(int OrderId);
        Task<Order> PutOrder(int orderId, Order order);
        Task<IReadOnlyList<Order>> ReturnPendingOrders();
        bool IsOrderExixtsAsync(int id);
        Task AcceptOrder(int orderId);
        Task PendingOrder(int orderId);
        Task RejectOrder(int orderId);
    }
}
