using ProductService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public Product GetById(Guid id)
        {
            return new Product();
        }
    }
}
