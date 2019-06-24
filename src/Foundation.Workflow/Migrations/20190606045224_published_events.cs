using Microsoft.EntityFrameworkCore.Migrations;

namespace Foundation.Workflow.Migrations
{
    public partial class published_events : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "Workflows",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "WorkflowDefinitionId",
                table: "Workflows",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublishedEvents",
                table: "ExecutionPointers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "Workflows");

            migrationBuilder.DropColumn(
                name: "WorkflowDefinitionId",
                table: "Workflows");

            migrationBuilder.DropColumn(
                name: "PublishedEvents",
                table: "ExecutionPointers");
        }
    }
}
