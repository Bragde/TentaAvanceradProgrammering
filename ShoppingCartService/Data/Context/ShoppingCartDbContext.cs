using Microsoft.EntityFrameworkCore;
using ShoppingCartService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCartService.Data.Context
{
    public class ShoppingCartDbContext : DbContext
    {
        public ShoppingCartDbContext(DbContextOptions<ShoppingCartDbContext> options)
            : base(options)
        {
        }

        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ShoppingCartItem>().HasData(
                new ShoppingCartItem { ShoppingCartId = new Guid("90d6da79-e0e2-4ba8-bf61-2d94d90df810"), ShoppingCartItemId = new Guid("90d6da79-e0e2-4ba8-bf61-2d94d90df811"), CatalogItemId = new Guid("90d6da79-e0e2-4ba8-bf61-2d94d90df801"), Amount = 1 },
                new ShoppingCartItem { ShoppingCartId = new Guid("90d6da79-e0e2-4ba8-bf61-2d94d90df810"), ShoppingCartItemId = new Guid("90d6da79-e0e2-4ba8-bf61-2d94d90df812"), CatalogItemId = new Guid("90d6da79-e0e2-4ba8-bf61-2d94d90df802"), Amount = 1 }           
            );
        }
    }
}
