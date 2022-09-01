using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TubimProject.UI.Migrations
{
    public partial class InitialTablesssllllopo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UT_OLAY",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KurumOlayId = table.Column<long>(type: "bigint", nullable: true),
                    KurumRef = table.Column<int>(type: "int", nullable: false),
                    HedefUlkesi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KacakcilikYontemi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MudahaleEdenBirim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OlayNumarasi = table.Column<long>(type: "bigint", nullable: true),
                    OlayTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SistemNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Durum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UT_OLAY", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UT_MADDE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OlayId = table.Column<long>(type: "bigint", nullable: false),
                    JandarmaMaddeId = table.Column<long>(type: "bigint", nullable: true),
                    EgmMaddeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OlayNumarasi = table.Column<long>(type: "bigint", nullable: false),
                    AnaTuru = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AltTuru = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Birimi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Miktari = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MalzemeNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KaynakUlke = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Durum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServisAnaTuru = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UT_MADDE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UT_MADDE_UT_OLAY_OlayId",
                        column: x => x.OlayId,
                        principalTable: "UT_OLAY",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UT_OLAYDETAY",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OlayId = table.Column<long>(type: "bigint", nullable: true),
                    OlayBeldeKoy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OlayIlcesi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OlayIli = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OlayMahalle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OlayEnlem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OlayBoylam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OlayBolgesi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UT_OLAYDETAY", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UT_OLAYDETAY_UT_OLAY_OlayId",
                        column: x => x.OlayId,
                        principalTable: "UT_OLAY",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UT_SUCTANIM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OlayId = table.Column<long>(type: "bigint", nullable: false),
                    SucTanimi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UzunSucTanimi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UT_SUCTANIM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UT_SUCTANIM_UT_OLAY_OlayId",
                        column: x => x.OlayId,
                        principalTable: "UT_OLAY",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UT_MADDE_OlayId",
                table: "UT_MADDE",
                column: "OlayId");

            migrationBuilder.CreateIndex(
                name: "IX_UT_OLAYDETAY_OlayId",
                table: "UT_OLAYDETAY",
                column: "OlayId");

            migrationBuilder.CreateIndex(
                name: "IX_UT_SUCTANIM_OlayId",
                table: "UT_SUCTANIM",
                column: "OlayId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UT_MADDE");

            migrationBuilder.DropTable(
                name: "UT_OLAYDETAY");

            migrationBuilder.DropTable(
                name: "UT_SUCTANIM");

            migrationBuilder.DropTable(
                name: "UT_OLAY");
        }
    }
}
