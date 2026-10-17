// Modern .NET, Chapter 9, Section 9.2
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/health-check", () => Results.Ok(new { status = "alive" }));
app.MapGet("/greet/{name}", (string name) => $"Hello, {name}!");

app.Run();
