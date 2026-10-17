// Modern .NET, Chapter 2, Section 2.9
// dotnet new console -n MigrationOrder
using System.Xml.Linq;

var root = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

var graph = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);

foreach (var path in Directory.EnumerateFiles(root, "*.csproj", SearchOption.AllDirectories))
{
    var name = Path.GetFileNameWithoutExtension(path);
    var deps = XDocument.Load(path).Descendants()
        .Where(e => e.Name.LocalName == "ProjectReference")
        .Select(e => Path.GetFileNameWithoutExtension((e.Attribute("Include")?.Value ?? "").Replace('\\', '/')))
        .Where(d => d.Length > 0)
        .ToHashSet(StringComparer.OrdinalIgnoreCase);
    graph[name] = deps;
}

// Kahn-style layering: a project is ready when all its dependencies are placed.
var placed = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
var layer = 1;

while (placed.Count < graph.Count)
{
    var ready = graph
        .Where(kv => !placed.Contains(kv.Key))
        .Where(kv => kv.Value.Where(graph.ContainsKey).All(placed.Contains))
        .Select(kv => kv.Key)
        .OrderBy(n => n)
        .ToList();

    if (ready.Count == 0)
    {
        Console.WriteLine("Cycle detected among: " +
            string.Join(", ", graph.Keys.Where(k => !placed.Contains(k))));
        return 1;
    }

    Console.WriteLine($"Layer {layer++}: {string.Join(", ", ready)}");
    foreach (var r in ready) placed.Add(r);
}

return 0;
