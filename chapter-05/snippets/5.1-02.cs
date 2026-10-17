// Modern .NET, Chapter 5, Section 5.1
public sealed class OrderProcessor(
    IOrderRepository orders,
    IEmailSender email,
    TimeProvider clock,
    IOptions<PricingOptions> pricing,
    ILogger<OrderProcessor> logger)
{
    public async Task ProcessAsync(int orderId, CancellationToken ct = default)
    {
        // every dependency is visible in the constructor
    }
}
