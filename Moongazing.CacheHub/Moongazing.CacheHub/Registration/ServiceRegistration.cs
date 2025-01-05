using Enyim.Caching;
using Enyim.Caching.Configuration;
using Hangfire;
using Moongazing.CacheHub.Entites;
using Moongazing.CacheHub.Monitoring;
using Moongazing.CacheHub.Providers;
using Moongazing.CacheHub.Services;
using StackExchange.Redis;

namespace Moongazing.CacheHub.Registration;

public static class ServiceRegistration
{
    public static IServiceCollection AddDistributedCaching(this IServiceCollection services, IConfiguration configuration)
    {
        var cacheConfig = configuration.GetSection("CacheConfiguration").Get<CacheConfiguration>();

        if (cacheConfig!.Provider == "Redis")
        {
            services.AddSingleton<IConnectionMultiplexer>(sp =>
                ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!));
            services.AddScoped<ICacheProvider, RedisCacheProvider>();
        }
        else if (cacheConfig.Provider == "Memcached")
        {
            services.AddSingleton<IMemcachedClient>(sp =>
                new MemcachedClient(new MemcachedClientConfiguration()));
            services.AddScoped<ICacheProvider, MemcachedProvider>();
        }

        services.AddScoped<ICacheService, CacheService>();
        services.AddScoped<IMetricsService, MetricsService>();
        services.AddScoped<IReportService, ReportService>();
        services.AddSingleton<PrometheusMetricsExporter>();
        services.AddHangfire(x => x.UseSqlServerStorage(configuration.GetConnectionString("Hangfire")));
        services.AddHangfireServer();

        return services;
    }
}