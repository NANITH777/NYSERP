using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NYS_ERP.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCCVariables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DOCTYPETEXT",
                table: "CostCenters",
                newName: "CCMDOCNUM");

            migrationBuilder.RenameColumn(
                name: "DOCTYPE",
                table: "CostCenters",
                newName: "CCMDOCTYPE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CCMDOCNUM",
                table: "CostCenters",
                newName: "DOCTYPETEXT");

            migrationBuilder.RenameColumn(
                name: "CCMDOCTYPE",
                table: "CostCenters",
                newName: "DOCTYPE");
        }
    }
}
