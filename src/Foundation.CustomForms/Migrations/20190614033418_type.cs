using Microsoft.EntityFrameworkCore.Migrations;

namespace Foundation.CustomForms.Migrations
{
    public partial class type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EntityType",
                table: "CustomForms",
                newName: "Type");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "CustomForms",
                newName: "EntityType");
        }
    }
}
