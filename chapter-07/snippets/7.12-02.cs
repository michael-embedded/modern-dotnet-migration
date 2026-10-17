// Modern .NET, Chapter 7, Section 7.12
#pragma warning disable SYSLIB0014   // WebClient is obsolete; kept only as a behavioral reference
using System.Globalization;
using System.Net;
using System.Text.Json;

public sealed class LegacyRateClient(string baseUrl)
{
    public decimal Convert(decimal amount, string from, string to)
    {
        using var client = new WebClient();
        var json = client.DownloadString($"{baseUrl}rates/{from}/{to}");
        using var doc = JsonDocument.Parse(json);
        var rate = doc.RootElement.GetProperty("rate").GetDecimal();
        return Math.Round(amount * rate, 2);
    }
}
