using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamAnalytics.Domain {
    public sealed class Tag {

        public Tag(string name) { 
            Name = name; 
        }
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<Game> Games { get; set; } = new List<Game>();
    }
}
