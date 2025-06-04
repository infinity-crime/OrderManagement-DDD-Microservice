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

        public async Task<Guid> CreateOrderAsync(Guid customerId, ShippingAddress address)
        {
            var newOrder = new Order(customerId, address, DateTime.Now);
            await _orderRepository.AddOrderAsync(newOrder);
            return newOrder.Id;
        }

        public async Task DeleteOrderAsync(Guid orderId)
        {
            bool isDeleted = await _orderRepository.DeleteOrderAsync(orderId);
            if (!isDeleted)
                throw new OrderIdNotFoundException("Order with received Id not found!", orderId);
        }

        public async Task<Guid> AddItemToOrderAsync(Guid orderId, Guid productId, int qty, decimal unitPrice)
        {
            var order = await _orderRepository.GetOrderAsync(orderId);
            if (order == null)
                throw new OrderIdNotFoundException("Order with received Id not found!", orderId);

            var orderItem = new OrderItem(orderId, productId, qty, unitPrice);

            order.AddOrderItem(orderItem);

            await _orderRepository.SaveChangesAsync();

            return orderItem.Id;
        }

        public async Task DeleteItemToOrderAsync(Guid orderId, Guid itemId)
        {
            var order = await _orderRepository.GetOrderAsync(orderId);
            if (order == null)
                throw new OrderIdNotFoundException("Order with received Id not found!", orderId);

            if (!order.DeleteOrderItem(itemId))
                throw new OrderItemIdNotFoundException("OrderItem with received Id not found!", itemId);
            
            await _orderRepository.SaveChangesAsync();
        }

        public async Task ChangeAddressOrderAsync(Guid orderId, ShippingAddress address)
        {
            var order = await _orderRepository.GetOrderAsync(orderId);
            if (order == null)
                throw new OrderIdNotFoundException("Order with received Id not found!", orderId);

            order.ChangeAddress(address);
            await _orderRepository.SaveChangesAsync();
        }
    }
}
