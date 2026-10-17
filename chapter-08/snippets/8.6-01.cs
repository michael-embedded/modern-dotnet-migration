// Modern .NET, Chapter 8, Section 8.6
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();      // swap for Redis/SQL in production
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();
app.UseSession();

app.MapPost("/cart/{sku}", (string sku, HttpContext ctx) =>
{
    var cart = ctx.Session.GetObject<List<string>>("cart") ?? [];
    cart.Add(sku);
    ctx.Session.SetObject("cart", cart);
    return Results.Ok(cart);
});

app.MapGet("/cart", (HttpContext ctx) => ctx.Session.GetObject<List<string>>("cart") ?? []);

app.Run();

public static class SessionExtensions
{
    public static void SetObject<T>(this ISession session, string key, T value) =>
        session.SetString(key, JsonSerializer.Serialize(value));

    public static T? GetObject<T>(this ISession session, string key) =>
        session.GetString(key) is { } json ? JsonSerializer.Deserialize<T>(json) : default;
}
