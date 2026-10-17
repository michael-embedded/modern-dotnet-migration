// Modern .NET, Chapter 14, Section 14.13
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

app.MapGet("/gateway/status", () => new { status = "up", time = DateTimeOffset.UtcNow });
app.MapReverseProxy();

app.Run();
