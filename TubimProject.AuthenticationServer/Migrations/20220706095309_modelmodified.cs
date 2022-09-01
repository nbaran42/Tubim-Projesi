using Microsoft.EntityFrameworkCore.Migrations;

namespace TubimProject.AuthenticationServer.Migrations
{
    public partial class modelmodified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KurumTelefon",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Sicil",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KurumTelefon",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sicil",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
