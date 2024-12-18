using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NYS_ERP.Migrations
{
    /// <inheritdoc />
    public partial class AddCityTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    CITYCODE = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    CITYTEXT = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    COMCODE = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    COUNTRYCODE = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CITYCODE);
                    table.ForeignKey(
                        name: "FK_Cities_Companies_COMCODE",
                        column: x => x.COMCODE,
                        principalTable: "Companies",
                        principalColumn: "COMCODE",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_COUNTRYCODE",
                        column: x => x.COUNTRYCODE,
                        principalTable: "Countries",
                        principalColumn: "COUNTRYCODE",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_COMCODE",
                table: "Cities",
                column: "COMCODE");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_COUNTRYCODE",
                table: "Cities",
                column: "COUNTRYCODE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
