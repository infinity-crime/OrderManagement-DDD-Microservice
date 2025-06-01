using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Repositories;
using OrderManagement.Infrastructure.Data;

namespace OrderManagement.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderManagementDbContext _dbContext;
        public OrderRepository(OrderManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddOrderAsync(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> AddOrderItemAsync(Guid orderId, Guid productId, int qty, decimal unitPrice)
        {
            var order = await _dbContext.Orders.FindAsync(orderId);
            if(order != null)
            {
                order.AddOrderItem(productId, qty, unitPrice);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteOrderByIdAsync(Guid orderId)
        {
            var order = await _dbContext.Orders.FindAsync(orderId);
            if(order != null)
            {
                _dbContext.Orders.Remove(order);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public Task<bool> DeleteOrderItemAsync(Guid orderId, Guid itemId)
        {
            throw new NotImplementedException();
        }

        public async Task<Order?> GetOrderByIdAsync(Guid orderId)
        {
            return await _dbContext.Orders.FindAsync(orderId);
        }
    }
}
