using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NYS_ERP.Migrations
{
    /// <inheritdoc />
    public partial class BOMANA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NWUNIT",
                table: "Materials",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "NETWEIGHT",
                table: "Materials",
                type: "decimal(12,3)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,3)");

            migrationBuilder.AlterColumn<string>(
                name: "BWUNIT",
                table: "Materials",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "BRUTWEIGHT",
                table: "Materials",
                type: "decimal(12,3)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,3)");

            migrationBuilder.CreateTable(
                name: "BOMAnas",
                columns: table => new
                {
                    COMCODE = table.Column<string>(type: "nvarchar(4)", nullable: false),
                    BOMDOCTYPE = table.Column<string>(type: "nvarchar(4)", nullable: false),
                    BOMDOCNUM = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BOMDOCFROM = table.Column<DateTime>(type: "DATE", nullable: false),
                    BOMDOCUNTIL = table.Column<DateTime>(type: "DATE", nullable: false),
                    MATDOCTYPE = table.Column<string>(type: "nvarchar(4)", nullable: false),
                    MATDOCNUM = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CONTENTNUM = table.Column<int>(type: "int", nullable: false),
                    QUANTITY = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    ISDELETED = table.Column<int>(type: "int", nullable: true),
                    ISPASSIVE = table.Column<int>(type: "int", nullable: true),
                    DRAWNUM = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    COMPONENT = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    COMPBOMDOCTYPE = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    COMPBOMDOCNUM = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    COMPONENT_QUANTITY = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BOMAnas", x => new { x.COMCODE, x.BOMDOCTYPE, x.BOMDOCNUM, x.BOMDOCFROM, x.BOMDOCUNTIL, x.MATDOCTYPE, x.MATDOCNUM, x.CONTENTNUM });
                    table.ForeignKey(
                        name: "FK_BOMAnas_BOMs_BOMDOCTYPE",
                        column: x => x.BOMDOCTYPE,
                        principalTable: "BOMs",
                        principalColumn: "BOMDOCTYPE",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BOMAnas_Companies_COMCODE",
                        column: x => x.COMCODE,
                        principalTable: "Companies",
                        principalColumn: "COMCODE",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BOMAnas_MaterialTypes_MATDOCTYPE",
                        column: x => x.MATDOCTYPE,
                        principalTable: "MaterialTypes",
                        principalColumn: "MATDOCTYPE",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BOMAnas_BOMDOCTYPE",
                table: "BOMAnas",
                column: "BOMDOCTYPE");

            migrationBuilder.CreateIndex(
                name: "IX_BOMAnas_MATDOCTYPE",
                table: "BOMAnas",
                column: "MATDOCTYPE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BOMAnas");

            migrationBuilder.AlterColumn<string>(
                name: "NWUNIT",
                table: "Materials",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "NETWEIGHT",
                table: "Materials",
                type: "decimal(12,3)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,3)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BWUNIT",
                table: "Materials",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "BRUTWEIGHT",
                table: "Materials",
                type: "decimal(12,3)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,3)",
                oldNullable: true);
        }
    }
}
