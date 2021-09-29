using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WanderlustPersistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Description = table.Column<string>(type: "character varying(2500)", maxLength: 2500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    Recipe = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Salt = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsAdmin = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CountryTraditionalFood",
                columns: table => new
                {
                    CountryId = table.Column<Guid>(type: "uuid", nullable: false),
                    TypicalFoodsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryTraditionalFood", x => new { x.CountryId, x.TypicalFoodsId });
                    table.ForeignKey(
                        name: "FK_CountryTraditionalFood_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryTraditionalFood_Foods_TypicalFoodsId",
                        column: x => x.TypicalFoodsId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountryUser",
                columns: table => new
                {
                    CountriesVisitedId = table.Column<Guid>(type: "uuid", nullable: false),
                    VisitedByUsersId = table.Column<Guid>(type: "uuid", nullable: false)
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
                name: "TraditionalFoodUser",
                columns: table => new
                {
                    EatenByUsersId = table.Column<Guid>(type: "uuid", nullable: false),
                    TraditionalFoodsEatenId = table.Column<Guid>(type: "uuid", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "CountryRegionComponent",
                columns: table => new
                {
                    CountryId = table.Column<Guid>(type: "uuid", nullable: false),
                    RegionsId = table.Column<Guid>(type: "uuid", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "RegionComponentUser",
                columns: table => new
                {
                    RegionsVisitedId = table.Column<Guid>(type: "uuid", nullable: false),
                    VisitedByUsersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionComponentUser", x => new { x.RegionsVisitedId, x.VisitedByUsersId });
                    table.ForeignKey(
                        name: "FK_RegionComponentUser_Users_VisitedByUsersId",
                        column: x => x.VisitedByUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sight",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    TownId = table.Column<Guid>(type: "uuid", nullable: true),
                    RegionId = table.Column<Guid>(type: "uuid", nullable: true),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    IsUnescoSight = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sight", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Towns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RegionId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Towns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    RegionId = table.Column<Guid>(type: "uuid", nullable: true),
                    CapitalId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Regions_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Regions_Towns_CapitalId",
                        column: x => x.CapitalId,
                        principalTable: "Towns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TownUser",
                columns: table => new
                {
                    TownsVisitedId = table.Column<Guid>(type: "uuid", nullable: false),
                    VisitedByUsersId = table.Column<Guid>(type: "uuid", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_CountryRegionComponent_RegionsId",
                table: "CountryRegionComponent",
                column: "RegionsId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryTraditionalFood_TypicalFoodsId",
                table: "CountryTraditionalFood",
                column: "TypicalFoodsId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryUser_VisitedByUsersId",
                table: "CountryUser",
                column: "VisitedByUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_RegionComponentUser_VisitedByUsersId",
                table: "RegionComponentUser",
                column: "VisitedByUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_CapitalId",
                table: "Regions",
                column: "CapitalId");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_RegionId",
                table: "Regions",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Sight_RegionId",
                table: "Sight",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Sight_TownId",
                table: "Sight",
                column: "TownId");

            migrationBuilder.CreateIndex(
                name: "IX_Towns_RegionId",
                table: "Towns",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_TownUser_VisitedByUsersId",
                table: "TownUser",
                column: "VisitedByUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_TraditionalFoodUser_TraditionalFoodsEatenId",
                table: "TraditionalFoodUser",
                column: "TraditionalFoodsEatenId");

            migrationBuilder.AddForeignKey(
                name: "FK_CountryRegionComponent_Regions_RegionsId",
                table: "CountryRegionComponent",
                column: "RegionsId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RegionComponentUser_Regions_RegionsVisitedId",
                table: "RegionComponentUser",
                column: "RegionsVisitedId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sight_Regions_RegionId",
                table: "Sight",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sight_Towns_TownId",
                table: "Sight",
                column: "TownId",
                principalTable: "Towns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Towns_Regions_RegionId",
                table: "Towns",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Towns_Regions_RegionId",
                table: "Towns");

            migrationBuilder.DropTable(
                name: "CountryRegionComponent");

            migrationBuilder.DropTable(
                name: "CountryTraditionalFood");

            migrationBuilder.DropTable(
                name: "CountryUser");

            migrationBuilder.DropTable(
                name: "RegionComponentUser");

            migrationBuilder.DropTable(
                name: "Sight");

            migrationBuilder.DropTable(
                name: "TownUser");

            migrationBuilder.DropTable(
                name: "TraditionalFoodUser");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "Towns");
        }
    }
}
