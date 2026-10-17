// Modern .NET, Chapter 2, Section 2.4
using System.Xml.Linq;

public static class RiskScore
{
    private static readonly string[] Blockers =
    {
        "System.Web", "System.ServiceModel", "System.Runtime.Remoting",
        "System.EnterpriseServices", "System.Management"
    };

    public static void Run(string root)
    {
        var projectFiles = Directory.EnumerateFiles(root, "*.csproj", SearchOption.AllDirectories).ToList();
        var byName = projectFiles.ToDictionary(
            p => Path.GetFileNameWithoutExtension(p),
            p => p,
            StringComparer.OrdinalIgnoreCase);

        // Fan-in: how many projects reference each project?
        var fanIn = byName.Keys.ToDictionary(k => k, _ => 0, StringComparer.OrdinalIgnoreCase);
        foreach (var path in projectFiles)
        {
            var doc = XDocument.Load(path);
            foreach (var pr in doc.Descendants().Where(e => e.Name.LocalName == "ProjectReference"))
            {
                var target = Path.GetFileNameWithoutExtension(
                    (pr.Attribute("Include")?.Value ?? "").Replace('\\', '/'));
                if (fanIn.ContainsKey(target)) fanIn[target]++;
            }
        }

        var scored = projectFiles.Select(path =>
        {
            var name = Path.GetFileNameWithoutExtension(path);
            var dir = Path.GetDirectoryName(path)!;
            var loc = Directory.EnumerateFiles(dir, "*.cs", SearchOption.AllDirectories)
                .Where(f => !f.Contains($"{Path.DirectorySeparatorChar}obj{Path.DirectorySeparatorChar}"))
                .Sum(f => File.ReadLines(f).Count());

            var doc = XDocument.Load(path);
            var blockers = doc.Descendants()
                .Where(e => e.Name.LocalName == "Reference")
                .Select(e => (e.Attribute("Include")?.Value ?? "").Split(',')[0].Trim())
                .Count(r => Blockers.Contains(r, StringComparer.OrdinalIgnoreCase));

            var legacy = doc.Root!.Attribute("Sdk") is null ? 1 : 0;

            // Weighted score: tune these to your own context.
            var score = (loc / 1000.0) + (fanIn[name] * 2) + (blockers * 5) + (legacy * 2);
            return new { Name = name, Loc = loc, FanIn = fanIn[name], Blockers = blockers, Score = score };
        })
        .OrderByDescending(x => x.Score)
        .ToList();

        Console.WriteLine();
        Console.WriteLine($"{"Project",-36} {"LOC",8} {"Fan-in",7} {"Blockers",9} {"Score",7}");
        Console.WriteLine(new string('-', 72));
        foreach (var s in scored)
            Console.WriteLine($"{s.Name,-36} {s.Loc,8} {s.FanIn,7} {s.Blockers,9} {s.Score,7:F1}");
    }
}
