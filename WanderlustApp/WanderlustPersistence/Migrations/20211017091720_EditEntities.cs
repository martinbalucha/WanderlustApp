using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WanderlustPersistence.Migrations
{
    public partial class EditEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Regions_Regions_RegionId",
                table: "Regions");

            migrationBuilder.DropForeignKey(
                name: "FK_Regions_Towns_CapitalId",
                table: "Regions");

            migrationBuilder.DropForeignKey(
                name: "FK_Sight_Regions_RegionId",
                table: "Sight");

            migrationBuilder.DropForeignKey(
                name: "FK_Sight_Towns_TownId",
                table: "Sight");

            migrationBuilder.DropForeignKey(
                name: "FK_Towns_Regions_RegionId",
                table: "Towns");

            migrationBuilder.DropTable(
                name: "CountryRegionComponent");

            migrationBuilder.DropTable(
                name: "RegionComponentUser");

            migrationBuilder.DropIndex(
                name: "IX_Regions_CapitalId",
                table: "Regions");

            migrationBuilder.DropIndex(
                name: "IX_Regions_RegionId",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "SightType",
                table: "Sight");

            migrationBuilder.DropColumn(
                name: "CapitalId",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "Regions");

            migrationBuilder.RenameColumn(
                name: "RegionId",
                table: "Sight",
                newName: "SightTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Sight_RegionId",
                table: "Sight",
                newName: "IX_Sight_SightTypeId");

            migrationBuilder.AlterColumn<Guid>(
                name: "RegionId",
                table: "Towns",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TownId",
                table: "Sight",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CountryId",
                table: "Regions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Foods",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "RegionUser",
                columns: table => new
                {
                    RegionsVisitedId = table.Column<Guid>(type: "uuid", nullable: false),
                    VisitedByUsersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionUser", x => new { x.RegionsVisitedId, x.VisitedByUsersId });
                    table.ForeignKey(
                        name: "FK_RegionUser_Regions_RegionsVisitedId",
                        column: x => x.RegionsVisitedId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegionUser_Users_VisitedByUsersId",
                        column: x => x.VisitedByUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SightType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SightOrigin = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SightType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Regions_CountryId",
                table: "Regions",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_RegionUser_VisitedByUsersId",
                table: "RegionUser",
                column: "VisitedByUsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Regions_Countries_CountryId",
                table: "Regions",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sight_SightType_SightTypeId",
                table: "Sight",
                column: "SightTypeId",
                principalTable: "SightType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sight_Towns_TownId",
                table: "Sight",
                column: "TownId",
                principalTable: "Towns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Towns_Regions_RegionId",
                table: "Towns",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Regions_Countries_CountryId",
                table: "Regions");

            migrationBuilder.DropForeignKey(
                name: "FK_Sight_SightType_SightTypeId",
                table: "Sight");

            migrationBuilder.DropForeignKey(
                name: "FK_Sight_Towns_TownId",
                table: "Sight");

            migrationBuilder.DropForeignKey(
                name: "FK_Towns_Regions_RegionId",
                table: "Towns");

            migrationBuilder.DropTable(
                name: "RegionUser");

            migrationBuilder.DropTable(
                name: "SightType");

            migrationBuilder.DropIndex(
                name: "IX_Regions_CountryId",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Regions");

            migrationBuilder.RenameColumn(
                name: "SightTypeId",
                table: "Sight",
                newName: "RegionId");

            migrationBuilder.RenameIndex(
                name: "IX_Sight_SightTypeId",
                table: "Sight",
                newName: "IX_Sight_RegionId");

            migrationBuilder.AlterColumn<Guid>(
                name: "RegionId",
                table: "Towns",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "TownId",
                table: "Sight",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<int>(
                name: "SightType",
                table: "Sight",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "CapitalId",
                table: "Regions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Regions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "RegionId",
                table: "Regions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Foods",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

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
                    table.ForeignKey(
                        name: "FK_CountryRegionComponent_Regions_RegionsId",
                        column: x => x.RegionsId,
                        principalTable: "Regions",
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

            migrationBuilder.CreateIndex(
                name: "IX_Regions_CapitalId",
                table: "Regions",
                column: "CapitalId");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_RegionId",
                table: "Regions",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryRegionComponent_RegionsId",
                table: "CountryRegionComponent",
                column: "RegionsId");

            migrationBuilder.CreateIndex(
                name: "IX_RegionComponentUser_VisitedByUsersId",
                table: "RegionComponentUser",
                column: "VisitedByUsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Regions_Regions_RegionId",
                table: "Regions",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Regions_Towns_CapitalId",
                table: "Regions",
                column: "CapitalId",
                principalTable: "Towns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
    }
}
