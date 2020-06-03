using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }
        public string UserId { get; set; }
        public DateTime OrderPlaced { get; set; }
        public decimal OrderTotal { get; set; }
        public IEnumerable<OrderProductDto> OrderedProducts { get; set; }

    }
}
