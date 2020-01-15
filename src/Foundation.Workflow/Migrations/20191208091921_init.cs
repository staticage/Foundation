using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkflowCore.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "wfc");

            migrationBuilder.CreateTable(
                name: "Workflows",
                schema: "wfc",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: true),
                    Version = table.Column<int>(nullable: false),
                    WorkflowDefinitionId = table.Column<string>(nullable: true),
                    Reference = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workflows", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExecutionPointers",
                schema: "wfc",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    StepId = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: false),
                    PublishedEvents = table.Column<string>(nullable: true),
                    EventName = table.Column<string>(nullable: true),
                    EventKey = table.Column<string>(nullable: true),
                    ExtensionAttributes = table.Column<string>(nullable: true),
                    WorkflowInstanceId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExecutionPointers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExecutionPointers_Workflows_WorkflowInstanceId",
                        column: x => x.WorkflowInstanceId,
                        principalSchema: "wfc",
                        principalTable: "Workflows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExecutionPointers_WorkflowInstanceId",
                schema: "wfc",
                table: "ExecutionPointers",
                column: "WorkflowInstanceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExecutionPointers",
                schema: "wfc");

            migrationBuilder.DropTable(
                name: "Workflows",
                schema: "wfc");
        }
    }
}
