using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CatalogService.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CatalogItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogItems", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CatalogItems",
                columns: new[] { "Id", "ImageUrl", "Price", "Title" },
                values: new object[,]
                {
                    { new Guid("90d6da79-e0e2-4ba8-bf61-2d94d90df801"), "/images/arizona_sunshine.jpg", 39.99m, "Arizona Sunshine" },
                    { new Guid("bfce20c2-22fa-4f73-bc07-54956cb52a02"), "/images/ben_and_ed_blood_party.jpg", 14.99m, "Ben and Ed - Blood Party" },
                    { new Guid("cc1ce8fc-8ff9-4b0c-ad18-a38c2fa7de03"), "/images/battleblock_theater.jpg", 14.99m, "BattleBlock Theater" },
                    { new Guid("933f7f9b-9960-4e2a-9450-e54e00a00c04"), "/images/castle_crashers.jpg", 11.99m, "Castle Crashers" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatalogItems");
        }
    }
}
