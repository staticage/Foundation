using Microsoft.EntityFrameworkCore.Migrations;

namespace Foundation.CustomForms.Migrations
{
    public partial class change_type_to_name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "CustomForms",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "CustomForms",
                newName: "Type");
        }
    }
}
