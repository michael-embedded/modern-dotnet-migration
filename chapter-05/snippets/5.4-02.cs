// Modern .NET, Chapter 5, Section 5.4
using Microsoft.Extensions.Logging;

public sealed class OrderProcessor(
    IOrderRepository orders,
    IEmailSender email,
    TimeProvider clock,
    ILogger<OrderProcessor> logger)
{
    private const decimal TaxRate = 0.08m;

    public async Task ProcessAsync(int orderId, CancellationToken ct = default)
    {
        var order = await orders.FindAsync(orderId, ct)
            ?? throw new InvalidOperationException($"Order {orderId} not found.");

        var processed = order with
        {
            Total = Math.Round(order.Total * (1 + TaxRate), 2),
            ProcessedAt = clock.GetUtcNow()
        };

        await orders.SaveAsync(processed, ct);
        await email.SendAsync(processed.CustomerEmail, "Order processed", "Thanks!", ct);

        logger.LogInformation("Processed order {OrderId} total {Total}", orderId, processed.Total);
    }
}
