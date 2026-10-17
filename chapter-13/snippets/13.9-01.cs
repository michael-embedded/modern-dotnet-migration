// Modern .NET, Chapter 13, Section 13.9
using Microsoft.Extensions.DependencyInjection;

public static class ServiceDefaultsExtensions
{
    public static TBuilder AddServiceDefaults<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        builder.ConfigureOpenTelemetry();                        // logs, metrics, traces (Chapter 11)
        builder.AddDefaultHealthChecks();                        // /health and /alive

        builder.Services.AddServiceDiscovery();                  // resolve "http://apiservice" at runtime
        builder.Services.ConfigureHttpClientDefaults(http =>
        {
            http.AddStandardResilienceHandler();                 // retries, timeouts, circuit breaker (Chapter 7)
            http.AddServiceDiscovery();
        });

        return builder;
    }
}
