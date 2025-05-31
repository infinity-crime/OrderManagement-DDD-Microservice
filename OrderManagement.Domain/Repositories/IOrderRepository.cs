using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task<bool> DeleteByIdAsync(Guid orderId);
        Task<Order?> GetByIdAsync(Guid orderId);
    }
}
