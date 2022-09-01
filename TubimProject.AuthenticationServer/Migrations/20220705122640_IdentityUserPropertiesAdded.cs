using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TubimProject.AuthenticationServer.Migrations
{
    public partial class IdentityUserPropertiesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Aktifmi",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CepTel",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KurumDaire",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KurumId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "KurumTelefon",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "T_Islem",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Unvan",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aktifmi",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CepTel",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "KurumDaire",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "KurumId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "KurumTelefon",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "T_Islem",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Unvan",
                table: "AspNetUsers");
        }
    }
}
