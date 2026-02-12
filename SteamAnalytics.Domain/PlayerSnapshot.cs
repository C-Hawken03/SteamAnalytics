using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamAnalytics.Domain {
    public sealed class PlayerSnapshot {
        public int Id { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; } = null!;

        public int PlayerCount { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
