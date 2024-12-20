using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NYS_ERP.Migrations
{
    /// <inheritdoc />
    public partial class CostCenterAna : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CostCenterAnas",
                columns: table => new
                {
                    COMCODE = table.Column<string>(type: "nvarchar(4)", nullable: false),
                    CCMDOCTYPE = table.Column<string>(type: "nvarchar(4)", nullable: false),
                    CCMDOCNUM = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    CCMDOCFROM = table.Column<DateTime>(type: "DATE", nullable: false),
                    CCMDOCUNTIL = table.Column<DateTime>(type: "DATE", nullable: false),
                    LANCODE = table.Column<string>(type: "nvarchar(3)", nullable: false),
                    MAINCCMDOCTYPE = table.Column<string>(type: "VARCHAR(4)", maxLength: 4, nullable: false),
                    MAINCCMDOCNUM = table.Column<string>(type: "VARCHAR(25)", maxLength: 25, nullable: false),
                    ISDELETED = table.Column<bool>(type: "bit", nullable: false),
                    ISPASSIVE = table.Column<bool>(type: "bit", nullable: false),
                    CCMSTEXT = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    CCMLTEXT = table.Column<string>(type: "VARCHAR(250)", maxLength: 250, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostCenterAnas", x => new { x.COMCODE, x.CCMDOCTYPE, x.CCMDOCNUM, x.CCMDOCFROM, x.CCMDOCUNTIL, x.LANCODE });
                    table.ForeignKey(
                        name: "FK_CostCenterAnas_Companies_COMCODE",
                        column: x => x.COMCODE,
                        principalTable: "Companies",
                        principalColumn: "COMCODE",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CostCenterAnas_CostCenters_CCMDOCTYPE",
                        column: x => x.CCMDOCTYPE,
                        principalTable: "CostCenters",
                        principalColumn: "CCMDOCTYPE",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CostCenterAnas_Languages_LANCODE",
                        column: x => x.LANCODE,
                        principalTable: "Languages",
                        principalColumn: "LANCODE",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CostCenterAnas_CCMDOCTYPE",
                table: "CostCenterAnas",
                column: "CCMDOCTYPE");

            migrationBuilder.CreateIndex(
                name: "IX_CostCenterAnas_LANCODE",
                table: "CostCenterAnas",
                column: "LANCODE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CostCenterAnas");
        }
    }
}
