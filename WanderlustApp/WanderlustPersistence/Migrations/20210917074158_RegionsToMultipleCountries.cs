using Microsoft.EntityFrameworkCore.Migrations;

namespace WanderlustPersistence.Migrations
{
    public partial class RegionsToMultipleCountries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Regions_Countries_CountryId",
                table: "Regions");

            migrationBuilder.DropIndex(
                name: "IX_Regions_CountryId",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Regions");

            migrationBuilder.CreateTable(
                name: "CountryRegionComponent",
                columns: table => new
                {
                    CountryId = table.Column<long>(type: "bigint", nullable: false),
                    RegionsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryRegionComponent", x => new { x.CountryId, x.RegionsId });
                    table.ForeignKey(
                        name: "FK_CountryRegionComponent_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryRegionComponent_Regions_RegionsId",
                        column: x => x.RegionsId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CountryRegionComponent_RegionsId",
                table: "CountryRegionComponent",
                column: "RegionsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CountryRegionComponent");

            migrationBuilder.AddColumn<long>(
                name: "CountryId",
                table: "Regions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Regions_CountryId",
                table: "Regions",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Regions_Countries_CountryId",
                table: "Regions",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
