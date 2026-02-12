using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SteamAnalytics.Application;
using SteamAnalytics.Infrastructure.Persistence;

namespace SteamAnalytics.Infrastructure.SteamAPI {
    /// <summary>
    /// Background service that fetches new games from an external catalog and inserts them into the database.
    /// </summary>
    public sealed class GameCatalogIngestionService : BackgroundService {
        private readonly IGameCatalogSource _catalogSource;
        private readonly IServiceProvider _services;
        private readonly ILogger<GameCatalogIngestionService> _logger;

        public GameCatalogIngestionService(
            IGameCatalogSource catalogSource,
            IServiceProvider services,
            ILogger<GameCatalogIngestionService> logger) {
            _catalogSource = catalogSource;
            _services = services;
            _logger = logger;
        }
        /// <summary>
        /// Executes the game catalog ingestion process by fetching games and inserting any that do not already exist.
        /// </summary>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
            _logger.LogInformation("Starting game catalog ingestion");

            using var scope = _services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<SteamAnalyticsDbContext>();

            var incomingGames = await _catalogSource.FetchGamesAsync(stoppingToken);

            var existingAppIds = await db.Games
                .AsNoTracking()
                .Select(g => g.AppId)
                .ToHashSetAsync(stoppingToken);

            var newGames = incomingGames
                .Where(g => !existingAppIds.Contains(g.AppId))
                .ToList();

            db.Games.AddRange(newGames);
            await db.SaveChangesAsync(stoppingToken);

            _logger.LogInformation(
                "Inserted {Count} new games",
                newGames.Count
            );
        }
    }
}
