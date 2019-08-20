using Microsoft.EntityFrameworkCore.Migrations;

namespace Foundation.Workflow.Migrations
{
    public partial class rename_column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WorkflowDefinitionId",
                table: "Workflows",
                newName: "WorkflowDefinitionName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WorkflowDefinitionName",
                table: "Workflows",
                newName: "WorkflowDefinitionId");
        }
    }
}
