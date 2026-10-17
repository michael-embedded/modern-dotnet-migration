// Modern .NET, Chapter 9, Section 9.14
using Asp.Versioning;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();
builder.Services.AddSingleton<IShipmentService, ShipmentService>();
builder.Services.AddApiVersioning(o =>
{
    o.DefaultApiVersion = new ApiVersion(1, 0);
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.ReportApiVersions = true;
    o.ApiVersionReader = new UrlSegmentApiVersionReader();
});

var app = builder.Build();
app.UseExceptionHandler();
app.UseStatusCodePages();

var versions = app.NewApiVersionSet().HasApiVersion(1, 0).HasApiVersion(2, 0).ReportApiVersions().Build();

// ---- v1: frozen legacy contract (do not "fix" it; clients depend on it) ----
app.MapGet("/api/v{version:apiVersion}/shipments/{id}", (string id, string? apiKey, HttpContext ctx, IShipmentService svc) =>
{
    ctx.Response.Headers["Deprecation"] = "true";                       // announce the sunset
    ctx.Response.Headers["Link"] = "</api/v2/shipments>; rel=\"successor-version\"";

    if (apiKey != "legacy-demo-key")
        return Results.Ok(new { success = false, error = "bad key" });  // legacy quirk preserved

    return svc.Find(id) is { } s
        ? Results.Ok(new { success = true, data = new { s.Id, s.Status } })
        : Results.Ok(new { success = false, error = "not found" });
}).WithApiVersionSet(versions).MapToApiVersion(1, 0);

// ---- v2: modern contract ----
app.MapGet("/api/v{version:apiVersion}/shipments/{id}", (string id, IShipmentService svc) =>
    svc.Find(id) is { } s
        ? Results.Ok(new ShipmentV2(s.Id, s.Status, s.Updated))
        : Results.Problem(statusCode: 404, title: "Shipment not found"))
   .WithApiVersionSet(versions).MapToApiVersion(2, 0).RequireAuthorization();

app.Run();

public sealed record Shipment(string Id, string Status, DateTimeOffset Updated);
public sealed record ShipmentV2(string Id, string Status, DateTimeOffset UpdatedAt);

public interface IShipmentService { Shipment? Find(string id); }
public sealed class ShipmentService : IShipmentService
{
    public Shipment? Find(string id) => id == "1Z999" ? new(id, "InTransit", DateTimeOffset.UtcNow) : null;
}
