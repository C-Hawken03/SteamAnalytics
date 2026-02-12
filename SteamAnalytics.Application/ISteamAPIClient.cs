
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SteamAnalytics.Domain {
    /// <summary>
    /// Interface for fetching games from the Steam API.
    /// </summary>
    public interface ISteamApiClient {
        Task<IReadOnlyList<Game>> FetchGamesAsync(CancellationToken cancellationToken);
    }
}
