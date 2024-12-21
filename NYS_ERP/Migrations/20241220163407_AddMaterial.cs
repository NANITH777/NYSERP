using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NYS_ERP.Migrations
{
    /// <inheritdoc />
    public partial class AddMaterial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    COMCODE = table.Column<string>(type: "nvarchar(4)", nullable: false),
                    MATDOCTYPE = table.Column<string>(type: "nvarchar(4)", nullable: false),
                    MATDOCNUM = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MATDOCFROM = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MATDOCUNTIL = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LANCODE = table.Column<string>(type: "nvarchar(3)", nullable: false),
                    SUPPLYTYPE = table.Column<int>(type: "int", nullable: false),
                    STUNIT = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    NETWEIGHT = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    NWUNIT = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    BRUTWEIGHT = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    BWUNIT = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    ISBOM = table.Column<int>(type: "int", nullable: false),
                    BOMDOCTYPE = table.Column<string>(type: "nvarchar(4)", nullable: false),
                    BOMDOCNUM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISROUTE = table.Column<int>(type: "int", nullable: false),
                    ROTDOCTYPE = table.Column<string>(type: "nvarchar(4)", nullable: false),
                    ROTDOCNUM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISDELETED = table.Column<int>(type: "int", nullable: false),
                    ISPASSIVE = table.Column<int>(type: "int", nullable: false),
                    MATSTEXT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MATLTEXT = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => new { x.COMCODE, x.MATDOCTYPE, x.MATDOCNUM, x.MATDOCFROM, x.MATDOCUNTIL, x.LANCODE });
                    table.ForeignKey(
                        name: "FK_Materials_BOMs_BOMDOCTYPE",
                        column: x => x.BOMDOCTYPE,
                        principalTable: "BOMs",
                        principalColumn: "BOMDOCTYPE",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Materials_Companies_COMCODE",
                        column: x => x.COMCODE,
                        principalTable: "Companies",
                        principalColumn: "COMCODE",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Materials_Languages_LANCODE",
                        column: x => x.LANCODE,
                        principalTable: "Languages",
                        principalColumn: "LANCODE",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Materials_MaterialTypes_MATDOCTYPE",
                        column: x => x.MATDOCTYPE,
                        principalTable: "MaterialTypes",
                        principalColumn: "MATDOCTYPE",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Materials_Rotas_ROTDOCTYPE",
                        column: x => x.ROTDOCTYPE,
                        principalTable: "Rotas",
                        principalColumn: "ROTDOCTYPE",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Materials_BOMDOCTYPE",
                table: "Materials",
                column: "BOMDOCTYPE");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_LANCODE",
                table: "Materials",
                column: "LANCODE");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_MATDOCTYPE",
                table: "Materials",
                column: "MATDOCTYPE");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_ROTDOCTYPE",
                table: "Materials",
                column: "ROTDOCTYPE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Materials");
        }
    }
}
