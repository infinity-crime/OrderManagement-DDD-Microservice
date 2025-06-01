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
        Task<bool> AddOrderItemAsync(Guid orderId, Guid productId, int qty, decimal unitPrice);
        Task<bool> DeleteOrderItemAsync(Guid orderId, Guid itemId);
        Task<bool> DeleteOrderByIdAsync(Guid orderId);
        Task<Order?> GetOrderByIdAsync(Guid orderId);
    }
}
