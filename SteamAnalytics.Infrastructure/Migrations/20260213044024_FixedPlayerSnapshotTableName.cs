using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SteamAnalytics.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixedPlayerSnapshotTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_playersnapshots_Games_GameId",
                table: "playersnapshots");

            migrationBuilder.DropForeignKey(
                name: "FK_playersnapshots_Games_GameId1",
                table: "playersnapshots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_playersnapshots",
                table: "playersnapshots");

            migrationBuilder.RenameTable(
                name: "playersnapshots",
                newName: "game_player_snapshots");

            migrationBuilder.RenameIndex(
                name: "IX_playersnapshots_GameId1",
                table: "game_player_snapshots",
                newName: "IX_game_player_snapshots_GameId1");

            migrationBuilder.RenameIndex(
                name: "IX_playersnapshots_GameId_Timestamp",
                table: "game_player_snapshots",
                newName: "IX_game_player_snapshots_GameId_Timestamp");

            migrationBuilder.AddPrimaryKey(
                name: "PK_game_player_snapshots",
                table: "game_player_snapshots",
                column: "Id");

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

            migrationBuilder.RenameTable(
                name: "game_player_snapshots",
                newName: "playersnapshots");

            migrationBuilder.RenameIndex(
                name: "IX_game_player_snapshots_GameId1",
                table: "playersnapshots",
                newName: "IX_playersnapshots_GameId1");

            migrationBuilder.RenameIndex(
                name: "IX_game_player_snapshots_GameId_Timestamp",
                table: "playersnapshots",
                newName: "IX_playersnapshots_GameId_Timestamp");

            migrationBuilder.AddPrimaryKey(
                name: "PK_playersnapshots",
                table: "playersnapshots",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_playersnapshots_Games_GameId",
                table: "playersnapshots",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_playersnapshots_Games_GameId1",
                table: "playersnapshots",
                column: "GameId1",
                principalTable: "Games",
                principalColumn: "Id");
        }
    }
}
