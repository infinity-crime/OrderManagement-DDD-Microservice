using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task AddOrderAsync(Order order);

        Task<bool> AddOrderItemAsync(OrderItem orderItem);

        Task<bool> DeleteOrderItemAsync(Guid itemId);

        Task<bool> DeleteOrderAsync(Guid orderId);

        Task<Order?> GetOrderAsync(Guid orderId);
    }
}
