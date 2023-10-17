using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskListWebApplication.Migrations
{
    /// <inheritdoc />
    public partial class taskModelMinorFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Tasks",
                newName: "user");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "user",
                table: "Tasks",
                newName: "userId");
        }
    }
}
