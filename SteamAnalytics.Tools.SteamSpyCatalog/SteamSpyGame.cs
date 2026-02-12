using System.Text.Json.Serialization;

public sealed class SteamSpyGame {
    [JsonPropertyName("appid")]
    public int AppId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("ccu")]
    public int CCU { get; set; }
}
