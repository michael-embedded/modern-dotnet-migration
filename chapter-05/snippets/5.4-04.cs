// Modern .NET, Chapter 5, Section 5.4
// In a test project: dotnet add package Microsoft.Extensions.TimeProvider.Testing
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Time.Testing;

public class OrderProcessorTests
{
    [Fact]
    public async Task ProcessAsync_AppliesTaxAndStampsTime()
    {
        var clock = new FakeTimeProvider(new DateTimeOffset(2026, 1, 15, 9, 0, 0, TimeSpan.Zero));
        var repo = new InMemoryOrderRepository();
        await repo.SaveAsync(new Order(1, "ada@example.com", 100m));
        var sut = new OrderProcessor(repo, new ConsoleEmailSender(), clock,
            NullLogger<OrderProcessor>.Instance);

        await sut.ProcessAsync(1);

        var saved = await repo.FindAsync(1);
        Assert.Equal(108m, saved!.Total);
        Assert.Equal(clock.GetUtcNow(), saved.ProcessedAt);
    }
}
