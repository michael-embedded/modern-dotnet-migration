// Modern .NET, Chapter 14, Section 14.13
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddSingleton<QuoteCalculator>();
var app = builder.Build();

app.MapGet("/api/quote", (string? tier, decimal? amount, QuoteCalculator calculator) =>
    amount is null or <= 0
        ? Results.Json(new { success = false, error = "bad amount" })        // legacy contract preserved on purpose
        : Results.Json(new { success = true, total = calculator.Total(amount.Value, tier) }));

app.MapGet("/api/greeting", (TimeProvider clock) =>
    Results.Json(new { message = $"Hello at hour {clock.GetLocalNow().Hour}" }));

app.Run();

public enum Tier { None, Silver, Gold }

public sealed class QuoteCalculator
{
    public decimal Total(decimal amount, string? tierName)
    {
        var tier = tierName switch { "gold" => Tier.Gold, "silver" => Tier.Silver, _ => Tier.None };

        var discount = tier switch { Tier.Gold => 0.15m, Tier.Silver => 0.10m, _ => 0m };
        if (amount > 1000) discount += 0.02m;

        return Math.Round(amount * (1 - discount), 2);
    }
}

public partial class Program;
