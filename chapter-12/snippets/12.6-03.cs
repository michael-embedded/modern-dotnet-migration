// Modern .NET, Chapter 12, Section 12.6
using Microsoft.Extensions.Caching.Hybrid;

// builder.Services.AddHybridCache();
public sealed class ProductLookup(HybridCache cache)
{
    public ValueTask<string> GetNameAsync(int id, CancellationToken ct = default) =>
        cache.GetOrCreateAsync($"product:{id}", async token =>
        {
            await Task.Delay(100, token);                    // pretend: expensive database call
            return $"Product {id}";
        }, new HybridCacheEntryOptions { Expiration = TimeSpan.FromMinutes(5) }, cancellationToken: ct);
}
