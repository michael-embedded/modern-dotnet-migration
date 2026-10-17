// Modern .NET, Chapter 8, Section 8.2
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Use(async (context, next) =>
{
    Console.WriteLine($"-> {context.Request.Method} {context.Request.Path}");
    await next(context);                                    // hand off to the next middleware
    Console.WriteLine($"<- {context.Response.StatusCode}");
});

app.Use(async (context, next) =>
{
    if (context.Request.Path.StartsWithSegments("/blocked"))
    {
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        await context.Response.WriteAsync("Blocked by middleware");
        return;                                             // short-circuit: next is never called
    }
    await next(context);
});

app.MapGet("/", () => "Home");
app.MapGet("/blocked/secret", () => "You will never see this");

app.Run();
