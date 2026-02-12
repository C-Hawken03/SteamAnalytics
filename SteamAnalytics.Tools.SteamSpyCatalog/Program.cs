using System.Text.Json;
using System.Text.Json.Serialization;

Console.WriteLine("=== SteamSpy Catalog Generator ===");
// --------------------------------------------------
// Resolve solution root (robust)
// --------------------------------------------------
string FindSolutionRoot() {
    var dir = AppContext.BaseDirectory;

    while (dir != null && !Directory.EnumerateFiles(dir, "*.sln").Any()) {
        dir = Directory.GetParent(dir)?.FullName;
    }

    if (dir == null)
        throw new InvalidOperationException(
            "Could not locate solution root (.sln file not found)."
        );

    return dir;
}

var solutionRoot = FindSolutionRoot();

var outputPath = Path.Combine(
    solutionRoot,
    "SteamAnalytics",
    "Data",
    "app.json"
);

Console.WriteLine($"Solution root : {solutionRoot}");
Console.WriteLine($"Output path   : {outputPath}");

// --------------------------------------------------
// Fetch SteamSpy data
// --------------------------------------------------
Console.WriteLine("Fetching SteamSpy data...");

using var http = new HttpClient {
    Timeout = TimeSpan.FromSeconds(30)
};

string rawJson;
try {
    rawJson = await http.GetStringAsync(
        "https://steamspy.com/api.php?request=all"
    );
} catch (Exception ex) {
    Console.Error.WriteLine("Failed to fetch SteamSpy data");
    Console.Error.WriteLine(ex.Message);
    return;
}

// --------------------------------------------------
// Deserialize
// --------------------------------------------------
var allGames = JsonSerializer.Deserialize<
    Dictionary<string, SteamSpyGame>
>(rawJson);

if (allGames == null || allGames.Count == 0) {
    Console.Error.WriteLine("SteamSpy response was empty or invalid.");
    return;
}

Console.WriteLine($"Loaded {allGames.Count:N0} games");

// --------------------------------------------------
// Select top 10,000 by CCU
// --------------------------------------------------
var topGames = allGames.Values
    .Where(g => g.CCU > 0)
    .OrderByDescending(g => g.CCU)
    .Take(10_000)
    .Select(g => new {
        appid = g.AppId,
        name = g.Name
    })
    .ToList();

Console.WriteLine($"Selected {topGames.Count:N0} games");

// --------------------------------------------------
// Write app.json
// --------------------------------------------------
Directory.CreateDirectory(Path.GetDirectoryName(outputPath)!);

var outputJson = JsonSerializer.Serialize(
    topGames,
    new JsonSerializerOptions {
        WriteIndented = true
    }
);

await File.WriteAllTextAsync(outputPath, outputJson);

Console.WriteLine("✔ app.json written successfully");
Console.WriteLine("Done.");
