// Modern .NET, Chapter 11, Section 11.4
[Fact]
public async Task New_Matches_Legacy_For_Prices()
{
    var legacyBase = Environment.GetEnvironmentVariable("LEGACY_BASE_URL");
    if (legacyBase is null) return;       // or use Skip via a custom fact attribute

    using var legacy = new HttpClient { BaseAddress = new Uri(legacyBase) };
    var modern = factory.CreateClient();

    foreach (var sku in new[] { "pen", "BOOK", "nope" })
    {
        var l = await legacy.GetAsync($"/prices/{sku}");
        var m = await modern.GetAsync($"/prices/{sku}");
        Assert.Equal(l.StatusCode, m.StatusCode);
        if (l.IsSuccessStatusCode)
            Assert.Equal(await l.Content.ReadAsStringAsync(), await m.Content.ReadAsStringAsync());
    }
}
