using ProductService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Repositories
{
    interface IProductRepository
    {
        //IEnumerable<Product> AllProducts { get; }

        Product GetById(Guid id);
        //Product GetProductByGenre(string genre);
    }
}
