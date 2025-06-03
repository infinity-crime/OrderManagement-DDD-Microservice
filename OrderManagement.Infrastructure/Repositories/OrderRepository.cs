using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Repositories;
using OrderManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public async Task<bool> AddOrderItemAsync(OrderItem orderItem)
        {
            // unload all OrderItems so that the recalculation of the order amount is correct in any case
            var order = await _dbContext.Orders
                .Include(o => o.OrderItems) 
                .FirstOrDefaultAsync(o => o.Id == orderItem.OrderId);

            if(order != null)
            {
                order.AddOrderItem(orderItem);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteOrderAsync(Guid orderId)
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

        public async Task<bool> DeleteOrderItemAsync(Guid itemId)
        {
            var orderItem = await _dbContext.OrderItems.FindAsync(itemId);
            if(orderItem != null)
            {
                _dbContext.OrderItems.Remove(orderItem);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<Order?> GetOrderAsync(Guid orderId)
        {
            return await _dbContext.Orders.FindAsync(orderId);
        }
    }
}
