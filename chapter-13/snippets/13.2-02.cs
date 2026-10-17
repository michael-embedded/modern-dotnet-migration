// Modern .NET, Chapter 13, Section 13.2
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy(), tags: ["live"])
    .AddCheck("config", () =>
        string.IsNullOrWhiteSpace(builder.Configuration["Shop:Name"])
            ? HealthCheckResult.Unhealthy("Shop:Name is not configured")
            : HealthCheckResult.Healthy(), tags: ["ready"]);

var app = builder.Build();

app.MapHealthChecks("/health/live", new() { Predicate = r => r.Tags.Contains("live") });
app.MapHealthChecks("/health/ready", new() { Predicate = r => r.Tags.Contains("ready") });

app.MapGet("/", (IConfiguration config) => new
{
    shop = config["Shop:Name"],
    host = Environment.MachineName,
    runtime = System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription
});

app.Run();
