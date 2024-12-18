using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NYS_ERP.Migrations
{
    /// <inheritdoc />
    public partial class UnitTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    UNITCODE = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    UNITTEXT = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    ISMAINUNIT = table.Column<int>(type: "int", nullable: false),
                    MAINUNITCODE = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    COMCODE = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.UNITCODE);
                    table.ForeignKey(
                        name: "FK_Units_Companies_COMCODE",
                        column: x => x.COMCODE,
                        principalTable: "Companies",
                        principalColumn: "COMCODE",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Units_COMCODE",
                table: "Units",
                column: "COMCODE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Units");
        }
    }
}
