// Modern .NET, Chapter 6, Section 6.7
public sealed class RateLimitedClient(HttpClient http)
{
    private readonly SemaphoreSlim _gate = new(initialCount: 3);   // at most 3 concurrent calls

    public async Task<string> GetAsync(string url, CancellationToken ct = default)
    {
        await _gate.WaitAsync(ct);
        try
        {
            return await http.GetStringAsync(url, ct);
        }
        finally
        {
            _gate.Release();                    // always release, even on exceptions
        }
    }
}
