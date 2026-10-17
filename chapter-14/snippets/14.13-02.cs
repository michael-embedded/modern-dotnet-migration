// Modern .NET, Chapter 14, Section 14.13
var app = WebApplication.CreateBuilder(args).Build();

app.MapGet("/api/quote", (string? tier, decimal? amount) =>
{
    if (amount == null || amount <= 0) return Results.Json(new { success = false, error = "bad amount" });

    var discount = 0m;
    if (tier == "gold") discount = 0.15m;
    else if (tier == "silver") discount = 0.10m;
    if (amount > 1000) discount += 0.02m;

    var total = Math.Round(amount.Value * (1 - discount), 2);
    return Results.Json(new { success = true, total });
});

app.MapGet("/api/greeting", () => Results.Json(new { message = "Hello at hour " + DateTime.Now.Hour }));

app.Run();

public partial class Program;
