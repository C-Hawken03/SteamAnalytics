using SteamAnalytics.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamAnalytics.Application {
    /// <summary>
    /// Interface for getting a list of games.
    /// </summary>
    public interface IGameCatalogSource {
        /// <summary> Fetches a list of games. </summary>
        Task<IReadOnlyList<Game>> FetchGamesAsync(CancellationToken ct);
    }
}
