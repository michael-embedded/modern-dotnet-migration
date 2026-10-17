// Modern .NET, Chapter 2, Section 2.1
using System.Xml.Linq;

var root = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

if (!Directory.Exists(root))
{
    Console.Error.WriteLine($"Directory not found: {root}");
    return 1;
}

var projects = Directory
    .EnumerateFiles(root, "*.csproj", SearchOption.AllDirectories)
    .Where(p => !p.Contains($"{Path.DirectorySeparatorChar}obj{Path.DirectorySeparatorChar}"))
    .Select(Analyze)
    .OrderBy(p => p.Name)
    .ToList();

Console.WriteLine($"{"Project",-36} {"Style",-8} {"Target",-18} {"Type",-10} {"Packages",-14} Refs");
Console.WriteLine(new string('-', 100));

foreach (var p in projects)
{
    Console.WriteLine(
        $"{Trunc(p.Name, 35),-36} {p.Style,-8} {Trunc(p.Target, 17),-18} {p.OutputType,-10} {p.PackageStyle,-14} {p.ProjectRefs}");
}

Console.WriteLine();
Console.WriteLine($"Total projects : {projects.Count}");
Console.WriteLine($"Legacy-style   : {projects.Count(p => p.Style == "Legacy")}");
Console.WriteLine($"SDK-style      : {projects.Count(p => p.Style == "SDK")}");

foreach (var g in projects.GroupBy(p => p.Target).OrderByDescending(g => g.Count()))
    Console.WriteLine($"  {g.Key,-20} {g.Count()} project(s)");

return 0;

static string Trunc(string s, int n) => s.Length <= n ? s : s[..(n - 1)] + "~";

static ProjectInfo Analyze(string path)
{
    var doc = XDocument.Load(path);
    var proj = doc.Root!;
    var isSdk = proj.Attribute("Sdk") is not null;

    string Value(string name) =>
        proj.Descendants().FirstOrDefault(e => e.Name.LocalName == name)?.Value ?? "";

    var target = isSdk
        ? FirstNonEmpty(Value("TargetFramework"), Value("TargetFrameworks"))
        : "net" + Value("TargetFrameworkVersion").TrimStart('v').Replace(".", "");

    var outputType = Value("OutputType");
    if (outputType.Length == 0) outputType = "Library";

    var dir = Path.GetDirectoryName(path)!;
    var hasPackagesConfig = File.Exists(Path.Combine(dir, "packages.config"));
    var hasPackageRef = proj.Descendants().Any(e => e.Name.LocalName == "PackageReference");

    var packageStyle = hasPackagesConfig ? "packages.config"
                     : hasPackageRef ? "PackageRef"
                     : "none";

    var refs = proj.Descendants().Count(e => e.Name.LocalName == "ProjectReference");

    return new ProjectInfo(
        Path.GetFileNameWithoutExtension(path),
        isSdk ? "SDK" : "Legacy",
        string.IsNullOrWhiteSpace(target) ? "unknown" : target,
        outputType,
        packageStyle,
        refs);
}

static string FirstNonEmpty(params string[] values) =>
    values.FirstOrDefault(v => !string.IsNullOrWhiteSpace(v)) ?? "";

record ProjectInfo(string Name, string Style, string Target,
                   string OutputType, string PackageStyle, int ProjectRefs);
