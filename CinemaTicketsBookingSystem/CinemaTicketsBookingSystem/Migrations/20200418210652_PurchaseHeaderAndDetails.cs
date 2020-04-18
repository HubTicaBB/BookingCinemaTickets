using Microsoft.EntityFrameworkCore.Migrations;

namespace CinemaTicketsBookingSystem.Migrations
{
    public partial class PurchaseHeaderAndDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Purchases",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentStatus",
                table: "Purchases",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Purchases",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Purchases",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_ApplicationUserId",
                table: "Purchases",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_AspNetUsers_ApplicationUserId",
                table: "Purchases",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_AspNetUsers_ApplicationUserId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_ApplicationUserId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Purchases");
        }
    }
}
