// Modern .NET, Chapter 7, Section 7.1
// dotnet new console -n ApiWatch
using System.Text.RegularExpressions;

var root = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

var rules = new (string Name, string Pattern, string Replacement)[]
{
    ("BinaryFormatter",  @"\bBinaryFormatter\b",        "System.Text.Json or MessagePack"),
    ("WebClient",        @"\bnew\s+WebClient\b",        "HttpClient via IHttpClientFactory"),
    ("HttpWebRequest",   @"\bHttpWebRequest\b",         "HttpClient"),
    ("Thread.Abort",     @"\.Abort\(\s*\)",             "CancellationToken (cooperative)"),
    ("ArrayList",        @"\bnew\s+ArrayList\b",        "List<T>"),
    ("ConfigManager",    @"\bConfigurationManager\.",   "IConfiguration + Options"),
    ("DateTime.Now",     @"\bDateTime\.(Now|UtcNow)\b", "TimeProvider"),
    ("SmtpClient",       @"\bnew\s+SmtpClient\b",       "MailKit behind an interface"),
};

var compiled = rules.Select(r => (r.Name, r.Replacement, Regex: new Regex(r.Pattern, RegexOptions.Compiled))).ToArray();
var counts = new Dictionary<string, int>();

foreach (var file in Directory.EnumerateFiles(root, "*.cs", SearchOption.AllDirectories)
             .Where(f => !f.Contains($"{Path.DirectorySeparatorChar}obj{Path.DirectorySeparatorChar}")))
{
    var text = File.ReadAllText(file);
    foreach (var (name, _, regex) in compiled)
    {
        var n = regex.Matches(text).Count;
        if (n > 0) counts[name] = counts.GetValueOrDefault(name) + n;
    }
}

Console.WriteLine($"{"API",-18} {"Hits",5}  Replace with");
foreach (var (name, replacement, _) in compiled.OrderByDescending(c => counts.GetValueOrDefault(c.Name)))
    Console.WriteLine($"{name,-18} {counts.GetValueOrDefault(name),5}  {replacement}");
