// Modern .NET, Chapter 9, Section 9.6
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = ctx =>
    {
        ctx.ProblemDetails.Extensions["traceId"] = System.Diagnostics.Activity.Current?.Id ?? ctx.HttpContext.TraceIdentifier;
    };
});
builder.Services.AddExceptionHandler<ApiExceptionHandler>();

// after Build(), first in the pipeline:
app.UseExceptionHandler();
app.UseStatusCodePages();       // 404/405 and other empty responses become ProblemDetails too
