// Modern .NET, Chapter 5, Section 5.10
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.Configure<NotificationOptions>(o =>
{
    o.FromAddress = "noreply@example.com";
    o.Subject = "Shipped";
});
builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddSingleton<IMessageChannel, ConsoleChannel>();
builder.Services.AddTransient<ShipmentNotifier>();

using var host = builder.Build();

await host.Services.GetRequiredService<ShipmentNotifier>()
    .NotifyAsync(orderId: 42, customerEmail: "grace@example.com");

public sealed class NotificationOptions
{
    public string FromAddress { get; set; } = "";
    public string Subject { get; set; } = "";
}

public interface IMessageChannel
{
    Task SendAsync(string from, string to, string subject, string body, CancellationToken ct);
}

public sealed class ConsoleChannel : IMessageChannel
{
    public Task SendAsync(string from, string to, string subject, string body, CancellationToken ct)
    {
        Console.WriteLine($"[{from} -> {to}] {subject}: {body}");
        return Task.CompletedTask;
    }
}

public sealed class ShipmentNotifier(
    IMessageChannel channel,
    IOptions<NotificationOptions> options,
    TimeProvider clock,
    ILogger<ShipmentNotifier> logger)
{
    public async Task NotifyAsync(int orderId, string customerEmail, CancellationToken ct = default)
    {
        var opts = options.Value;
        var body = $"Your order {orderId} shipped at {clock.GetLocalNow():g}";

        await channel.SendAsync(opts.FromAddress, customerEmail, opts.Subject, body, ct);

        logger.LogInformation("Notified customer for order {OrderId}", orderId);
    }
}
