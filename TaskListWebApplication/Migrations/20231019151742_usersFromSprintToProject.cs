using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskListWebApplication.Migrations
{
    /// <inheritdoc />
    public partial class usersFromSprintToProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "users",
                table: "sprint");

            migrationBuilder.AddColumn<List<string>>(
                name: "users",
                table: "project",
                type: "text[]",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "users",
                table: "project");

            migrationBuilder.AddColumn<List<string>>(
                name: "users",
                table: "sprint",
                type: "text[]",
                nullable: false);
        }
    }
}
