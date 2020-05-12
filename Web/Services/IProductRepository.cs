using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Services
{
    public interface IProductRepository
    {
        IEnumerable<Product> AllProducts { get; }

        Product GetProductById(int id);
    }
}
