using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.ViewModels
{
    public class OrderViewModel
    {
        public OrderDto Order { get; set; }
        public ApplicationUser User { get; set; }
    }
}
