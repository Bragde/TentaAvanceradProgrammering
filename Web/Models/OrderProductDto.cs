using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class OrderProductDto
    {
        public CatalogItemDto Product { get; set; }
        public int Amount { get; set; }
    }
}
