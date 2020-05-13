using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Data;
using Web.Models;

namespace Web.Services
{
    public class OrderRepository : IOrderRepository
    {
        private readonly InMemoryOrders _inMemoryOrders;
        private readonly ShoppingCart _shoppingCart;

        public OrderRepository(InMemoryOrders inMemoryOrders, ShoppingCart shoppingCart)
        {
            _inMemoryOrders = inMemoryOrders;
            _shoppingCart = shoppingCart;
        }

        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;

            var shoppingCartItems = _shoppingCart.ShoppingCartItems;
            order.OrderTotal = _shoppingCart.GetShoppingCartTotal();

            order.OrderDetails = new List<OrderDetail>();

            foreach (var shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {                    
                    Product = shoppingCartItem.Product,                    
                    Amount = shoppingCartItem.Amount,
                };

                order.OrderDetails.Add(orderDetail);
            }

            _inMemoryOrders.Orders.Add(order);
        }
    }
}
