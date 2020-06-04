using OrderService.Data.Context;
using OrderService.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;
using OrderService.Data.Entities;
using System.Collections.Generic;

namespace OrderService.Test
{
    public class RepositoryTests
    {
        private readonly IOrderRepository _repository;
        public RepositoryTests()
        {
            _repository = GetInMemoryCatalogRepository();
        }

        [Fact(Skip = "Cant evaluate result course get order function is not implemented")]
        public void CreateOrder()
        {
            var order = new Order
            {
                OrderId = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f0101"),
                OrderPlaced = new DateTime(2020, 06, 03),
                OrderTotal = 49.99M,
                UserId = "d28888e9-2ba9-473a-a40f-e38cb54f0201"
            };
            var orderProducts = new List<OrderProduct>
            {
                new OrderProduct{ OrderId = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f0101"), OrderProductId = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f0301"), Title = "Arizona Sunshine", Price = 39.99M, Amount = 1 },
                new OrderProduct{ OrderId = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f0101"), OrderProductId = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f0302"), Title = "Ben and Ed - Blood Party", Price = 14.99M, Amount = 2 }
            };

            _repository.CreateOrder(order, orderProducts);

            // Have no function yet to get order
        }

        private IOrderRepository GetInMemoryCatalogRepository()
        {
            DbContextOptions<OrderDbContext> options;
            var builder = new DbContextOptionsBuilder<OrderDbContext>();
            builder.UseInMemoryDatabase(databaseName: "CatalogServiceTestDatabase");
            options = builder.Options;
            OrderDbContext context = new OrderDbContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return new OrderRepository(context);
        }
    }
}
