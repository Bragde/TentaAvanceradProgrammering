﻿// <auto-generated />
using System;
using CatalogService.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CatalogService.Migrations
{
    [DbContext(typeof(CatalogDbContext))]
    partial class CatalogDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CatalogService.Data.Entities.CatalogItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CatalogItems");

                    b.HasData(
                        new
                        {
                            Id = new Guid("90d6da79-e0e2-4ba8-bf61-2d94d90df801"),
                            ImageUrl = "/images/arizona_sunshine.jpg",
                            Price = 39.99m,
                            Title = "Arizona Sunshine"
                        },
                        new
                        {
                            Id = new Guid("bfce20c2-22fa-4f73-bc07-54956cb52a02"),
                            ImageUrl = "/images/ben_and_ed_blood_party.jpg",
                            Price = 14.99m,
                            Title = "Ben and Ed - Blood Party"
                        },
                        new
                        {
                            Id = new Guid("cc1ce8fc-8ff9-4b0c-ad18-a38c2fa7de03"),
                            ImageUrl = "/images/battleblock_theater.jpg",
                            Price = 14.99m,
                            Title = "BattleBlock Theater"
                        },
                        new
                        {
                            Id = new Guid("933f7f9b-9960-4e2a-9450-e54e00a00c04"),
                            ImageUrl = "/images/castle_crashers.jpg",
                            Price = 11.99m,
                            Title = "Castle Crashers"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
