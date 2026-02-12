using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SteamAnalytics.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIsImportantToGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsImportant",
                table: "Games",
                type: "tinyint(1)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsImportant",
                table: "Games");
        }
    }
}
