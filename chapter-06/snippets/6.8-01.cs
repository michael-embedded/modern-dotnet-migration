// Modern .NET, Chapter 6, Section 6.8
// LEGACY
public string GetQuote(string symbol)
{
    using var client = new WebClient();
    return client.DownloadString("https://api.example.com/quote/" + symbol);
}

// MODERN
public async Task<string> GetQuoteAsync(string symbol, CancellationToken ct = default)
{
    var response = await _http.GetAsync($"quote/{symbol}", ct);
    response.EnsureSuccessStatusCode();
    return await response.Content.ReadAsStringAsync(ct);
}
