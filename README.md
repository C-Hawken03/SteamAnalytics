Steam Analytics:
  A .NET-based data ingestion and analytics platform for analyzing Steam game data.
  The project collects data from the Steam API, stores it in MySQL, enriches it, and prepares it for analytics and visualization.

Steam Analytics is designed to:
  - Ingest game data from the Steam Store API
  - Store structured data in MySQL
  - Enrich and normalize game metadata (genres, tags, etc.)
  - Prepare data for visualization and graph-based analysis
  - Serve data to a web frontend for dashboards and infographics

The solution is structured as follows:

SteamAnalytics
│
├── SteamAnalytics.Api              → ASP.NET Core Web API
├── SteamAnalytics.Application      → Interfaces & business logic
├── SteamAnalytics.Domain           → Core domain models
├── SteamAnalytics.Infrastructure   → EF Core, MySQL, Steam API integration

Technologies Used:
  - .NET
  - ASP.NET Core
  - Entity Framework Core
  - MySQL
  - Steam Store API
  - Background Services (IHostedService)
  - Dependency Injection

Database:
  The project uses MySQL as the primary data store. Migrations are handled using Entity Framework Core.

Run migrations:
  dotnet ef database update --project SteamAnalytics.Infrastructure --startup-project SteamAnalytics.Api

Data Flow:
  - Steam API client retrieves app data.
  - SteamIngestionService processes and stores base game data.
  - GameGenreEnrichmentService enriches genre metadata.
  - Data becomes available through the API layer.
  - Frontend consumes the API for charts/graphs.

Configuration:
  - Update appsettings.json in the API project
  {
    "ConnectionStrings": {
      "DefaultConnection": "server=localhost;database=SteamAnalytics;user=root;password=yourpassword;"
    }
  }

Running the Project:
  1. Clone the repository
  2. Configure MySQL connection string
  3. Apply migrations
  4. Run the API via:
    dotnet run --project SteamAnalytics

Project Status:
  In active development
  Core ingestion and persistence pipeline complete
  Analytics and visualization layer in progress
