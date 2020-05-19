using CatalogService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.ViewModels
{
    public class ProductViewModel
    {
        public IEnumerable<CatalogItemDto> Products { get; set; }
    }
}
