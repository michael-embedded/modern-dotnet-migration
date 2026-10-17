// Modern .NET, Chapter 8, Section 8.2
// LEGACY (for reference)
// public class TimingModule : IHttpModule {
//     public void Init(HttpApplication app) {
//         app.BeginRequest += (s, e) => HttpContext.Current.Items["start"] = Stopwatch.GetTimestamp();
//         app.EndRequest   += (s, e) => { ... HttpContext.Current.Response.Headers.Add("X-Elapsed", ...); };
//     }
// }

// MODERN
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<RequestTimingMiddleware>();
var app = builder.Build();

app.UseMiddleware<RequestTimingMiddleware>();
app.MapGet("/", async () => { await Task.Delay(50); return "ok"; });
app.Run();

public sealed class RequestTimingMiddleware(ILogger<RequestTimingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var start = Stopwatch.GetTimestamp();

        context.Response.OnStarting(() =>
        {
            var elapsed = Stopwatch.GetElapsedTime(start);
            context.Response.Headers["X-Elapsed-Ms"] = elapsed.TotalMilliseconds.ToString("F1");
            return Task.CompletedTask;
        });

        await next(context);
        logger.LogInformation("{Method} {Path} took {Elapsed:F1} ms",
            context.Request.Method, context.Request.Path, Stopwatch.GetElapsedTime(start).TotalMilliseconds);
    }
}
