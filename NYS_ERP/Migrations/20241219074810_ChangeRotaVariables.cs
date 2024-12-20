using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NYS_ERP.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRotaVariables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DOCTYPETEXT",
                table: "Rotas",
                newName: "ROTDOCNUM");

            migrationBuilder.RenameColumn(
                name: "DOCTYPE",
                table: "Rotas",
                newName: "ROTDOCTYPE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ROTDOCNUM",
                table: "Rotas",
                newName: "DOCTYPETEXT");

            migrationBuilder.RenameColumn(
                name: "ROTDOCTYPE",
                table: "Rotas",
                newName: "DOCTYPE");
        }
    }
}
