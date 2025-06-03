using OrderManagement.Domain.Entities;
using OrderManagement.Domain.ValueObjects;
using OrderManagement.Domain.Repositories;
using OrderManagement.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace OrderManagement.Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task AddItemToOrderAsync(Guid orderId, Guid productId, int qty, decimal unitPrice)
        {
            var order = await _orderRepository.GetOrderAsync(orderId);
            if (order == null)
                throw new OrderIdNotFoundException("Order with received Id not found!", orderId);

            var orderItem = new OrderItem(orderId, productId, qty, unitPrice);

            order.AddOrderItem(orderItem);

            await _orderRepository.SaveChangesAsync();
        }

        public async Task<bool> ChangeAddressOrderAsync(Guid orderId, ShippingAddress address)
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> CreateOrderAsync(Guid customerId, ShippingAddress address)
        {
            try
            {
                var newOrder = new Order(customerId, address, DateTime.Now);
                await _orderRepository.AddOrderAsync(newOrder);
                return newOrder.Id;
            }
            catch(DomainException)
            {
                return Guid.Empty;
            }
        }

        public async Task<bool> DeleteItemToOrderAsync(Guid orderId, Guid itemId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteOrderAsync(Guid orderId)
        {
            return await _orderRepository.DeleteOrderAsync(orderId);
        }
    }
}
