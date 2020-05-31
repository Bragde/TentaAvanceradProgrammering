using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Data.Entities
{
    public class OrderProduct
    {
        public Guid OrderProductId { get; set; }
        public Guid OrderId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
    }
}
