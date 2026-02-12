using Microsoft.EntityFrameworkCore;
using SteamAnalytics.Application;
using SteamAnalytics.Infrastructure;
using SteamAnalytics.Infrastructure.Persistence;
using SteamAnalytics.Infrastructure.SteamAPI;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Repositories
builder.Services.AddScoped<IGameRepository, GameRepository>();

// DbContext
builder.Services.AddDbContext<SteamAnalyticsDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(
            builder.Configuration.GetConnectionString("DefaultConnection")),
        b => b.MigrationsAssembly("SteamAnalytics.Infrastructure")
    )
);

// Game catalog (JSON ONLY)
builder.Services.AddSingleton<IGameCatalogSource>(sp =>
    new JsonGameCatalogSource(sp.GetRequiredService<IHostEnvironment>())
);


// Steam APIs
builder.Services.AddHttpClient<SteamStoreApiClient>();

// Background services
builder.Services.AddHostedService<GameCatalogIngestionService>();
builder.Services.AddHostedService<GameGenreEnrichmentService>();
builder.Services.AddHostedService<PlayerCountPollingService>();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Serve static files (wwwroot)
app.UseDefaultFiles();
app.UseStaticFiles();

// Map API controllers
app.MapControllers();

app.Run();
