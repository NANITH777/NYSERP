using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NYS_ERP.Migrations
{
    /// <inheritdoc />
    public partial class OPUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DOCTYPETEXT",
                table: "Operations",
                newName: "OPRDOCNUM");

            migrationBuilder.RenameColumn(
                name: "DOCTYPE",
                table: "Operations",
                newName: "OPRDOCTYPE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OPRDOCNUM",
                table: "Operations",
                newName: "DOCTYPETEXT");

            migrationBuilder.RenameColumn(
                name: "OPRDOCTYPE",
                table: "Operations",
                newName: "DOCTYPE");
        }
    }
}
