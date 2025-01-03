using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NYS_ERP.Migrations
{
    /// <inheritdoc />
    public partial class CompanyUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BOMAnas_Companies_COMCODE",
                table: "BOMAnas");

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Companies_COMCODE",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_CostCenterAnas_Companies_COMCODE",
                table: "CostCenterAnas");

            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Companies_COMCODE",
                table: "Materials");

            migrationBuilder.DropForeignKey(
                name: "FK_RotaAnas_Companies_COMCODE",
                table: "RotaAnas");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkCenterAnas_Companies_COMCODE",
                table: "WorkCenterAnas");

            migrationBuilder.AddForeignKey(
                name: "FK_BOMAnas_Companies_COMCODE",
                table: "BOMAnas",
                column: "COMCODE",
                principalTable: "Companies",
                principalColumn: "COMCODE",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Companies_COMCODE",
                table: "Cities",
                column: "COMCODE",
                principalTable: "Companies",
                principalColumn: "COMCODE",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CostCenterAnas_Companies_COMCODE",
                table: "CostCenterAnas",
                column: "COMCODE",
                principalTable: "Companies",
                principalColumn: "COMCODE",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Companies_COMCODE",
                table: "Materials",
                column: "COMCODE",
                principalTable: "Companies",
                principalColumn: "COMCODE",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RotaAnas_Companies_COMCODE",
                table: "RotaAnas",
                column: "COMCODE",
                principalTable: "Companies",
                principalColumn: "COMCODE",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkCenterAnas_Companies_COMCODE",
                table: "WorkCenterAnas",
                column: "COMCODE",
                principalTable: "Companies",
                principalColumn: "COMCODE",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BOMAnas_Companies_COMCODE",
                table: "BOMAnas");

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Companies_COMCODE",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_CostCenterAnas_Companies_COMCODE",
                table: "CostCenterAnas");

            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Companies_COMCODE",
                table: "Materials");

            migrationBuilder.DropForeignKey(
                name: "FK_RotaAnas_Companies_COMCODE",
                table: "RotaAnas");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkCenterAnas_Companies_COMCODE",
                table: "WorkCenterAnas");

            migrationBuilder.AddForeignKey(
                name: "FK_BOMAnas_Companies_COMCODE",
                table: "BOMAnas",
                column: "COMCODE",
                principalTable: "Companies",
                principalColumn: "COMCODE",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Companies_COMCODE",
                table: "Cities",
                column: "COMCODE",
                principalTable: "Companies",
                principalColumn: "COMCODE",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CostCenterAnas_Companies_COMCODE",
                table: "CostCenterAnas",
                column: "COMCODE",
                principalTable: "Companies",
                principalColumn: "COMCODE",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Companies_COMCODE",
                table: "Materials",
                column: "COMCODE",
                principalTable: "Companies",
                principalColumn: "COMCODE",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RotaAnas_Companies_COMCODE",
                table: "RotaAnas",
                column: "COMCODE",
                principalTable: "Companies",
                principalColumn: "COMCODE",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkCenterAnas_Companies_COMCODE",
                table: "WorkCenterAnas",
                column: "COMCODE",
                principalTable: "Companies",
                principalColumn: "COMCODE",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
