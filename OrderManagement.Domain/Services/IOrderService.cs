using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Exceptions;
using OrderManagement.Domain.ValueObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain.Services
{
    public interface IOrderService
    {
        /// <summary>
        /// Allows you to get a list of all orders, including OrderItems at the customer. 
        /// </summary>
        Task<ICollection> GetAllCustomerOrdersAsync(Guid customerId);

        /// <summary>
        /// Returns a list of all OrderItems at a single Order. 
        /// If no OrderId is found, an exception of type OrderIdNotFoundException is generated.
        /// </summary>
        Task<IEnumerable<object>> GetAllOrderItemsAsync(Guid orderId);

        /// <summary>
        /// Creating a new order in the database.
        /// Returns a new order Id or an exception of type DomainException if the input 
        /// data violates the business logic.
        /// </summary>
        Task<Guid> CreateOrderAsync(Guid customerId, ShippingAddress address);

        /// <summary>
        /// Deletes the order from the database.
        /// In case of failure, throws an exception of type OrderIdNotFoundException 
        /// (when trying to delete a non-existent Id).
        /// </summary>
        Task DeleteOrderAsync(Guid orderId);

        /// <summary>
        /// Adds a new OrderItem to the selected Order. 
        /// Returns the Guid Id of the added OrderItem. 
        /// If the specified OrderId does not exist, an exception of type OrderIdNotFoundException is thrown, 
        /// if an error in the parameters for the OrderItem - DomainException is thrown.
        /// </summary> 
        Task<Guid> AddItemToOrderAsync(Guid orderId, Guid productId, int qty, decimal unitPrice);

        /// <summary>
        /// Removes an OrderItem from the selected Order.
        /// If the specified OrderId does not exist, an exception of type OrderIdNotFoundException is thrown, 
        /// if a non-existent Id is specified for the OrderItem - OrderItemIdNotFoundException is thrown.
        /// </summary>
        Task DeleteItemToOrderAsync(Guid orderId, Guid itemId);

        /// <summary>
        /// Changes the address on the order. 
        /// If the order is not found - generates an OrderIdNotFoundException exception.
        /// </summary>
        Task ChangeAddressOrderAsync(Guid orderId, ShippingAddress address);
    }
}
