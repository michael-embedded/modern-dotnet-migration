// Modern .NET, Chapter 2, Section 2.3
// Run as a separate small console app: dotnet new console -n PackageAudit
using System.Xml.Linq;

var root = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

var rows = Directory
    .EnumerateFiles(root, "packages.config", SearchOption.AllDirectories)
    .SelectMany(file =>
    {
        var project = Path.GetFileName(Path.GetDirectoryName(file))!;
        return XDocument.Load(file).Descendants("package").Select(p => new
        {
            Project = project,
            Id = p.Attribute("id")?.Value ?? "?",
            Version = p.Attribute("version")?.Value ?? "?",
            Framework = p.Attribute("targetFramework")?.Value ?? "?"
        });
    })
    .ToList();

var distinct = rows
    .GroupBy(r => r.Id, StringComparer.OrdinalIgnoreCase)
    .Select(g => new
    {
        Id = g.Key,
        Versions = string.Join(", ", g.Select(x => x.Version).Distinct().OrderBy(v => v)),
        UsedBy = g.Select(x => x.Project).Distinct().Count()
    })
    .OrderByDescending(x => x.UsedBy)
    .ThenBy(x => x.Id);

Console.WriteLine($"{"Package",-45} {"Versions",-30} Projects");
Console.WriteLine(new string('-', 90));
foreach (var d in distinct)
    Console.WriteLine($"{d.Id,-45} {d.Versions,-30} {d.UsedBy}");

Console.WriteLine($"\n{distinct.Count()} distinct packages across {rows.Select(r => r.Project).Distinct().Count()} projects.");
