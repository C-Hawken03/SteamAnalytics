using SteamAnalytics.Domain;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace SteamAnalytics.Infrastructure.SteamAPI {
    public class SteamStoreApiClient {
        private readonly HttpClient _http;

        public SteamStoreApiClient(HttpClient http) {
            _http = http;
        }

        public async Task<List<string>> GetGenresAsync(int appId) {
            var url = $"https://store.steampowered.com/api/appdetails?appids={appId}";
            var jsonDoc = await _http.GetFromJsonAsync<JsonElement>(url);

            if (!jsonDoc.TryGetProperty(appId.ToString(), out var appElement))
                return new List<string>();

            if (!appElement.GetProperty("success").GetBoolean())
                return new List<string>();

            if (!appElement.GetProperty("data").TryGetProperty("genres", out var genresElement))
                return new List<string>();

            var genres = new List<string>();
            foreach (var genre in genresElement.EnumerateArray()) {
                if (genre.TryGetProperty("description", out var desc))
                    genres.Add(desc.GetString()!);
            }

            return genres;
        }

        public async Task<int?> GetNumberPlayersAsync(int appId) {
            var url = $"https://api.steampowered.com/ISteamUserStats/GetNumberOfCurrentPlayers/v1/?appid={appId}";
            var jsonDoc = await _http.GetFromJsonAsync<JsonElement>(url);

            if (!jsonDoc.TryGetProperty("response", out var responseElement))
                return null;

            if (!responseElement.TryGetProperty("result", out var resultElement))
                return null;

            if (resultElement.GetInt32() != 1)
                return null;

            if (!responseElement.TryGetProperty("player_count", out var countElement))
                return null;

            return countElement.GetInt32();
        }

    }

}
