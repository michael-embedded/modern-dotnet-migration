// Modern .NET, Chapter 4, Section 4.6
public sealed class InvoiceService(IClock clock, ILogger<InvoiceService> logger)
{
    public Invoice Create(decimal amount)
    {
        logger.LogInformation("Creating invoice for {Amount}", amount);
        return new Invoice(Guid.NewGuid(), amount, clock.UtcNow);
    }
}

public interface IClock { DateTimeOffset UtcNow { get; } }
public sealed record Invoice(Guid Id, decimal Amount, DateTimeOffset CreatedAt);
