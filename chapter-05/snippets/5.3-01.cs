// Modern .NET, Chapter 5, Section 5.3
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddSingleton<SingletonService>();
services.AddScoped<ScopedService>();
services.AddTransient<TransientService>();

using var provider = services.BuildServiceProvider();

Console.WriteLine("--- Scope 1 ---");
using (var scope = provider.CreateScope())
{
    Show(scope.ServiceProvider, "first resolve ");
    Show(scope.ServiceProvider, "second resolve");
}

Console.WriteLine("--- Scope 2 ---");
using (var scope = provider.CreateScope())
{
    Show(scope.ServiceProvider, "first resolve ");
}

static void Show(IServiceProvider sp, string label)
{
    Console.WriteLine(
        $"{label}: singleton={sp.GetRequiredService<SingletonService>().Id}  " +
        $"scoped={sp.GetRequiredService<ScopedService>().Id}  " +
        $"transient={sp.GetRequiredService<TransientService>().Id}");
}

public abstract class Tagged { public string Id { get; } = Guid.NewGuid().ToString("N")[..6]; }
public sealed class SingletonService : Tagged;
public sealed class ScopedService : Tagged;
public sealed class TransientService : Tagged;
