using System.Collections.Generic;
using Web.Models;

namespace Web.ViewModels
{
    public class ProductViewModel
    {
        public IEnumerable<CatalogItemDto> Products { get; set; }
    }
}
