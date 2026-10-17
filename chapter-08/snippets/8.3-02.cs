// Modern .NET, Chapter 8, Section 8.3
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => options.Filters.Add<AuditFilter>());
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddSingleton<AuditFilter>();

var app = builder.Build();

app.UseExceptionHandler();                       // replaces Application_Error
app.MapControllers();                            // replaces RouteConfig / WebApiConfig

var lifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();
lifetime.ApplicationStarted.Register(() => app.Logger.LogInformation("Application started"));
lifetime.ApplicationStopping.Register(() => app.Logger.LogInformation("Application stopping")); // Application_End

app.Run();

public sealed class AuditFilter(ILogger<AuditFilter> logger) : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context) =>
        logger.LogInformation("Executing {Action}", context.ActionDescriptor.DisplayName);

    public void OnActionExecuted(ActionExecutedContext context) { }
}

public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken ct)
    {
        logger.LogError(exception, "Unhandled exception");
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Title = "An unexpected error occurred",
            Status = 500
        }, ct);
        return true;                              // handled
    }
}

[ApiController]
[Route("boom")]
public class BoomController : ControllerBase
{
    [HttpGet] public IActionResult Get() => throw new InvalidOperationException("kaboom");
}
