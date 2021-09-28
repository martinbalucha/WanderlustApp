using Microsoft.EntityFrameworkCore.Migrations;

namespace WanderlustPersistence.Migrations
{
    public partial class AddDescriptionColumnCountry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Countries",
                type: "character varying(2500)",
                maxLength: 2500,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Countries");
        }
    }
}
