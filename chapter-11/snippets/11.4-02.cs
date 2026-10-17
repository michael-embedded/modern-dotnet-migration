// Modern .NET, Chapter 11, Section 11.4
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IPriceService, PriceService>();

var app = builder.Build();

app.MapGet("/prices/{sku}", (string sku, IPriceService prices) =>
    prices.Find(sku) is { } price ? Results.Ok(new { sku, price }) : Results.NotFound());

app.Run();

public interface IPriceService { decimal? Find(string sku); }

public sealed class PriceService : IPriceService
{
    private static readonly Dictionary<string, decimal> Prices = new() { ["PEN"] = 1.50m, ["BOOK"] = 12m };
    public decimal? Find(string sku) => Prices.TryGetValue(sku.ToUpperInvariant(), out var p) ? p : null;
}

public partial class Program;     // exposes the entry point to the test project
