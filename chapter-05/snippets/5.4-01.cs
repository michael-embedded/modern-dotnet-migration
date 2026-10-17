// Modern .NET, Chapter 5, Section 5.4
public sealed record Order(int Id, string CustomerEmail, decimal Total)
{
    public DateTimeOffset? ProcessedAt { get; init; }
}

public interface IOrderRepository
{
    Task<Order?> FindAsync(int id, CancellationToken ct = default);
    Task SaveAsync(Order order, CancellationToken ct = default);
}

public interface IEmailSender
{
    Task SendAsync(string to, string subject, string body, CancellationToken ct = default);
}
