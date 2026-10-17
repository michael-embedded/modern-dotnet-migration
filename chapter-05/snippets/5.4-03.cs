// Modern .NET, Chapter 5, Section 5.4
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddSingleton<IOrderRepository, InMemoryOrderRepository>();
builder.Services.AddSingleton<IEmailSender, ConsoleEmailSender>();
builder.Services.AddTransient<OrderProcessor>();

using var host = builder.Build();

var repo = host.Services.GetRequiredService<IOrderRepository>();
await repo.SaveAsync(new Order(1, "ada@example.com", 100m));

var processor = host.Services.GetRequiredService<OrderProcessor>();
await processor.ProcessAsync(1);

public sealed class InMemoryOrderRepository : IOrderRepository
{
    private readonly Dictionary<int, Order> _store = [];

    public Task<Order?> FindAsync(int id, CancellationToken ct = default) =>
        Task.FromResult(_store.GetValueOrDefault(id));

    public Task SaveAsync(Order order, CancellationToken ct = default)
    {
        _store[order.Id] = order;
        return Task.CompletedTask;
    }
}

public sealed class ConsoleEmailSender : IEmailSender
{
    public Task SendAsync(string to, string subject, string body, CancellationToken ct = default)
    {
        Console.WriteLine($"EMAIL to {to}: {subject} - {body}");
        return Task.CompletedTask;
    }
}
