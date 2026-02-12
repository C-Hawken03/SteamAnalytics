namespace SteamAnalytics.Domain {
    public sealed class Game {
        public Game(int appId, string name) {
            AppId = appId;
            Name = name;
        }

        public int Id { get; set; }
        public int AppId { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public ICollection<PlayerSnapshot> PlayerSnapshots { get; set; } = new List<PlayerSnapshot>();
        public void AddTag(Tag tag) { 
            Tags.Add(tag);
        }
    }
}
