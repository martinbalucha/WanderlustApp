using Microsoft.EntityFrameworkCore.Migrations;

namespace WanderlustPersistence.Migrations
{
    public partial class ManyToManyRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Countries_Users_UserId",
                table: "Countries");

            migrationBuilder.DropForeignKey(
                name: "FK_Foods_Users_UserId",
                table: "Foods");

            migrationBuilder.DropForeignKey(
                name: "FK_Regions_Users_UserId",
                table: "Regions");

            migrationBuilder.DropForeignKey(
                name: "FK_Towns_Users_UserId",
                table: "Towns");

            migrationBuilder.DropIndex(
                name: "IX_Towns_UserId",
                table: "Towns");

            migrationBuilder.DropIndex(
                name: "IX_Regions_UserId",
                table: "Regions");

            migrationBuilder.DropIndex(
                name: "IX_Foods_UserId",
                table: "Foods");

            migrationBuilder.DropIndex(
                name: "IX_Countries_UserId",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Towns");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Countries");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Towns",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CountryUser",
                columns: table => new
                {
                    CountriesVisitedId = table.Column<long>(type: "bigint", nullable: false),
                    VisitedByUsersId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryUser", x => new { x.CountriesVisitedId, x.VisitedByUsersId });
                    table.ForeignKey(
                        name: "FK_CountryUser_Countries_CountriesVisitedId",
                        column: x => x.CountriesVisitedId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryUser_Users_VisitedByUsersId",
                        column: x => x.VisitedByUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegionComponentUser",
                columns: table => new
                {
                    RegionsVisitedId = table.Column<long>(type: "bigint", nullable: false),
                    VisitedByUsersId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionComponentUser", x => new { x.RegionsVisitedId, x.VisitedByUsersId });
                    table.ForeignKey(
                        name: "FK_RegionComponentUser_Regions_RegionsVisitedId",
                        column: x => x.RegionsVisitedId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegionComponentUser_Users_VisitedByUsersId",
                        column: x => x.VisitedByUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TownUser",
                columns: table => new
                {
                    TownsVisitedId = table.Column<long>(type: "bigint", nullable: false),
                    VisitedByUsersId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TownUser", x => new { x.TownsVisitedId, x.VisitedByUsersId });
                    table.ForeignKey(
                        name: "FK_TownUser_Towns_TownsVisitedId",
                        column: x => x.TownsVisitedId,
                        principalTable: "Towns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TownUser_Users_VisitedByUsersId",
                        column: x => x.VisitedByUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TraditionalFoodUser",
                columns: table => new
                {
                    EatenByUsersId = table.Column<long>(type: "bigint", nullable: false),
                    TraditionalFoodsEatenId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TraditionalFoodUser", x => new { x.EatenByUsersId, x.TraditionalFoodsEatenId });
                    table.ForeignKey(
                        name: "FK_TraditionalFoodUser_Foods_TraditionalFoodsEatenId",
                        column: x => x.TraditionalFoodsEatenId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TraditionalFoodUser_Users_EatenByUsersId",
                        column: x => x.EatenByUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CountryUser_VisitedByUsersId",
                table: "CountryUser",
                column: "VisitedByUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_RegionComponentUser_VisitedByUsersId",
                table: "RegionComponentUser",
                column: "VisitedByUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_TownUser_VisitedByUsersId",
                table: "TownUser",
                column: "VisitedByUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_TraditionalFoodUser_TraditionalFoodsEatenId",
                table: "TraditionalFoodUser",
                column: "TraditionalFoodsEatenId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CountryUser");

            migrationBuilder.DropTable(
                name: "RegionComponentUser");

            migrationBuilder.DropTable(
                name: "TownUser");

            migrationBuilder.DropTable(
                name: "TraditionalFoodUser");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Towns");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Towns",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Regions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Foods",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Countries",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Towns_UserId",
                table: "Towns",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_UserId",
                table: "Regions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Foods_UserId",
                table: "Foods",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_UserId",
                table: "Countries",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Countries_Users_UserId",
                table: "Countries",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_Users_UserId",
                table: "Foods",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Regions_Users_UserId",
                table: "Regions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Towns_Users_UserId",
                table: "Towns",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
