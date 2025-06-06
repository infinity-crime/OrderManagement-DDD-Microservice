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
using System.Collections;

namespace OrderManagement.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderManagementDbContext _dbContext;
        public OrderRepository(OrderManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Order?> GetOrderAsync(Guid orderId, bool asNoTracking = false)
        {
            return asNoTracking switch
            {
                true => await _dbContext.Orders
                            .AsNoTracking()
                            .Include(o => o.OrderItems)
                            .FirstOrDefaultAsync(o => o.Id == orderId),

                false => await _dbContext.Orders
                           .Include(o => o.OrderItems)
                           .FirstOrDefaultAsync(o => o.Id == orderId)
            };
        }

        public async Task<ICollection> GetCustomerOrdersAsync(Guid customerId)
        {
            var orders = _dbContext.Orders
                .AsNoTracking()
                .Where(o => o.CustomerId == customerId)
                .Select(o => new
                {
                    o.Id,
                    o.Status,
                    o.CreatedAt,
                    o.TotalAmount,
                    o.ShippingAddress,

                    OrderItems = o.OrderItems.Select(oi => new
                    {
                        oi.Id,
                        oi.OrderId,
                        oi.ProductId,
                        oi.Quantity,
                        oi.UnitPrice
                    })
                });

            return await orders.ToListAsync();
        }

        public async Task<Guid> AddOrderAsync(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            return order.Id;
        }

        public async Task<bool> DeleteOrderAsync(Guid orderId)
        {
            var rowsAffected = await _dbContext.Orders
                .Where(o => o.Id == orderId)
                .ExecuteDeleteAsync();

            return rowsAffected > 0;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
