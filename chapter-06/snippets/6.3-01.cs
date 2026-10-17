// Modern .NET, Chapter 6, Section 6.3
public sealed class CachedLookup(Func<int, Task<string>> loader)
{
    private readonly Dictionary<int, string> _cache = [];

    public ValueTask<string> GetAsync(int id)
    {
        if (_cache.TryGetValue(id, out var hit))
            return ValueTask.FromResult(hit);      // synchronous path, no allocation

        return new ValueTask<string>(LoadAndCacheAsync(id));
    }

    private async Task<string> LoadAndCacheAsync(int id)
    {
        var value = await loader(id);
        _cache[id] = value;
        return value;
    }
}
