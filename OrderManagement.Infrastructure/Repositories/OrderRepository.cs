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

        /* In all cases, we need to retrofit nested entities into the Order class. 
         * But there are cases when we just output a list, and there are cases when 
         * we make changes. That's why there is a second argument responsible for tracking */
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

                    // Without select, there will be an infinite recursion on the Order property of OrderItem
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

        /* ExecuteDeleteAsync() returns how many rows were deleted from the database. 
         * In case when no data is found, the method will return 0 deleted rows. 
         * Thus, we do not make additional checks and only 1 query to the database 
         * is made for deletion. */
        public async Task<bool> DeleteOrderAsync(Guid orderId)
        {
            var rowsAffected = await _dbContext.Orders
                .Where(o => o.Id == orderId)
                .ExecuteDeleteAsync();

            return rowsAffected > 0;
        }

        /* In the service, when manipulating data, in particular OrderItem, 
         * we do not have direct access to the database context. 
         * That's why we need a double of SaveChangesAsync() function, 
         * which will be available to the service. */
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
