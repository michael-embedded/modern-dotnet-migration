// Modern .NET, Chapter 5, Section 5.8
using Microsoft.Extensions.Logging;

public static partial class Log
{
    [LoggerMessage(EventId = 1001, Level = LogLevel.Information,
        Message = "Order {OrderId} processed in {ElapsedMs} ms")]
    public static partial void OrderProcessed(ILogger logger, int orderId, long elapsedMs);

    [LoggerMessage(EventId = 2001, Level = LogLevel.Warning,
        Message = "Retrying payment for order {OrderId}, attempt {Attempt}")]
    public static partial void PaymentRetry(ILogger logger, int orderId, int attempt);
}

// Usage:
// Log.OrderProcessed(logger, order.Id, stopwatch.ElapsedMilliseconds);
