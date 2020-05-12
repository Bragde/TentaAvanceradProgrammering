using System.Collections.Generic;
using System.Linq;
using Web.Models;
using Web.Services;

namespace Web.Data
{
    public class MockProductRepository : IProductRepository
    {
        public IEnumerable<Product> AllProducts =>
            new List<Product>
            {
                new Product { Id = 1, Name = "Arizona Sunshine", Price = 39.99M },
                new Product { Id = 2, Name = "Ben and Ed - Blood Party", Price = 14.99M },
                new Product { Id = 3, Name = "BattleBlock Theater", Price = 14.99M },
                new Product { Id = 4, Name = "Castle Crashers", Price = 11.99M }
            };

        public Product GetProductById(int id)
        {
            return AllProducts.FirstOrDefault(p => p.Id == id);
        }
    }
}
