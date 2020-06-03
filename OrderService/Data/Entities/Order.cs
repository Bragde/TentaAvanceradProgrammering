using OrderService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Data.Entities
{
    public class Order
    {
        public Order()
        {

        }

        public Order(OrderDto order)
        {
            UserId = order.UserId;
            OrderPlaced = order.OrderPlaced;
            OrderTotal = order.OrderTotal;
        }

        public Guid OrderId { get; set; }
        public string UserId { get; set; }
        public DateTime OrderPlaced { get; set; }
        public decimal OrderTotal { get; set; }

    }
}
