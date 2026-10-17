// Modern .NET, Chapter 14, Section 14.1
// dotnet new console -n RoadmapStatus
using System.Text.RegularExpressions;

var path = args.Length > 0 ? args[0] : "ROADMAP.md";

if (!File.Exists(path))
{
    File.WriteAllText(path, """
        # Migration Roadmap

        ## Phase 0: Audit and plan
        - [x] Inventory complete
        - [x] Risk ranking reviewed
        - [x] Baseline recorded
        - [x] Rollback runbook rehearsed

        ## Phase 1: Safety net
        - [x] CI builds the solution
        - [ ] Characterization tests cover top-risk logic
        - [ ] Legacy build reproducible from clean checkout

        ## Phase 2: Project structure
        - [ ] All projects SDK-style
        - [ ] Libraries multi-target net48;net10.0
        """);
    Console.WriteLine($"Created sample {path}. Edit it and run again.");
}

string? phase = null;
var phases = new List<(string Name, int Done, int Total)>();
var done = 0; var total = 0;

foreach (var line in File.ReadLines(path))
{
    if (line.StartsWith("## "))
    {
        if (phase is not null) phases.Add((phase, done, total));
        phase = line[3..].Trim(); done = 0; total = 0;
    }
    else if (Regex.Match(line, @"^\s*- \[(?<mark>[ xX])\]") is { Success: true } m)
    {
        total++;
        if (m.Groups["mark"].Value != " ") done++;
    }
}
if (phase is not null) phases.Add((phase, done, total));

foreach (var (name, d, t) in phases)
{
    var pct = t == 0 ? 0 : d * 100 / t;
    var bar = new string('#', pct / 5).PadRight(20, '.');
    Console.WriteLine($"{name,-34} [{bar}] {d}/{t} ({pct}%)");
}

var current = phases.FirstOrDefault(p => p.Done < p.Total);
Console.WriteLine(current.Name is null ? "\nAll phases complete." : $"\nCurrent phase: {current.Name}");
