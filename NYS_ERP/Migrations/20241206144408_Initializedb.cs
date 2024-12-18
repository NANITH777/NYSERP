using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NYS_ERP.Migrations
{
    /// <inheritdoc />
    public partial class Initializedb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    COMCODE = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    COMTEXT = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    ADDRESS1 = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    ADDRESS2 = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.COMCODE);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    LANCODE = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    LANTEXT = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    COMCODE = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.LANCODE);
                    table.ForeignKey(
                        name: "FK_Languages_Companies_COMCODE",
                        column: x => x.COMCODE,
                        principalTable: "Companies",
                        principalColumn: "COMCODE",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "COMCODE", "ADDRESS1", "ADDRESS2", "COMTEXT" },
                values: new object[,]
                {
                    { "001", "123 Main St", "Suite 100", "Company A" },
                    { "002", "456 Elm St", "Apt 5", "Company B" },
                    { "003", "789 Oak St", "Floor 2", "Company C" },
                    { "004", "101 Pine St", "Building B", "Company D" }
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "LANCODE", "COMCODE", "LANTEXT" },
                values: new object[,]
                {
                    { "001", "001", "English" },
                    { "002", "001", "French" },
                    { "003", "002", "Spanish" },
                    { "004", "002", "German" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Languages_COMCODE",
                table: "Languages",
                column: "COMCODE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
