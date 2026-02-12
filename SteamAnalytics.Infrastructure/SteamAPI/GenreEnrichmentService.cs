using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SteamAnalytics.Domain;
using SteamAnalytics.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace SteamAnalytics.Infrastructure.SteamAPI {
    /// <summary>
    /// Background service that adds genre tags to games from the Steam API.
    /// </summary>
    public sealed class GameGenreEnrichmentService : BackgroundService {
        private readonly IServiceProvider _services;
        private readonly SteamStoreApiClient _api;
        private readonly ILogger<GameGenreEnrichmentService> _logger;

        public GameGenreEnrichmentService(
            IServiceProvider services,
            SteamStoreApiClient api,
            ILogger<GameGenreEnrichmentService> logger) {
            _services = services;
            _api = api;
            _logger = logger;
        }
        /// <summary>
        /// Fetches games without tags, retrieves their genres from the Steam API, and stores them in the database.
        /// </summary>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
            _logger.LogInformation("Starting genre enrichment");

            while (!stoppingToken.IsCancellationRequested) {
                using var scope = _services.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<SteamAnalyticsDbContext>();

                var games = await db.Games
                    .Where(g => !g.Tags.Any())
                    .OrderBy(g => g.Id)
                    .Take(50)
                    .ToListAsync(stoppingToken);

                if (!games.Any()) {
                    _logger.LogInformation("No more games to enrich");
                    return;
                }

                var existingTags = await db.Tags
                    .ToDictionaryAsync(t => t.Name, t => t, stoppingToken);

                foreach (var game in games) {
                    stoppingToken.ThrowIfCancellationRequested();

                    try {
                        var genres = await _api.GetGenresAsync(game.AppId);

                        foreach (var genre in genres) {
                            if (!existingTags.TryGetValue(genre, out var tag)) {
                                tag = new Tag(genre);
                                db.Tags.Add(tag);
                                existingTags[genre] = tag;
                            }

                            game.AddTag(tag);
                        }

                        await Task.Delay(500, stoppingToken);
                    } catch (Exception ex) {
                        _logger.LogWarning(ex, "Failed to enrich AppId {AppId}", game.AppId);
                    }
                }

                await db.SaveChangesAsync(stoppingToken);
                _logger.LogInformation("Enriched {Count} games", games.Count);
            }
        }

    }

}
