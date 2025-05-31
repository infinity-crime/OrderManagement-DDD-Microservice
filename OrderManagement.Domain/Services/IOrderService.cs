using OrderManagement.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain.Services
{
    public interface IOrderService
    {
        Task<Guid> CreateOrderAsync(Guid customerId, ShippingAddress address);
        Task<bool> DeleteOrderAsync(Guid orderId);
        Task AddItemToOrderAsync(Guid orderId, Guid productId, int qty, decimal unitPrice);
        Task<bool> DeleteItemToOrderAsync(Guid orderId, Guid itemId);
        Task ChangeAddressOrderAsync(Guid orderId, ShippingAddress address);
    }
}
