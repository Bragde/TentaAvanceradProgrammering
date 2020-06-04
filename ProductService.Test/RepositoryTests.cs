using CatalogService.Data.Context;
using CatalogService.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace CatalogService.Test
{
    public class RepositoryTest
    {
        private readonly ICatalogRepository _repository;
        public RepositoryTest()
        {
            _repository = GetInMemoryCatalogRepository();
        }

        [Fact]
        public void GetCatalogItemById()
        {
            var sut = _repository.GetById(Guid.Parse("90d6da79-e0e2-4ba8-bf61-2d94d90df801"));

            Assert.Equal("90d6da79-e0e2-4ba8-bf61-2d94d90df801", sut.Id.ToString());
        }

        [Fact]
        public void GetAllCatalogItems()
        {
            var sut = _repository.GettAll();

            Assert.Equal(4, sut.Count);
        }

        private ICatalogRepository GetInMemoryCatalogRepository()
        {
            // Some default objects are seeded in the CatalogDbContext/OnModelCreating

            DbContextOptions<CatalogDbContext> options;
            var builder = new DbContextOptionsBuilder<CatalogDbContext>();
            builder.UseInMemoryDatabase(databaseName: "CatalogServiceTestDatabase");
            options = builder.Options;
            CatalogDbContext catalogDbContext = new CatalogDbContext(options);
            catalogDbContext.Database.EnsureDeleted();
            catalogDbContext.Database.EnsureCreated();

            return new CatalogRepository(catalogDbContext);
        }
    }
}
