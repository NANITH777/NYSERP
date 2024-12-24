using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NYS_ERP.Migrations
{
    /// <inheritdoc />
    public partial class RotaAna : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RotaAnas",
                columns: table => new
                {
                    COMCODE = table.Column<string>(type: "nvarchar(4)", nullable: false),
                    ROTDOCTYPE = table.Column<string>(type: "nvarchar(4)", nullable: false),
                    ROTDOCNUM = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ROTDOCFROM = table.Column<DateTime>(type: "DATE", nullable: false),
                    ROTDOCUNTIL = table.Column<DateTime>(type: "DATE", nullable: false),
                    MATDOCTYPE = table.Column<string>(type: "nvarchar(4)", nullable: false),
                    MATDOCNUM = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OPRNUM = table.Column<int>(type: "int", nullable: false),
                    BOMDOCTYPE = table.Column<string>(type: "nvarchar(4)", nullable: false),
                    BOMDOCNUM = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CONTENTNUM = table.Column<int>(type: "int", nullable: false),
                    QUANTITY = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    ISDELETED = table.Column<bool>(type: "bit", nullable: false),
                    ISPASSIVE = table.Column<bool>(type: "bit", nullable: false),
                    DRAWNUM = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    WCMDOCTYPE = table.Column<string>(type: "nvarchar(4)", nullable: false),
                    WCMDOCNUM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OPRDOCTYPE = table.Column<string>(type: "nvarchar(4)", nullable: false),
                    SETUPTIME = table.Column<decimal>(type: "decimal(3,2)", precision: 3, scale: 2, nullable: true),
                    MACHINETIME = table.Column<decimal>(type: "decimal(3,2)", precision: 3, scale: 2, nullable: true),
                    LABOURTIME = table.Column<decimal>(type: "decimal(3,2)", precision: 3, scale: 2, nullable: true),
                    COMPONENT = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    COMPONENT_QUANTITY = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RotaAnas", x => new { x.COMCODE, x.ROTDOCTYPE, x.ROTDOCNUM, x.ROTDOCFROM, x.ROTDOCUNTIL, x.MATDOCTYPE, x.MATDOCNUM, x.OPRNUM, x.BOMDOCTYPE, x.BOMDOCNUM, x.CONTENTNUM });
                    table.ForeignKey(
                        name: "FK_RotaAnas_BOMs_BOMDOCTYPE",
                        column: x => x.BOMDOCTYPE,
                        principalTable: "BOMs",
                        principalColumn: "BOMDOCTYPE",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RotaAnas_Companies_COMCODE",
                        column: x => x.COMCODE,
                        principalTable: "Companies",
                        principalColumn: "COMCODE",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RotaAnas_MaterialTypes_MATDOCTYPE",
                        column: x => x.MATDOCTYPE,
                        principalTable: "MaterialTypes",
                        principalColumn: "MATDOCTYPE",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RotaAnas_Operations_OPRDOCTYPE",
                        column: x => x.OPRDOCTYPE,
                        principalTable: "Operations",
                        principalColumn: "OPRDOCTYPE",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RotaAnas_Rotas_ROTDOCTYPE",
                        column: x => x.ROTDOCTYPE,
                        principalTable: "Rotas",
                        principalColumn: "ROTDOCTYPE",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RotaAnas_WorkCenters_WCMDOCTYPE",
                        column: x => x.WCMDOCTYPE,
                        principalTable: "WorkCenters",
                        principalColumn: "WCMDOCTYPE",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RotaAnas_BOMDOCTYPE",
                table: "RotaAnas",
                column: "BOMDOCTYPE");

            migrationBuilder.CreateIndex(
                name: "IX_RotaAnas_MATDOCTYPE",
                table: "RotaAnas",
                column: "MATDOCTYPE");

            migrationBuilder.CreateIndex(
                name: "IX_RotaAnas_OPRDOCTYPE",
                table: "RotaAnas",
                column: "OPRDOCTYPE");

            migrationBuilder.CreateIndex(
                name: "IX_RotaAnas_ROTDOCTYPE",
                table: "RotaAnas",
                column: "ROTDOCTYPE");

            migrationBuilder.CreateIndex(
                name: "IX_RotaAnas_WCMDOCTYPE",
                table: "RotaAnas",
                column: "WCMDOCTYPE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RotaAnas");
        }
    }
}
