// Modern .NET, Chapter 1, Section 1.8
using System.Text.RegularExpressions;

var root = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

if (!Directory.Exists(root))
{
    Console.Error.WriteLine($"Directory not found: {root}");
    return 1;
}

var findings = new List<Finding>();

foreach (var file in EnumerateFiles(root, "*.*"))
{
    var name = Path.GetFileName(file);

    if (name.Equals("packages.config", StringComparison.OrdinalIgnoreCase))
    {
        findings.Add(new(file, 0, "packages.config",
            "Legacy NuGet management. Migrate to PackageReference."));
    }
    else if (name.EndsWith(".csproj", StringComparison.OrdinalIgnoreCase))
    {
        var text = File.ReadAllText(file);
        if (!text.Contains("Sdk=\"Microsoft.NET.Sdk", StringComparison.Ordinal))
        {
            findings.Add(new(file, 0, "Non-SDK project",
                "Old-style project file. Convert to SDK-style."));
        }
    }
    else if (name.EndsWith(".cs", StringComparison.OrdinalIgnoreCase))
    {
        ScanSource(file, findings);
    }
}

PrintReport(findings);
return findings.Count == 0 ? 0 : 2;

static IEnumerable<string> EnumerateFiles(string root, string pattern)
{
    var skip = new[] { $"{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}",
                       $"{Path.DirectorySeparatorChar}obj{Path.DirectorySeparatorChar}" };
    return Directory.EnumerateFiles(root, pattern, SearchOption.AllDirectories)
        .Where(f => !skip.Any(s => f.Contains(s, StringComparison.OrdinalIgnoreCase)));
}

static void ScanSource(string file, List<Finding> findings)
{
    var blocking = new Regex(@"\.(Result\b|Wait\(\)|GetAwaiter\(\)\.GetResult\(\))",
        RegexOptions.Compiled);
    var lines = File.ReadAllLines(file);

    for (var i = 0; i < lines.Length; i++)
    {
        var line = lines[i];
        var lineNo = i + 1;

        if (line.Contains("ConfigurationManager", StringComparison.Ordinal))
            findings.Add(new(file, lineNo, "ConfigurationManager",
                "Use IConfiguration and the Options pattern."));

        if (line.Contains("using System.Web", StringComparison.Ordinal))
            findings.Add(new(file, lineNo, "System.Web",
                "Not available in ASP.NET Core. Plan a replacement."));

        if (blocking.IsMatch(line))
            findings.Add(new(file, lineNo, "Sync-over-async",
                "Blocking on a Task risks deadlocks and thread starvation."));
    }
}

static void PrintReport(List<Finding> findings)
{
    if (findings.Count == 0)
    {
        Console.WriteLine("No legacy patterns found. Suspiciously clean.");
        return;
    }

    Console.WriteLine($"Found {findings.Count} legacy signal(s):\n");

    foreach (var group in findings.GroupBy(f => f.Rule).OrderByDescending(g => g.Count()))
    {
        Console.WriteLine($"== {group.Key} ({group.Count()}) ==");
        foreach (var f in group.Take(5))
        {
            var loc = f.Line > 0 ? $"{f.File}:{f.Line}" : f.File;
            Console.WriteLine($"  {loc}\n    -> {f.Advice}");
        }
        if (group.Count() > 5) Console.WriteLine($"  ... and {group.Count() - 5} more");
        Console.WriteLine();
    }
}

record Finding(string File, int Line, string Rule, string Advice);
