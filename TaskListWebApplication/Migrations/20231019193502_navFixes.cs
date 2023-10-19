using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskListWebApplication.Migrations
{
    /// <inheritdoc />
    public partial class navFixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sprint_project_projectId",
                table: "sprint");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_sprint_sprintId1",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_sprintId1",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sprint",
                table: "sprint");

            migrationBuilder.DropPrimaryKey(
                name: "PK_project",
                table: "project");

            migrationBuilder.DropColumn(
                name: "sprintId1",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "projectId1",
                table: "sprint");

            migrationBuilder.RenameTable(
                name: "Tasks",
                newName: "tasks");

            migrationBuilder.RenameTable(
                name: "sprint",
                newName: "sprints");

            migrationBuilder.RenameTable(
                name: "project",
                newName: "projects");

            migrationBuilder.RenameIndex(
                name: "IX_sprint_projectId",
                table: "sprints",
                newName: "IX_sprints_projectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tasks",
                table: "tasks",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sprints",
                table: "sprints",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_projects",
                table: "projects",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_tasks_sprintId",
                table: "tasks",
                column: "sprintId");

            migrationBuilder.AddForeignKey(
                name: "FK_sprints_projects_projectId",
                table: "sprints",
                column: "projectId",
                principalTable: "projects",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_sprints_sprintId",
                table: "tasks",
                column: "sprintId",
                principalTable: "sprints",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sprints_projects_projectId",
                table: "sprints");

            migrationBuilder.DropForeignKey(
                name: "FK_tasks_sprints_sprintId",
                table: "tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tasks",
                table: "tasks");

            migrationBuilder.DropIndex(
                name: "IX_tasks_sprintId",
                table: "tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sprints",
                table: "sprints");

            migrationBuilder.DropPrimaryKey(
                name: "PK_projects",
                table: "projects");

            migrationBuilder.RenameTable(
                name: "tasks",
                newName: "Tasks");

            migrationBuilder.RenameTable(
                name: "sprints",
                newName: "sprint");

            migrationBuilder.RenameTable(
                name: "projects",
                newName: "project");

            migrationBuilder.RenameIndex(
                name: "IX_sprints_projectId",
                table: "sprint",
                newName: "IX_sprint_projectId");

            migrationBuilder.AddColumn<Guid>(
                name: "sprintId1",
                table: "Tasks",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "projectId1",
                table: "sprint",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sprint",
                table: "sprint",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_project",
                table: "project",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_sprintId1",
                table: "Tasks",
                column: "sprintId1");

            migrationBuilder.AddForeignKey(
                name: "FK_sprint_project_projectId",
                table: "sprint",
                column: "projectId",
                principalTable: "project",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_sprint_sprintId1",
                table: "Tasks",
                column: "sprintId1",
                principalTable: "sprint",
                principalColumn: "id");
        }
    }
}
