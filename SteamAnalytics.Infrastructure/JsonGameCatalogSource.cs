using SteamAnalytics.Application;
using SteamAnalytics.Domain;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text;

namespace SteamAnalytics.Infrastructure.SteamAPI {
    public sealed class JsonGameCatalogSource : IGameCatalogSource {
        private readonly string _path;

        public JsonGameCatalogSource(Microsoft.Extensions.Hosting.IHostEnvironment env) {
            _path = Path.Combine(env.ContentRootPath, "Data", "app.json");
        }

        public async Task<IReadOnlyList<Game>> FetchGamesAsync(CancellationToken cancellationToken = default) {
            if (!File.Exists(_path))
                throw new FileNotFoundException($"Could not find {_path}");

            var json = await File.ReadAllTextAsync(_path, Encoding.UTF8);

            List<AppJsonDto> apps;
            try {
                apps = JsonSerializer.Deserialize<List<AppJsonDto>>(json)
                       ?? new List<AppJsonDto>();
            } catch (Exception ex) {
                Console.WriteLine($"Deserialization failed: {ex.Message}");
                Console.WriteLine(json.Substring(0, Math.Min(500, json.Length)));
                throw;
            }

            // Deduplicate by AppId and skip invalid 0 values
            apps = apps
                .Where(a => a.AppId > 0)
                .GroupBy(a => a.AppId)
                .Select(g => g.First())
                .ToList();

            Console.WriteLine($"Total apps in JSON: {apps.Count}");
            foreach (var app in apps.Take(10))
                Console.WriteLine($"AppId: {app.AppId}, Name: {app.Name}");

            return apps.Select(a => new Game(a.AppId, a.Name)).ToList();
        }

        private sealed class AppJsonDto {
            [JsonPropertyName("appid")]
            [JsonConverter(typeof(IntFromStringOrNumberConverter))]
            public int AppId { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; } = string.Empty;
        }

        private class IntFromStringOrNumberConverter : JsonConverter<int> {
            public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
                if (reader.TokenType == JsonTokenType.Number && reader.TryGetInt32(out var value))
                    return value;
                if (reader.TokenType == JsonTokenType.String && int.TryParse(reader.GetString(), out var strValue))
                    return strValue;
                throw new JsonException("Invalid appid value");
            }

            public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
                => writer.WriteNumberValue(value);
        }
    }
}
