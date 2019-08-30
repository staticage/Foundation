using Microsoft.EntityFrameworkCore.Migrations;

namespace Foundation.Workflow.Migrations
{
    public partial class rename_column1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExecutionPointers_Workflows_WorkflowId",
                table: "ExecutionPointers");

            migrationBuilder.RenameColumn(
                name: "WorkflowId",
                table: "ExecutionPointers",
                newName: "WorkflowInstanceId");

            migrationBuilder.RenameIndex(
                name: "IX_ExecutionPointers_WorkflowId",
                table: "ExecutionPointers",
                newName: "IX_ExecutionPointers_WorkflowInstanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExecutionPointers_Workflows_WorkflowInstanceId",
                table: "ExecutionPointers",
                column: "WorkflowInstanceId",
                principalTable: "Workflows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExecutionPointers_Workflows_WorkflowInstanceId",
                table: "ExecutionPointers");

            migrationBuilder.RenameColumn(
                name: "WorkflowInstanceId",
                table: "ExecutionPointers",
                newName: "WorkflowId");

            migrationBuilder.RenameIndex(
                name: "IX_ExecutionPointers_WorkflowInstanceId",
                table: "ExecutionPointers",
                newName: "IX_ExecutionPointers_WorkflowId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExecutionPointers_Workflows_WorkflowId",
                table: "ExecutionPointers",
                column: "WorkflowId",
                principalTable: "Workflows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
