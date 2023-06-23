using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TMarket.Persistence.Migrations
{
    public partial class ShoppingCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CartDTO",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    InsertDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartDTO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartDTO_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartProductDTO",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false),
                    CartId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartProductDTO", x => new { x.ProductId, x.CartId });
                    table.ForeignKey(
                        name: "FK_CartProductDTO_CartDTO_CartId",
                        column: x => x.CartId,
                        principalTable: "CartDTO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartProductDTO_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartDTO_UserId",
                table: "CartDTO",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CartProductDTO_CartId",
                table: "CartProductDTO",
                column: "CartId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartProductDTO");

            migrationBuilder.DropTable(
                name: "CartDTO");
        }
    }
}
