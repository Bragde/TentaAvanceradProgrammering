using CatalogService.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace CatalogService.Data.Context
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext()
        {

        }
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options)
            : base(options)
        {
        }

        public DbSet<CatalogItem> CatalogItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CatalogItem>().HasData(
                new CatalogItem { Id = new Guid("90d6da79-e0e2-4ba8-bf61-2d94d90df801"), Title = "Arizona Sunshine", Price = 39.99M, ImageUrl = "/images/arizona_sunshine.jpg" },
                new CatalogItem { Id = new Guid("bfce20c2-22fa-4f73-bc07-54956cb52a02"), Title = "Ben and Ed - Blood Party", Price = 14.99M, ImageUrl = "/images/ben_and_ed_blood_party.jpg" },
                new CatalogItem { Id = new Guid("cc1ce8fc-8ff9-4b0c-ad18-a38c2fa7de03"), Title = "BattleBlock Theater", Price = 14.99M, ImageUrl = "/images/battleblock_theater.jpg" },
                new CatalogItem { Id = new Guid("933f7f9b-9960-4e2a-9450-e54e00a00c04"), Title = "Castle Crashers", Price = 11.99M, ImageUrl = "/images/castle_crashers.jpg" }
            );
        }
    }
}
