// Modern .NET, Chapter 3, Section 3.4
// dotnet new console -n CsprojConverter
using System.Xml.Linq;

if (args.Length == 0)
{
    Console.WriteLine("Usage: CsprojConverter <path-to-legacy.csproj> [targets]");
    return 1;
}

var path = args[0];
var targets = args.Length > 1 ? args[1] : "net48;net10.0";

if (!File.Exists(path))
{
    Console.Error.WriteLine($"File not found: {path}");
    return 1;
}

var doc = XDocument.Load(path);
if (doc.Root!.Attribute("Sdk") is not null)
{
    Console.WriteLine("Project is already SDK-style. Nothing to do.");
    return 0;
}

string Prop(string name) =>
    doc.Descendants().FirstOrDefault(e => e.Name.LocalName == name)?.Value ?? "";

var rootNamespace = Prop("RootNamespace");
var dir = Path.GetDirectoryName(Path.GetFullPath(path))!;
var packagesConfig = Path.Combine(dir, "packages.config");

var packages = File.Exists(packagesConfig)
    ? XDocument.Load(packagesConfig).Descendants("package")
        .Select(p => (Id: p.Attribute("id")!.Value, Version: p.Attribute("version")!.Value))
        .ToList()
    : [];

var projectRefs = doc.Descendants()
    .Where(e => e.Name.LocalName == "ProjectReference")
    .Select(e => e.Attribute("Include")!.Value)
    .ToList();

var lines = new List<string>
{
    "<Project Sdk=\"Microsoft.NET.Sdk\">",
    "",
    "  <PropertyGroup>",
    $"    <TargetFrameworks>{targets}</TargetFrameworks>",
};
if (rootNamespace.Length > 0) lines.Add($"    <RootNamespace>{rootNamespace}</RootNamespace>");
lines.Add("    <LangVersion>latest</LangVersion>");
lines.Add("    <GenerateAssemblyInfo>false</GenerateAssemblyInfo>");
lines.Add("  </PropertyGroup>");
lines.Add("");

if (packages.Count > 0)
{
    lines.Add("  <ItemGroup>");
    foreach (var (id, version) in packages)
        lines.Add($"    <PackageReference Include=\"{id}\" Version=\"{version}\" />");
    lines.Add("  </ItemGroup>");
    lines.Add("");
}

if (projectRefs.Count > 0)
{
    lines.Add("  <ItemGroup>");
    foreach (var r in projectRefs)
        lines.Add($"    <ProjectReference Include=\"{r}\" />");
    lines.Add("  </ItemGroup>");
    lines.Add("");
}

lines.Add("</Project>");

var output = Path.ChangeExtension(path, ".sdk.csproj");
File.WriteAllLines(output, lines);
Console.WriteLine($"Wrote {output}");
Console.WriteLine("Review it, then replace the original and delete packages.config.");
return 0;
