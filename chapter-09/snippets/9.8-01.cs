// Modern .NET, Chapter 9, Section 9.8
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    // Global: 100 requests per minute per client IP (or per user when authenticated)
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(ctx =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: ctx.User.Identity?.Name ?? ctx.Connection.RemoteIpAddress?.ToString() ?? "anonymous",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 100,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0
            }));

    // Named policy: token bucket for writes, allowing short bursts
    options.AddTokenBucketLimiter("writes", o =>
    {
        o.TokenLimit = 10;
        o.TokensPerPeriod = 2;
        o.ReplenishmentPeriod = TimeSpan.FromSeconds(1);
        o.QueueLimit = 0;
    });

    options.OnRejected = async (ctx, ct) =>
    {
        if (ctx.Lease.TryGetMetadata(MetaData.RetryAfter, out var retryAfter))
            ctx.HttpContext.Response.Headers.RetryAfter = ((int)retryAfter.TotalSeconds).ToString();

        await ctx.HttpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = 429,
            Title = "Too many requests",
            Detail = "Slow down and retry later."
        }, ct);
    };
});

// after Build(), before UseAuthentication so abusive anonymous traffic is cheap to reject:
app.UseRateLimiter();

// apply to endpoints:
products.MapPost("/", ...).RequireRateLimiting("writes");
