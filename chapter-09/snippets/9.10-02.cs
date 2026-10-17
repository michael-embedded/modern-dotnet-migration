// Modern .NET, Chapter 9, Section 9.10
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();
builder.Services.AddApiVersioning(o =>
{
    o.DefaultApiVersion = new ApiVersion(1, 0);
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.ReportApiVersions = true;
    o.ApiVersionReader = new UrlSegmentApiVersionReader();
});
builder.Services.AddSingleton<IShipmentRepository, InMemoryShipmentRepository>();

var app = builder.Build();

app.UseExceptionHandler();
app.UseStatusCodePages();

var versions = app.NewApiVersionSet().HasApiVersion(new ApiVersion(1, 0)).ReportApiVersions().Build();

app.MapGet("/api/v{version:apiVersion}/shipments/{id}",
    async Task<IResult> (string id, IShipmentRepository repo, CancellationToken ct) =>
        await repo.FindAsync(id, ct) is { } shipment
            ? TypedResults.Ok(shipment)
            : TypedResults.Problem(statusCode: 404, title: "Shipment not found", detail: $"No shipment with id '{id}'."))
   .WithApiVersionSet(versions)
   .MapToApiVersion(1, 0);

app.Run();

public sealed record Shipment(string Id, string Status, DateTimeOffset Updated);

public interface IShipmentRepository { Task<Shipment?> FindAsync(string id, CancellationToken ct); }

public sealed class InMemoryShipmentRepository : IShipmentRepository
{
    public Task<Shipment?> FindAsync(string id, CancellationToken ct) =>
        Task.FromResult(id == "1Z999" ? new Shipment(id, "InTransit", DateTimeOffset.UtcNow) : null);
}
