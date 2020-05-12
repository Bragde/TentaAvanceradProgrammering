using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Data
{
    public class InMemoryShoppingCartItems
    {
        private static InMemoryShoppingCartItems _instance;        

        private InMemoryShoppingCartItems()
        {
            ShoppingCartItems = new List<ShoppingCartItem>();
        }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public static InMemoryShoppingCartItems GetInstance()
        {
            if (_instance == null)
            {
                _instance = new InMemoryShoppingCartItems();
            }
            return _instance;
        }
    }
}
