using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskListWebApplication.Migrations
{
    /// <inheritdoc />
    public partial class usersFixSprintNav : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "projectId",
                table: "Tasks",
                newName: "sprintId1");

            migrationBuilder.AlterColumn<Guid>(
                name: "sprintId1",
                table: "Tasks",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "sprintId",
                table: "Tasks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_sprintId1",
                table: "Tasks",
                column: "sprintId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_sprint_sprintId1",
                table: "Tasks",
                column: "sprintId1",
                principalTable: "sprint",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_sprint_sprintId1",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_sprintId1",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "sprintId",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "sprintId1",
                table: "Tasks",
                newName: "projectId");

            migrationBuilder.AlterColumn<Guid>(
                name: "projectId",
                table: "Tasks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);
        }
    }
}
