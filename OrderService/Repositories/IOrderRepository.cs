using OrderService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Repositories
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order, IEnumerable<OrderProduct> orderProducts);
        Task<bool> Save();
    }
}
