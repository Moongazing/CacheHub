using Hangfire;
using Moongazing.CacheHub.Jobs;
using Moongazing.CacheHub.Registration;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDistributedCaching(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
RecurringJob.AddOrUpdate<MetricsCollectorJob>("metrics-collector", job => job.ExecuteAsync(), Cron.Minutely);
RecurringJob.AddOrUpdate<CacheCleanupJob>("cache-cleanup", job => job.ExecuteAsync(), Cron.Daily);


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
