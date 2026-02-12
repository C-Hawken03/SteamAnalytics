using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SteamAnalytics.Domain;
using SteamAnalytics.Application;

namespace SteamAnalytics.Infrastructure.Persistence {
    /// <summary>
    /// Handles saving and updating game data in the database.
    /// </summary>
    public sealed class GameRepository : IGameRepository {
        private readonly SteamAnalyticsDbContext _db;

        public GameRepository(SteamAnalyticsDbContext db) {
            _db = db;
        }

        /// <summary> Inserts a new game or updates an existing one. </summary>
        public async Task UpsertAsync(Game game, CancellationToken ct) {
            var existing = await _db.Games
                .Include(g => g.Tags)
                .FirstOrDefaultAsync(g => g.AppId == game.AppId, ct);

            if (existing is null) {
                _db.Games.Add(game);
                return;
            }

            existing.Name = game.Name;

            await SyncTagsAsync(existing, game.Tags, ct);
        }
        /// <summary> Saves all pending database changes. </summary>
        public Task SaveChangesAsync(CancellationToken ct) =>
            _db.SaveChangesAsync(ct);

        /// <summary> Synchronises a game’s tags with the database, creating new tags if needed. </summary>
        private async Task SyncTagsAsync(
            Game existing,
            ICollection<Tag> incomingTags,
            CancellationToken ct) {

            // Load all tags we might need in ONE query
            var tagNames = incomingTags.Select(t => t.Name).ToList();

            var existingTags = await _db.Tags
                .Where(t => tagNames.Contains(t.Name))
                .ToDictionaryAsync(t => t.Name, ct);

            existing.Tags.Clear();

            foreach (var tag in incomingTags) {
                if (!existingTags.TryGetValue(tag.Name, out var dbTag)) {
                    dbTag = new Tag(tag.Name);
                    _db.Tags.Add(dbTag);
                }

                existing.Tags.Add(dbTag);
            }
        }
        public void ClearTracking() {
            _db.ChangeTracker.Clear();
        }


    }
}
