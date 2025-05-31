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

        public async Task AddAsync(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteByIdAsync(Guid orderId)
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

        public Task<Order?> GetByIdAsync(Guid orderId)
        {
            throw new NotImplementedException();
        }
    }
}
