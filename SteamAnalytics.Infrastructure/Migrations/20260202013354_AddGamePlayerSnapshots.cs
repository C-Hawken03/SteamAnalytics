using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SteamAnalytics.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGamePlayerSnapshots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerSnapshots_Games_GameId",
                table: "PlayerSnapshots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayerSnapshots",
                table: "PlayerSnapshots");

            migrationBuilder.RenameTable(
                name: "PlayerSnapshots",
                newName: "game_player_snapshots");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerSnapshots_GameId_Timestamp",
                table: "game_player_snapshots",
                newName: "IX_game_player_snapshots_GameId_Timestamp");

            migrationBuilder.AddColumn<int>(
                name: "GameId1",
                table: "game_player_snapshots",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_game_player_snapshots",
                table: "game_player_snapshots",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_game_player_snapshots_GameId1",
                table: "game_player_snapshots",
                column: "GameId1");

            migrationBuilder.AddForeignKey(
                name: "FK_game_player_snapshots_Games_GameId",
                table: "game_player_snapshots",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_game_player_snapshots_Games_GameId1",
                table: "game_player_snapshots",
                column: "GameId1",
                principalTable: "Games",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_game_player_snapshots_Games_GameId",
                table: "game_player_snapshots");

            migrationBuilder.DropForeignKey(
                name: "FK_game_player_snapshots_Games_GameId1",
                table: "game_player_snapshots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_game_player_snapshots",
                table: "game_player_snapshots");

            migrationBuilder.DropIndex(
                name: "IX_game_player_snapshots_GameId1",
                table: "game_player_snapshots");

            migrationBuilder.DropColumn(
                name: "GameId1",
                table: "game_player_snapshots");

            migrationBuilder.RenameTable(
                name: "game_player_snapshots",
                newName: "PlayerSnapshots");

            migrationBuilder.RenameIndex(
                name: "IX_game_player_snapshots_GameId_Timestamp",
                table: "PlayerSnapshots",
                newName: "IX_PlayerSnapshots_GameId_Timestamp");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayerSnapshots",
                table: "PlayerSnapshots",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerSnapshots_Games_GameId",
                table: "PlayerSnapshots",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
