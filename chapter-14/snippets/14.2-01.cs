// Modern .NET, Chapter 14, Section 14.2
// dotnet new console -n ContractFingerprint
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

var baseUrl = args.Length > 0 ? args[0] : "https://example.com";
var urlsFile = args.Length > 1 ? args[1] : "urls.txt";
var outFile = args.Length > 2 ? args[2] : "fingerprint.json";
var compare = args.Contains("--compare");

if (!File.Exists(urlsFile)) File.WriteAllLines(urlsFile, ["/", "/robots.txt"]);

using var http = new HttpClient { BaseAddress = new Uri(baseUrl), Timeout = TimeSpan.FromSeconds(15) };
var current = new SortedDictionary<string, Fingerprint>();

foreach (var path in File.ReadAllLines(urlsFile).Where(l => !string.IsNullOrWhiteSpace(l)))
{
    try
    {
        using var response = await http.GetAsync(path);
        var body = await response.Content.ReadAsStringAsync();
        current[path] = new Fingerprint(
            (int)response.StatusCode,
            response.Content.Headers.ContentType?.MediaType ?? "",
            Hash(Normalize(body)));
    }
    catch (Exception ex)
    {
        current[path] = new Fingerprint(0, ex.GetType().Name, "");
    }
}

if (!compare || !File.Exists(outFile))
{
    File.WriteAllText(outFile, JsonSerializer.Serialize(current, new JsonSerializerOptions { WriteIndented = true }));
    Console.WriteLine($"Saved {current.Count} fingerprints to {outFile}");
    return 0;
}

var saved = JsonSerializer.Deserialize<SortedDictionary<string, Fingerprint>>(File.ReadAllText(outFile))!;
var differences = 0;
foreach (var (path, now) in current)
{
    if (!saved.TryGetValue(path, out var before) || before != now)
    {
        differences++;
        Console.WriteLine($"CHANGED {path}: {(saved.TryGetValue(path, out var b) ? $"{b.Status}/{b.ContentType}" : "new")} -> {now.Status}/{now.ContentType}");
    }
}
Console.WriteLine(differences == 0 ? "No differences." : $"{differences} difference(s).");
return differences == 0 ? 0 : 1;

static string Normalize(string body) =>
    Regex.Replace(Regex.Replace(body, @"__RequestVerificationToken[^>]*>", ""), @"\s+", " ").Trim();   // strip volatile tokens

static string Hash(string s) => Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(s)))[..16];

record Fingerprint(int Status, string ContentType, string BodyHash);
