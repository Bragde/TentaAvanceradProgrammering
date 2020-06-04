using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.ViewModels
{
    public class ShoppingCartViewModel
    {
        public ShoppingCartViewModel()
        {
            ShoppingCartItems = new List<ShoppingCartItem>();
        }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        public decimal ShoppingCartTotal { get; set; }
    }
}
