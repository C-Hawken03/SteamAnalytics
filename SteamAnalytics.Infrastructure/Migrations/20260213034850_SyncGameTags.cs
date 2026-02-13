using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SteamAnalytics.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SyncGameTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameTags_GameId",
                table: "GameTags");

            migrationBuilder.DropForeignKey(
                name: "FK_GameTags_TagId",
                table: "GameTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameTags",
                table: "GameTags");

            migrationBuilder.RenameTable(
                name: "GameTags",
                newName: "gametags");

            migrationBuilder.RenameColumn(
                name: "TagId",
                table: "gametags",
                newName: "TagsId");

            migrationBuilder.RenameColumn(
                name: "GameId",
                table: "gametags",
                newName: "GamesId");

            migrationBuilder.RenameIndex(
                name: "IX_GameTags_TagId",
                table: "gametags",
                newName: "IX_gametags_TagsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_gametags",
                table: "gametags",
                columns: new[] { "GamesId", "TagsId" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_gametags_GamesId",
                table: "gametags");

            migrationBuilder.DropForeignKey(
                name: "FK_gametags_TagsId",
                table: "gametags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_gametags",
                table: "gametags");

            migrationBuilder.RenameTable(
                name: "gametags",
                newName: "GameTags");

            migrationBuilder.RenameColumn(
                name: "TagsId",
                table: "GameTags",
                newName: "TagId");

            migrationBuilder.RenameColumn(
                name: "GamesId",
                table: "GameTags",
                newName: "GameId");

            migrationBuilder.RenameIndex(
                name: "IX_gametags_TagsId",
                table: "GameTags",
                newName: "IX_GameTags_TagId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameTags",
                table: "GameTags",
                columns: new[] { "GameId", "TagId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GameTags_GameId",
                table: "GameTags",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameTags_TagId",
                table: "GameTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
