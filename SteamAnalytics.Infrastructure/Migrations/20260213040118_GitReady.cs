using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SteamAnalytics.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class GitReady : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_gametags_GamesId",
                table: "gametags");

            migrationBuilder.DropForeignKey(
                name: "FK_gametags_TagsId",
                table: "gametags");

            migrationBuilder.AddForeignKey(
                name: "FK_gametags_Games_GamesId",
                table: "gametags",
                column: "GamesId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_gametags_Tags_TagsId",
                table: "gametags",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_gametags_Games_GamesId",
                table: "gametags");

            migrationBuilder.DropForeignKey(
                name: "FK_gametags_Tags_TagsId",
                table: "gametags");

            migrationBuilder.AddForeignKey(
                name: "FK_gametags_GamesId",
                table: "gametags",
                column: "GamesId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_gametags_TagsId",
                table: "gametags",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
