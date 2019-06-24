using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Foundation.Workflow.Migrations
{
    public partial class start_time : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Workflows",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Workflows",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Workflows");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Workflows");
        }
    }
}
