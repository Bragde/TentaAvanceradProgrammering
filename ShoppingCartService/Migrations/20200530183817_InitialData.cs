using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingCartService.Migrations
{
    public partial class InitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ShoppingCartItems",
                columns: new[] { "ShoppingCartItemId", "Amount", "CatalogItemId", "ShoppingCartId" },
                values: new object[] { new Guid("90d6da79-e0e2-4ba8-bf61-2d94d90df811"), 1, new Guid("90d6da79-e0e2-4ba8-bf61-2d94d90df801"), new Guid("90d6da79-e0e2-4ba8-bf61-2d94d90df810") });

            migrationBuilder.InsertData(
                table: "ShoppingCartItems",
                columns: new[] { "ShoppingCartItemId", "Amount", "CatalogItemId", "ShoppingCartId" },
                values: new object[] { new Guid("90d6da79-e0e2-4ba8-bf61-2d94d90df812"), 1, new Guid("90d6da79-e0e2-4ba8-bf61-2d94d90df802"), new Guid("90d6da79-e0e2-4ba8-bf61-2d94d90df810") });
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
