// Modern .NET, Chapter 14, Section 14.8
// dotnet new console -n ReplayDiff
using System.Text.Json;

if (args.Length < 3)
{
    Console.WriteLine("Usage: ReplayDiff <legacyBaseUrl> <modernBaseUrl> <paths.txt>");
    return 2;
}

using var legacy = new HttpClient { BaseAddress = new Uri(args[0]), Timeout = TimeSpan.FromSeconds(30) };
using var modern = new HttpClient { BaseAddress = new Uri(args[1]), Timeout = TimeSpan.FromSeconds(30) };

var mismatches = 0;
var total = 0;

foreach (var path in File.ReadLines(args[2]).Where(p => p.StartsWith('/')))
{
    total++;
    var (ls, lb) = await Fetch(legacy, path);
    var (ms, mb) = await Fetch(modern, path);

    if (ls != ms || !SemanticallyEqual(lb, mb))
    {
        mismatches++;
        Console.WriteLine($"MISMATCH {path}  legacy={ls} modern={ms}");
    }
}

Console.WriteLine($"\n{total} requests, {mismatches} mismatches ({(total == 0 ? 0 : mismatches * 100.0 / total):F1}%)");
return mismatches == 0 ? 0 : 1;

static async Task<(int Status, string Body)> Fetch(HttpClient http, string path)
{
    try
    {
        using var response = await http.GetAsync(path);
        return ((int)response.StatusCode, await response.Content.ReadAsStringAsync());
    }
    catch (Exception ex)
    {
        return (0, ex.GetType().Name);
    }
}

// JSON compares structurally (property order and whitespace are irrelevant); other bodies compare after trimming.
static bool SemanticallyEqual(string a, string b)
{
    try
    {
        using var da = JsonDocument.Parse(a);
        using var db = JsonDocument.Parse(b);
        return JsonEquals(da.RootElement, db.RootElement);
    }
    catch (JsonException)
    {
        return a.Trim() == b.Trim();
    }
}

static bool JsonEquals(JsonElement a, JsonElement b)
{
    if (a.ValueKind != b.ValueKind) return false;
    return a.ValueKind switch
    {
        JsonValueKind.Object => a.EnumerateObject().OrderBy(p => p.Name).Select(p => p.Name)
                                    .SequenceEqual(b.EnumerateObject().OrderBy(p => p.Name).Select(p => p.Name))
                                && a.EnumerateObject().All(p => JsonEquals(p.Value, b.GetProperty(p.Name))),
        JsonValueKind.Array => a.GetArrayLength() == b.GetArrayLength()
                               && a.EnumerateArray().Zip(b.EnumerateArray()).All(t => JsonEquals(t.First, t.Second)),
        _ => a.GetRawText() == b.GetRawText()
    };
}
