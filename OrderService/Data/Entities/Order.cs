using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Data.Entities
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public DateTime OrderPlaced { get; set; }
        public decimal OrderTotal { get; set; }

    }
}
