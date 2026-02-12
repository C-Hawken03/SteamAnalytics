using SteamAnalytics.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace SteamAnalytics.Application {
    /// <summary>
    /// Interface for saving and updating games in the database.
    /// </summary>
    public interface IGameRepository {
        /// <summary> Updates or inserts a game if it already exists. </summary>
        Task UpsertAsync(Game game, CancellationToken cancellationToken);
        /// <summary> Saves pending changes to the database. </summary>
        Task SaveChangesAsync(CancellationToken ct);
        /// <summary> Clears Entity Framework change tracking. </summary>
        void ClearTracking();
    }
}
