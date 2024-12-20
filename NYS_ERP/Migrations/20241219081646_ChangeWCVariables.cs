using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NYS_ERP.Migrations
{
    /// <inheritdoc />
    public partial class ChangeWCVariables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DOCTYPETEXT",
                table: "WorkCenters",
                newName: "WCMDOCNUM");

            migrationBuilder.RenameColumn(
                name: "DOCTYPE",
                table: "WorkCenters",
                newName: "WCMDOCTYPE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WCMDOCNUM",
                table: "WorkCenters",
                newName: "DOCTYPETEXT");

            migrationBuilder.RenameColumn(
                name: "WCMDOCTYPE",
                table: "WorkCenters",
                newName: "DOCTYPE");
        }
    }
}
