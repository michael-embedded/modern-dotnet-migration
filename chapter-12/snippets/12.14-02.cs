// Modern .NET, Chapter 12, Section 12.14
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<RateProvider>();
var app = builder.Build();

var products = Enumerable.Range(1, 2_000).Select(i => new Product(i, $"Product {i}", 10 + i % 90, i % 3 == 0 ? "EUR" : "USD")).ToArray();

// SLOW version
app.MapGet("/catalog/slow", async (RateProvider rates) =>
{
    var sb = "";
    foreach (var p in products)
    {
        var rate = await rates.GetRateAsync(p.Currency);            // PROBLEM 1: same lookup repeated 2,000 times
        sb += $"{p.Name},{Math.Round(p.Price * rate, 2)}\n";        // PROBLEM 2: quadratic string concatenation
    }
    return Results.Text(sb);
});

// FAST version
app.MapGet("/catalog/fast", async (RateProvider rates) =>
{
    var lookup = new Dictionary<string, decimal>
    {
        ["USD"] = await rates.GetRateAsync("USD"),                  // FIX 1: resolve each distinct currency once
        ["EUR"] = await rates.GetRateAsync("EUR")
    };

    var sb = new StringBuilder(products.Length * 24);               // FIX 2: single buffer
    foreach (var p in products)
        sb.Append(p.Name).Append(',').Append(Math.Round(p.Price * lookup[p.Currency], 2)).Append('\n');
    return Results.Text(sb.ToString());
});

app.Run();

public sealed record Product(int Id, string Name, decimal Price, string Currency);

public sealed class RateProvider
{
    public async Task<decimal> GetRateAsync(string currency)
    {
        await Task.Delay(1);                                        // pretend: a remote call
        return currency == "EUR" ? 0.92m : 1m;
    }
}
