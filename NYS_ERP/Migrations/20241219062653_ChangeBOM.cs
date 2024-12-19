using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NYS_ERP.Migrations
{
    /// <inheritdoc />
    public partial class ChangeBOM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DOCTYPETEXT",
                table: "BOMs",
                newName: "BOMDOCNUM");

            migrationBuilder.RenameColumn(
                name: "DOCTYPE",
                table: "BOMs",
                newName: "BOMDOCTYPE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BOMDOCNUM",
                table: "BOMs",
                newName: "DOCTYPETEXT");

            migrationBuilder.RenameColumn(
                name: "BOMDOCTYPE",
                table: "BOMs",
                newName: "DOCTYPE");
        }
    }
}
