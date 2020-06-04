using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingCartService.Migrations
{
    public partial class UserIdAsString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ShoppingCartItems",
                keyColumn: "ShoppingCartItemId",
                keyValue: new Guid("90d6da79-e0e2-4ba8-bf61-2d94d90df811"));

            migrationBuilder.DeleteData(
                table: "ShoppingCartItems",
                keyColumn: "ShoppingCartItemId",
                keyValue: new Guid("90d6da79-e0e2-4ba8-bf61-2d94d90df812"));

            migrationBuilder.DropColumn(
                name: "ShoppingCartId",
                table: "ShoppingCartItems");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ShoppingCartItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ShoppingCartItems");

            migrationBuilder.AddColumn<Guid>(
                name: "ShoppingCartId",
                table: "ShoppingCartItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "ShoppingCartItems",
                columns: new[] { "ShoppingCartItemId", "Amount", "CatalogItemId", "ShoppingCartId" },
                values: new object[] { new Guid("90d6da79-e0e2-4ba8-bf61-2d94d90df811"), 1, new Guid("90d6da79-e0e2-4ba8-bf61-2d94d90df801"), new Guid("90d6da79-e0e2-4ba8-bf61-2d94d90df810") });

            migrationBuilder.InsertData(
                table: "ShoppingCartItems",
                columns: new[] { "ShoppingCartItemId", "Amount", "CatalogItemId", "ShoppingCartId" },
                values: new object[] { new Guid("90d6da79-e0e2-4ba8-bf61-2d94d90df812"), 1, new Guid("90d6da79-e0e2-4ba8-bf61-2d94d90df802"), new Guid("90d6da79-e0e2-4ba8-bf61-2d94d90df810") });
        }
    }
}
