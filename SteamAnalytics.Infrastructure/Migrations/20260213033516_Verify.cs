using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SteamAnalytics.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Verify : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameTags_Games_GamesId",
                table: "GameTags");

            migrationBuilder.DropForeignKey(
                name: "FK_GameTags_Tags_TagsId",
                table: "GameTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameTags",
                table: "GameTags");

            migrationBuilder.DropIndex(
                name: "IX_Games_AppId",
                table: "Games");

            migrationBuilder.RenameTable(
                name: "GameTags",
                newName: "gametags");

            migrationBuilder.RenameIndex(
                name: "IX_GameTags_TagsId",
                table: "gametags",
                newName: "IX_gametags_TagsId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Games",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_gametags",
                table: "gametags",
                columns: new[] { "GamesId", "TagsId" });

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_gametags",
                table: "gametags");

            migrationBuilder.RenameTable(
                name: "gametags",
                newName: "GameTags");

            migrationBuilder.RenameIndex(
                name: "IX_gametags_TagsId",
                table: "GameTags",
                newName: "IX_GameTags_TagsId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Games",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameTags",
                table: "GameTags",
                columns: new[] { "GamesId", "TagsId" });

            migrationBuilder.CreateIndex(
                name: "IX_Games_AppId",
                table: "Games",
                column: "AppId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GameTags_Games_GamesId",
                table: "GameTags",
                column: "GamesId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameTags_Tags_TagsId",
                table: "GameTags",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
