using System;
using System.Collections;
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
        /// Returns the order by its orderId with OrderItems loaded, or null if not found.
        /// The second optional parameter is responsible for tracking entities during the query. 
        /// If you don't need to track the entities being uploaded from the database, 
        /// then pass true to this parameter. Otherwise, do not pass anything.
        /// </summary>
        Task<Order?> GetOrderAsync(Guid orderId, bool asNoTracking = false);

        /// <summary>
        /// Returns a list of all orders, including OrderItems at the customer. 
        /// </summary>
        Task<ICollection> GetCustomerOrdersAsync(Guid customerId);

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
        /// Double function SaveChangesAsync from DbContext
        /// </summary>
        Task SaveChangesAsync();
    }
}
