using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Repositories;
using OrderManagement.Infrastructure.Data;
using OrderManagement.Domain.Exceptions;
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

        public async Task<Guid> AddOrderAsync(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            return order.Id;
        }

        public async Task<bool> DeleteOrderAsync(Guid orderId)
        {
            var order = await _dbContext.Orders.FindAsync(orderId);
            if (order is null)
                return false;

            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Order?> GetOrderAsync(Guid orderId)
        {
            return await _dbContext.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
