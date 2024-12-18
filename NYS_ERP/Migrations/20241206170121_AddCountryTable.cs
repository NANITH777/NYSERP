using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NYS_ERP.Migrations
{
    /// <inheritdoc />
    public partial class AddCountryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    COUNTRYCODE = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    COUNTRYTEXT = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    COMCODE = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.COUNTRYCODE);
                    table.ForeignKey(
                        name: "FK_Countries_Companies_COMCODE",
                        column: x => x.COMCODE,
                        principalTable: "Companies",
                        principalColumn: "COMCODE",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "COUNTRYCODE", "COMCODE", "COUNTRYTEXT" },
                values: new object[,]
                {
                    { "DEU", "004", "Germany" },
                    { "FRA", "001", "France" },
                    { "TUR", "003", "Turkey" },
                    { "USA", "002", "United States" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Countries_COMCODE",
                table: "Countries",
                column: "COMCODE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
