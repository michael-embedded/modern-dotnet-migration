// Modern .NET, Chapter 11, Section 11.7
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy(), tags: ["live"])
    .AddCheck<PartnerApiCheck>("partner-api", tags: ["ready"])
    .AddCheck("disk-space", () =>
    {
        var free = new DriveInfo(Path.GetPathRoot(Environment.CurrentDirectory)!).AvailableFreeSpace / 1_000_000_000.0;
        return free > 1 ? HealthCheckResult.Healthy($"{free:F1} GB free")
                        : HealthCheckResult.Degraded($"Only {free:F1} GB free");
    }, tags: ["ready"]);

var app = builder.Build();

app.MapHealthChecks("/health/live", new() { Predicate = r => r.Tags.Contains("live") });
app.MapHealthChecks("/health/ready", new() { Predicate = r => r.Tags.Contains("ready") });

app.Run();

public sealed class PartnerApiCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken ct = default)
    {
        // Real check: a cheap ping to the dependency with a short timeout.
        var reachable = true;
        return Task.FromResult(reachable ? HealthCheckResult.Healthy() : HealthCheckResult.Unhealthy("Partner API unreachable"));
    }
}
