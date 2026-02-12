using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamAnalytics.Domain {
    public class TagPlayerCountDto {
        public string TagName { get; set; } = string.Empty;
        public long PlayerCount { get; set; }
    }
}
