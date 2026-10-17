// Modern .NET, Chapter 14, Section 14.13
using System.Text.Json;

namespace Equivalence.Tests;

public class QuoteEquivalenceTests
{
    private static readonly HttpClient Legacy = new() { BaseAddress = new Uri("http://localhost:5001") };
    private static readonly HttpClient Modern = new() { BaseAddress = new Uri("http://localhost:5002") };

    public static IEnumerable<object?[]> Grid()
    {
        foreach (var tier in new string?[] { null, "gold", "silver", "platinum" })
        foreach (var amount in new[] { "0", "-5", "10", "99.99", "1000", "1000.01", "5000", "abc", "" })
            yield return [tier, amount];
    }

    [Theory]
    [MemberData(nameof(Grid))]
    public async Task Quote_Matches_Between_Legacy_And_Modern(string? tier, string amount)
    {
        var query = $"/api/quote?amount={Uri.EscapeDataString(amount)}" + (tier is null ? "" : $"&tier={tier}");

        var legacy = await Legacy.GetAsync(query);
        var modern = await Modern.GetAsync(query);

        Assert.Equal(legacy.StatusCode, modern.StatusCode);
        Assert.Equal(Normalize(await legacy.Content.ReadAsStringAsync()), Normalize(await modern.Content.ReadAsStringAsync()));
    }

    private static string Normalize(string json)
    {
        try { using var d = JsonDocument.Parse(json); return JsonSerializer.Serialize(d.RootElement); }
        catch (JsonException) { return json.Trim(); }
    }
}
