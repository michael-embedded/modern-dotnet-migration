// Modern .NET, Chapter 5, Section 5.8
using (logger.BeginScope(new Dictionary<string, object> { ["CorrelationId"] = correlationId }))
{
    logger.LogInformation("Starting import");
    // every log line inside this block carries CorrelationId
}
