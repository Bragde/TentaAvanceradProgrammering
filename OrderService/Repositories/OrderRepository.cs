using OrderService.Data.Context;
using OrderService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _context;

        public OrderRepository(OrderDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void CreateOrder(Order order, IEnumerable<OrderProduct> orderProducts)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            if (orderProducts == null)
                throw new ArgumentNullException(nameof(orderProducts));

            // Create a new order
            _context.Orders.Add(order);
            _context.SaveChanges();

            // Add products to order
            foreach (var product in orderProducts)
            {
                product.OrderId = order.OrderId;
                _context.OrderProducts.Add(product);
            }
        }

        public async Task<bool> Save()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
