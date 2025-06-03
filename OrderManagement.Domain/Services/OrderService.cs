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

        public async Task<bool> AddItemToOrderAsync(Guid orderId, Guid productId, int qty, decimal unitPrice)
        {
            try
            {
                var item = new OrderItem(orderId, productId, qty, unitPrice);
                return await _orderRepository.AddOrderItemAsync(item);
            }
            catch(DomainException)
            {
                return false;
            }
        }

        public async Task<bool> ChangeAddressOrderAsync(Guid orderId, ShippingAddress address)
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> CreateOrderAsync(Guid customerId, ShippingAddress address)
        {
            var newOrder = new Order(customerId, address, DateTime.Now);
            await _orderRepository.AddOrderAsync(newOrder);
            return newOrder.Id;
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
