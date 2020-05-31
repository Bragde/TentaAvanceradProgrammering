using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class ShoppingCartItemDto
    {
        public Guid ShoppingCartItemId { get; set; }
        public Guid CatalogItemId { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }
    }
}
