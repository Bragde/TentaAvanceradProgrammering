using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Data
{
    public class InMemoryOrders
    {
        private static InMemoryOrders _instance;

        private InMemoryOrders()
        {
            Orders = new List<Order>();
        }

        public List<Order> Orders { get; set; }

        public static InMemoryOrders GetInstance()
        {
            if (_instance == null)
            {
                _instance = new InMemoryOrders();
            }
            return _instance;
        }
    }
}
