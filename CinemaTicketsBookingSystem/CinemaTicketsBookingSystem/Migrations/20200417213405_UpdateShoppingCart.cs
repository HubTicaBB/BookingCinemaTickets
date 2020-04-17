using Microsoft.EntityFrameworkCore.Migrations;

namespace CinemaTicketsBookingSystem.Migrations
{
    public partial class UpdateShoppingCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsProcessed",
                table: "ShoppingCarts");

            migrationBuilder.AddColumn<bool>(
                name: "IsPending",
                table: "ShoppingCarts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPending",
                table: "ShoppingCarts");

            migrationBuilder.AddColumn<bool>(
                name: "IsProcessed",
                table: "ShoppingCarts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
