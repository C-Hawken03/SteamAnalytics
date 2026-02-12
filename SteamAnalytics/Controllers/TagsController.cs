using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SteamAnalytics.Infrastructure.Persistence;
using SteamAnalytics.Domain;

namespace SteamAnalytics.Api.Controllers {
    [ApiController]
    [Route("api/tags")]
    public class TagsController : ControllerBase {
        private readonly SteamAnalyticsDbContext _db;

        public TagsController(SteamAnalyticsDbContext db) {
            _db = db;
        }

        /// <summary>
        /// Raw SQL query to compute total current player count per tag based on the MOST RECENT snapshot per game
        /// </summary>
        [HttpGet("playercounts")]
        public async Task<IActionResult> GetTagPlayerCounts() {
            var sql = @"
        SELECT t.Name AS TagName, SUM(ps.PlayerCount) AS PlayerCount
        FROM Tags t
        JOIN GameTags gt ON t.Id = gt.TagsId
        JOIN Games g ON g.Id = gt.GamesId
        JOIN (
            SELECT GameId, PlayerCount
            FROM (
                SELECT gps.GameId, gps.PlayerCount,
                       ROW_NUMBER() OVER (PARTITION BY GameId ORDER BY Timestamp DESC) AS rn
                FROM game_player_snapshots gps
            ) x
            WHERE rn = 1
        ) ps ON g.Id = ps.GameId
        GROUP BY t.Name
        ORDER BY PlayerCount DESC";

            // You can create a simple DTO class to hold these results
            var results = await _db.Database.SqlQueryRaw<TagPlayerCountDto>(sql).ToListAsync();

            return Ok(results);
        }

    }
}
