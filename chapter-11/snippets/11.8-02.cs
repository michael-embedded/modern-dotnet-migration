// Modern .NET, Chapter 11, Section 11.8
using System.Diagnostics;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenTelemetry()
    .ConfigureResource(r => r.AddService("traced-api", serviceVersion: "1.0.0"))
    .WithTracing(t => t
        .AddAspNetCoreInstrumentation()           // a span per incoming request
        .AddHttpClientInstrumentation()           // a span per outgoing HttpClient call
        .AddSource(Telemetry.SourceName)          // our own spans
        .AddConsoleExporter())
    .WithMetrics(m => m
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddMeter("Shop.Orders")
        .AddConsoleExporter());

builder.Services.AddHttpClient("self", c => c.BaseAddress = new Uri("http://localhost:5100"));

var app = builder.Build();
app.Urls.Add("http://localhost:5100");

app.MapGet("/checkout/{id:int}", async (int id, IHttpClientFactory http) =>
{
    using var activity = Telemetry.Source.StartActivity("ValidateCart");
    activity?.SetTag("order.id", id);

    await Task.Delay(30);                                       // pretend: validate the cart

    var client = http.CreateClient("self");
    var stock = await client.GetStringAsync($"/inventory/{id}"); // traced HTTP hop

    return Results.Ok(new { id, stock });
});

app.MapGet("/inventory/{id:int}", async (int id) =>
{
    using var activity = Telemetry.Source.StartActivity("QueryStock");
    activity?.SetTag("db.system", "sqlite");
    await Task.Delay(80);                                       // pretend: database call
    if (id == 13) { activity?.SetStatus(ActivityStatusCode.Error, "out of stock"); }
    return $"{id * 10} in stock";
});

app.Run();

public static class Telemetry
{
    public const string SourceName = "TracedApi";
    public static readonly ActivitySource Source = new(SourceName);
}
