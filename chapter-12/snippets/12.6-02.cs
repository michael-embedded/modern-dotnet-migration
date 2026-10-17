// Modern .NET, Chapter 12, Section 12.6
public sealed class CachedPriceService(Func<string, Task<decimal>> loader)
{
    private readonly Dictionary<string, decimal> _cache = [];

    public ValueTask<decimal> GetAsync(string sku) =>
        _cache.TryGetValue(sku, out var price)
            ? new ValueTask<decimal>(price)                                 // no Task allocation on hits
            : new ValueTask<decimal>(LoadAsync(sku));

    private async Task<decimal> LoadAsync(string sku)
    {
        var price = await loader(sku);
        _cache[sku] = price;                                                // not thread-safe: demo only
        return price;
    }
}
