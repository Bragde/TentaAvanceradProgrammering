using ProductService.Models;
using ProductService.Repositories;
using System;
using Xunit;

namespace ProductService.Test
{
    public class RepositoryTests
    {
        [Fact]
        public void GetProductById_Returns_Product()
        {
            var productRepository = new ProductRepository();
            var product = productRepository.GetById(Guid.Empty);

            Assert.IsType<Product>(product);
        }
    }
}
