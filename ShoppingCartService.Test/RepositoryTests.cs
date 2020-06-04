using Microsoft.EntityFrameworkCore;
using ShoppingCartService.Data.Context;
using ShoppingCartService.Models;
using ShoppingCartService.Repositories;
using System;
using System.Linq;
using Xunit;

namespace ShoppingCartService.Test
{
    public class RepositoryTests
    {
        private readonly IShoppingCartRepository _repository;
        public RepositoryTests()
        {
            _repository = GetInMemoryCatalogRepository();
        }

        [Fact]
        public void GetShoppingCartItemsByUserId()
        {
            var sut = _repository.GetShoppingCartItemsByUserId("d28888e9-2ba9-473a-a40f-e38cb54f0201");

            Assert.Equal(2, sut.Count);          
        }

        [Fact]
        public void GetItemFromShoppingCart()
        {
            var item = new ShoppingCartItem
            {
                CatalogItemId = new Guid("90d6da79-e0e2-4ba8-bf61-2d94d90df801"),
                UserId = "d28888e9-2ba9-473a-a40f-e38cb54f0201"
            };

            var sut = _repository.GetItemFromShoppingCart(item);

            Assert.Equal(Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f0101"), sut.ShoppingCartItemId);
            Assert.Equal(1, sut.Amount);
        }

        [Fact]
        public void AddItemToShoppingCart()
        {
            var itemToAdd = new ShoppingCartItem { ShoppingCartItemId = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f0103"), UserId = "d28888e9-2ba9-473a-a40f-e38cb54f0201", CatalogItemId = new Guid("90d6da79-e0e2-4ba8-bf61-2d94d90df803"), Amount = 3 };
            _repository.AddItemToShoppingCart(itemToAdd);
            _repository.Save();

            var itemRetrived = _repository.GetItemFromShoppingCart(itemToAdd);

            Assert.Equal(itemToAdd.CatalogItemId, itemRetrived.CatalogItemId);
            Assert.Equal(itemToAdd.UserId, itemRetrived.UserId);
            Assert.Equal(itemToAdd.Amount, itemRetrived.Amount);
        }

        [Fact]
        public void DeleteShoppingCart()
        {
            var userId = "d28888e9-2ba9-473a-a40f-e38cb54f0201";
            var itemToAdd = new ShoppingCartItem { ShoppingCartItemId = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f0104"), UserId = userId, CatalogItemId = new Guid("90d6da79-e0e2-4ba8-bf61-2d94d90df803"), Amount = 3 };
            _repository.AddItemToShoppingCart(itemToAdd);
            _repository.Save();            

            _repository.DeleteShoppingCart(userId);
            _repository.Save();

            var itemsRetrived = _repository.GetShoppingCartItemsByUserId(userId);

            Assert.Empty(itemsRetrived);
        }

        private IShoppingCartRepository GetInMemoryCatalogRepository()
        {
            DbContextOptions<ShoppingCartDbContext> options;
            var builder = new DbContextOptionsBuilder<ShoppingCartDbContext>();
            builder.UseInMemoryDatabase(databaseName: "CatalogServiceTestDatabase");
            options = builder.Options;
            ShoppingCartDbContext context = new ShoppingCartDbContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            Seed(context);

            return new ShoppingCartRepository(context);
        }

        private void Seed(ShoppingCartDbContext context)
        {
            var item_1 = new ShoppingCartItem { ShoppingCartItemId = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f0101"), UserId = "d28888e9-2ba9-473a-a40f-e38cb54f0201", CatalogItemId = new Guid("90d6da79-e0e2-4ba8-bf61-2d94d90df801"), Amount = 1 };
            var item_2 = new ShoppingCartItem { ShoppingCartItemId = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f0102"), UserId = "d28888e9-2ba9-473a-a40f-e38cb54f0201", CatalogItemId = new Guid("bfce20c2-22fa-4f73-bc07-54956cb52a02"), Amount = 2 };
            context.AddRange(item_1, item_2);
            context.SaveChanges();
        }
    }
}
