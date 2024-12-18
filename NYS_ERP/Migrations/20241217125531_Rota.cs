using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NYS_ERP.Migrations
{
    /// <inheritdoc />
    public partial class Rota : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rotas",
                columns: table => new
                {
                    DOCTYPE = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    DOCTYPETEXT = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    ISPASSIVE = table.Column<int>(type: "int", nullable: false),
                    COMCODE = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rotas", x => x.DOCTYPE);
                    table.ForeignKey(
                        name: "FK_Rotas_Companies_COMCODE",
                        column: x => x.COMCODE,
                        principalTable: "Companies",
                        principalColumn: "COMCODE",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rotas_COMCODE",
                table: "Rotas",
                column: "COMCODE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rotas");
        }
    }
}
