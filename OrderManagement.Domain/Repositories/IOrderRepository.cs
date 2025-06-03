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
        /// <summary>
        /// Adds a new order.
        /// Returns the Id of the created order.
        /// </summary>
        Task<Guid> AddOrderAsync(Order order);

        /// <summary>
        /// Deletes an order with the specified orderId.
        /// Returns true if an order was found and deleted, false if no order was found and deleted.
        /// </summary>
        Task<bool> DeleteOrderAsync(Guid orderId);

        /// <summary>
        /// Returns the order by its orderId with OrderItems loaded, or null if not found.
        /// </summary>
        Task<Order?> GetOrderAsync(Guid orderId);

        Task SaveChangesAsync();
    }
}
