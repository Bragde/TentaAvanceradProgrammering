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
                new Product { Id = 1, Name = "Arizona Sunshine", Price = 39.99M, imageUrl = "/images/arizona_sunshine.jpg" },
                new Product { Id = 2, Name = "Ben and Ed - Blood Party", Price = 14.99M, imageUrl = "/images/ben_and_ed_blood_party.jpg" },
                new Product { Id = 3, Name = "BattleBlock Theater", Price = 14.99M, imageUrl = "/images/battleblock_theater.jpg" },
                new Product { Id = 4, Name = "Castle Crashers", Price = 11.99M, imageUrl = "/images/castle_crashers.jpg" }
            };

        public Product GetProductById(int id)
        {
            return AllProducts.FirstOrDefault(p => p.Id == id);
        }
    }
}
