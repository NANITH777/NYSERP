using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NYS_ERP.Migrations
{
    /// <inheritdoc />
    public partial class WC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkCenterAnas",
                columns: table => new
                {
                    COMCODE = table.Column<string>(type: "nvarchar(4)", nullable: false),
                    WCMDOCTYPE = table.Column<string>(type: "nvarchar(4)", nullable: false),
                    WCMDOCNUM = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    WCMDOCFROM = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WCMDOCUNTIL = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LANCODE = table.Column<string>(type: "nvarchar(3)", nullable: false),
                    OPRDOCTYPE = table.Column<string>(type: "nvarchar(4)", nullable: false),
                    MAINWCMDOCTYPE = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    MAINWCMDOCNUM = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CCMDOCTYPE = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    CCMDOCNUM = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    WORKTIME = table.Column<decimal>(type: "decimal(3,2)", precision: 3, scale: 2, nullable: false),
                    ISDELETED = table.Column<int>(type: "int", nullable: false),
                    ISPASSIVE = table.Column<int>(type: "int", nullable: false),
                    WCMSTEXT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    WCMLTEXT = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkCenterAnas", x => new { x.COMCODE, x.WCMDOCTYPE, x.WCMDOCNUM, x.WCMDOCFROM, x.WCMDOCUNTIL, x.LANCODE, x.OPRDOCTYPE });
                    table.ForeignKey(
                        name: "FK_WorkCenterAnas_Companies_COMCODE",
                        column: x => x.COMCODE,
                        principalTable: "Companies",
                        principalColumn: "COMCODE",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkCenterAnas_CostCenters_CCMDOCTYPE",
                        column: x => x.CCMDOCTYPE,
                        principalTable: "CostCenters",
                        principalColumn: "CCMDOCTYPE",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkCenterAnas_Languages_LANCODE",
                        column: x => x.LANCODE,
                        principalTable: "Languages",
                        principalColumn: "LANCODE",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkCenterAnas_Operations_OPRDOCTYPE",
                        column: x => x.OPRDOCTYPE,
                        principalTable: "Operations",
                        principalColumn: "OPRDOCTYPE",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkCenterAnas_WorkCenters_WCMDOCTYPE",
                        column: x => x.WCMDOCTYPE,
                        principalTable: "WorkCenters",
                        principalColumn: "WCMDOCTYPE",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkCenterAnas_CCMDOCTYPE",
                table: "WorkCenterAnas",
                column: "CCMDOCTYPE");

            migrationBuilder.CreateIndex(
                name: "IX_WorkCenterAnas_LANCODE",
                table: "WorkCenterAnas",
                column: "LANCODE");

            migrationBuilder.CreateIndex(
                name: "IX_WorkCenterAnas_OPRDOCTYPE",
                table: "WorkCenterAnas",
                column: "OPRDOCTYPE");

            migrationBuilder.CreateIndex(
                name: "IX_WorkCenterAnas_WCMDOCTYPE",
                table: "WorkCenterAnas",
                column: "WCMDOCTYPE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkCenterAnas");
        }
    }
}
