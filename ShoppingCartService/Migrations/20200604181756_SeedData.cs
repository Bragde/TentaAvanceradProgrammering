using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingCartService.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ShoppingCartItems",
                columns: new[] { "ShoppingCartItemId", "Amount", "CatalogItemId", "UserId" },
                values: new object[] { new Guid("90d6da79-e0e2-4ba8-bf61-2d94d90df811"), 1, new Guid("90d6da79-e0e2-4ba8-bf61-2d94d90df801"), "d28888e9-2ba9-473a-a40f-e38cb54f9b35" });

            migrationBuilder.InsertData(
                table: "ShoppingCartItems",
                columns: new[] { "ShoppingCartItemId", "Amount", "CatalogItemId", "UserId" },
                values: new object[] { new Guid("90d6da79-e0e2-4ba8-bf61-2d94d90df812"), 1, new Guid("90d6da79-e0e2-4ba8-bf61-2d94d90df802"), "d28888e9-2ba9-473a-a40f-e38cb54f9b35" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ShoppingCartItems",
                keyColumn: "ShoppingCartItemId",
                keyValue: new Guid("90d6da79-e0e2-4ba8-bf61-2d94d90df811"));

            migrationBuilder.DeleteData(
                table: "ShoppingCartItems",
                keyColumn: "ShoppingCartItemId",
                keyValue: new Guid("90d6da79-e0e2-4ba8-bf61-2d94d90df812"));
        }
    }
}
