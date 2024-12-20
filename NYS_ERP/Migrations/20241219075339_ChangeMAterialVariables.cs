using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NYS_ERP.Migrations
{
    /// <inheritdoc />
    public partial class ChangeMAterialVariables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DOCTYPETEXT",
                table: "MaterialTypes",
                newName: "MATDOCNUM");

            migrationBuilder.RenameColumn(
                name: "DOCTYPE",
                table: "MaterialTypes",
                newName: "MATDOCTYPE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MATDOCNUM",
                table: "MaterialTypes",
                newName: "DOCTYPETEXT");

            migrationBuilder.RenameColumn(
                name: "MATDOCTYPE",
                table: "MaterialTypes",
                newName: "DOCTYPE");
        }
    }
}
