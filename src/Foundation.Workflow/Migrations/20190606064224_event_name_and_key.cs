using Microsoft.EntityFrameworkCore.Migrations;

namespace Foundation.Workflow.Migrations
{
    public partial class event_name_and_key : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EventKey",
                table: "ExecutionPointers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EventName",
                table: "ExecutionPointers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventKey",
                table: "ExecutionPointers");

            migrationBuilder.DropColumn(
                name: "EventName",
                table: "ExecutionPointers");
        }
    }
}
