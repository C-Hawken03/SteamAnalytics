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
├── SteamAnalytics                  → ASP.NET Core Web API
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

Data Flow:
  - Steam API client retrieves app data.
  - SteamIngestionService processes and stores base game data.
  - GameGenreEnrichmentService enriches genre metadata.
  - Data becomes available through the API layer.
  - Frontend consumes the API for charts/graphs.

Prerequisites To Run:
  - .NET 9 SDK
  - MySQL 8 running locally
  - EF Core CLI tool
    
Running The Project:
1. Clone Repository

  git clone <repo-url>
  cd SteamAnalytics

2. Create Database

  Open MySQL and run:
  CREATE DATABASE SteamAnalytics;

3. Configure Connection String

  In SteamAnalytics.Api/appsettings.json:
  {
    "ConnectionStrings": {
      "DefaultConnection": "server=localhost;database=SteamAnalytics;user=root;password=yourpassword;"
    }
  }

4. Apply Migrations
   
  Run from the solution root:
  dotnet ef database update \
    --project SteamAnalytics.Infrastructure \
    --startup-project SteamAnalytics
    
5. Run the API

  dotnet run --project SteamAnalytics
  
  The API will start at:
  https://localhost:5001


Project Status:
  In active development
  Core ingestion and persistence pipeline complete
  Analytics and visualization layer in progress
