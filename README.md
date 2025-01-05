CacheHub

CacheHub is a Distributed Caching Management System built with .NET 9. It provides a flexible, extensible, and high-performance caching solution using Redis, Memcached, and Hangfire. The system also includes metrics tracking with Prometheus, reporting, and log management with Serilog.
Features

    Multi-Provider Support:
        Choose between Redis and Memcached as the caching provider.
    Metrics Tracking:
        Track cache performance, including total keys, hits, and misses.
        Export metrics to Prometheus for advanced monitoring.
    Cache Management:
        Add, retrieve, remove, and clear cache entries with ease.
    Task Scheduling:
        Automatic cache cleanup using Hangfire.
    Logging:
        Integrated with Serilog for detailed application logs.
    Reporting:
        Generate reports on frequently accessed cache keys.

Technologies Used

    Backend: .NET 9, ASP.NET Core
    Cache: Redis, Memcached
    Task Scheduler: Hangfire
    Database: MSSQL (for Hangfire tasks)
    Monitoring: Prometheus, Grafana

Project Structure

- CacheHub
  - Core
    - Interfaces
    - Models
  - Providers
    - RedisCacheProvider.cs
    - MemcachedProvider.cs
  - Services
    - CacheService.cs
    - MetricsService.cs
    - ReportService.cs
  - BackgroundJobs
    - CacheCleanupJob.cs
    - MetricsCollectorJob.cs
  - Extensions
    - ServiceRegistration.cs
  - Controllers
    - CacheController.cs
    - MetricsController.cs
    - ReportController.cs
  - Monitoring
    - PrometheusMetricsExporter.cs
  - Program.cs
  - appsettings.json

Installation
Prerequisites

    Install .NET 8 SDK.
    Set up Redis or Memcached on your system.
    Install Prometheus and Grafana for metrics visualization (optional).

Setup

    Clone the repository:

git clone https://github.com/your-repo/cachehub.git
cd cachehub

Configure appsettings.json:

{
  "CacheConfiguration": {
    "Provider": "Redis", 
    "DefaultExpirationMinutes": 60
  },
  "Redis": {
    "ConnectionString": "localhost:6379"
  },
  "Memcached": {
    "Servers": [
      { "Address": "127.0.0.1", "Port": 11211 }
    ]
  },
  "Hangfire": {
    "ConnectionString": "Server=localhost;Database=HangfireDb;User Id=sa;Password=your_password;"
  }
}

Restore packages and build the solution:

dotnet restore
dotnet build

Run the application:

    dotnet run

Usage
Endpoints
Cache Operations

    Set Cache:
    POST /api/cache/set
    Body:

    {
      "key": "exampleKey",
      "value": "exampleValue",
      "expiration": "00:30:00"
    }

    Get Cache:
    GET /api/cache/get/{key}

    Remove Cache:
    DELETE /api/cache/remove/{key}

    Clear All Cache:
    POST /api/cache/clear

Metrics

    Get Cache Metrics:
    GET /api/metrics

Reporting

    Get Top Accessed Keys:
    GET /api/report/top-accessed?topN=10

Prometheus Integration

To enable Prometheus integration, add the following configuration to your prometheus.yml file:

scrape_configs:
  - job_name: 'cachehub'
    static_configs:
      - targets: ['localhost:5000']

Run Prometheus and Grafana to monitor cache performance metrics like hits, misses, and total keys.
Extendability

    Add a New Cache Provider:
        Implement the ICacheProvider interface for the new provider.
        Register the provider in ServiceRegistration.cs.

    Custom Metrics or Reporting:
        Add new methods in MetricsService or ReportService.
        Create a new API endpoint for the feature.

    Custom Cleanup Tasks:
        Add a new Hangfire job in the BackgroundJobs folder.

Contributing

We welcome contributions! Here's how you can get started:

    Fork the repository.
    Create a new branch for your feature or bug fix.
    Commit your changes with clear messages.
    Submit a pull request.

License

This project is licensed under the MIT License. See the LICENSE file for details.

