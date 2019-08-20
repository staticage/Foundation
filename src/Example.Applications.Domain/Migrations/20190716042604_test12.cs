using Microsoft.EntityFrameworkCore.Migrations;

namespace Example.Applications.Domain.Migrations
{
    public partial class test12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_customers",
                table: "customers");

            migrationBuilder.RenameTable(
                name: "customers",
                newName: "Customers");

            migrationBuilder.RenameColumn(
                name: "source",
                table: "Customers",
                newName: "Source");

            migrationBuilder.RenameColumn(
                name: "photo",
                table: "Customers",
                newName: "Photo");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Customers",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "mobile",
                table: "Customers",
                newName: "Mobile");

            migrationBuilder.RenameColumn(
                name: "age",
                table: "Customers",
                newName: "Age");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Customers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id_card",
                table: "Customers",
                newName: "IdCard");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "customers");

            migrationBuilder.RenameColumn(
                name: "Source",
                table: "customers",
                newName: "source");

            migrationBuilder.RenameColumn(
                name: "Photo",
                table: "customers",
                newName: "photo");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "customers",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Mobile",
                table: "customers",
                newName: "mobile");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "customers",
                newName: "age");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "customers",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "IdCard",
                table: "customers",
                newName: "id_card");

            migrationBuilder.AddPrimaryKey(
                name: "pk_customers",
                table: "customers",
                column: "id");
        }
    }
}
