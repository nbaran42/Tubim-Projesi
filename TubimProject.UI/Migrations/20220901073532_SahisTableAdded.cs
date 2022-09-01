using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TubimProject.UI.Migrations
{
    public partial class SahisTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UT_SAHISLAR",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OlayId = table.Column<int>(type: "int", nullable: false),
                    SupheliId = table.Column<int>(type: "int", nullable: true),
                    OlayNumarasi = table.Column<long>(type: "bigint", nullable: false),
                    SupheliNo = table.Column<long>(type: "bigint", nullable: true),
                    Cinsiyet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DogumTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MedeniDurumu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Meslek = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TCKimlikNoPasaportNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Uyrugu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Durum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UT_SAHISLAR", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UT_SAHISLAR");
        }
    }
}
