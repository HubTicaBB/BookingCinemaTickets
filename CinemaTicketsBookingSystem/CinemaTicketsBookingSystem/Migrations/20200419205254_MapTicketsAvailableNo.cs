using Microsoft.EntityFrameworkCore.Migrations;

namespace CinemaTicketsBookingSystem.Migrations
{
    public partial class MapTicketsAvailableNo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TicketsAvailable",
                table: "Showtimes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketsAvailable",
                table: "Showtimes");
        }
    }
}
