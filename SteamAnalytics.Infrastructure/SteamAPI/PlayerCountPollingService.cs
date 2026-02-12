using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SteamAnalytics.Domain;
using SteamAnalytics.Infrastructure.Persistence;

namespace SteamAnalytics.Infrastructure.SteamAPI {
    /// <summary>
    /// Background service that periodically polls Steam for player counts and stores snapshots.
    /// </summary>
    public sealed class PlayerCountPollingService : BackgroundService {
        private readonly IServiceProvider _services;
        private readonly SteamStoreApiClient _api;
        private readonly ILogger<PlayerCountPollingService> _logger;

        public PlayerCountPollingService(
            IServiceProvider services,
            SteamStoreApiClient api,
            ILogger<PlayerCountPollingService> logger) {
            _services = services;
            _api = api;
            _logger = logger;
        }
        /// <summary>
        /// Fetches current player counts for games and saves them as timestamped snapshots.
        /// </summary>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
            _logger.LogInformation("Starting player count polling");

            while (!stoppingToken.IsCancellationRequested) {
                using var scope = _services.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<SteamAnalyticsDbContext>();

                var games = await db.Games
                    .OrderBy(g => g.Id)
                    .Take(50)
                    .ToListAsync(stoppingToken);

                if (!games.Any()) {
                    _logger.LogInformation("No games to poll");
                    return;
                }

                var batchTimestamp = DateTime.UtcNow;

                foreach (var game in games) {
                    stoppingToken.ThrowIfCancellationRequested();

                    try {

                        var players = await _api.GetNumberPlayersAsync(game.AppId);
                        if (players.HasValue) {
                            _logger.LogInformation("AppId {AppId} player_count: {PlayerCount}", game.AppId, players.Value);

                            db.PlayerSnapshots.Add(new PlayerSnapshot {
                                GameId = game.Id,
                                PlayerCount = players.Value,
                                Timestamp = batchTimestamp
                            });
                        }

                        await Task.Delay(1500, stoppingToken);
                    } catch (Exception ex) {
                        _logger.LogWarning(ex, "Failed to poll players for AppId {AppId}", game.AppId);
                    }
                }

                await db.SaveChangesAsync(stoppingToken);

                // Wait 10 minutes before next batch
                await Task.Delay(TimeSpan.FromMinutes(15), stoppingToken);
            }
        }

    }

}
