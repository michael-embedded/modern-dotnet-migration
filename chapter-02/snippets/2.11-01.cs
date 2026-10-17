// Modern .NET, Chapter 2, Section 2.11
using System.Text;

public static class ReportWriter
{
    public static void Write(string path, IEnumerable<ProjectInfo> projects)
    {
        var list = projects.ToList();
        var sb = new StringBuilder();

        sb.AppendLine("# .NET Migration Audit Report");
        sb.AppendLine();
        sb.AppendLine($"Generated: {DateTime.UtcNow:yyyy-MM-dd HH:mm} UTC");
        sb.AppendLine();
        sb.AppendLine("## Summary");
        sb.AppendLine();
        sb.AppendLine($"- Total projects: **{list.Count}**");
        sb.AppendLine($"- Legacy-style project files: **{list.Count(p => p.Style == "Legacy")}**");
        sb.AppendLine($"- Projects using packages.config: **{list.Count(p => p.PackageStyle == "packages.config")}**");
        sb.AppendLine($"- Executable projects: **{list.Count(p => p.OutputType is "Exe" or "WinExe")}**");
        sb.AppendLine();
        sb.AppendLine("## Projects");
        sb.AppendLine();
        sb.AppendLine("| Project | Style | Target | Type | Packages | Project refs |");
        sb.AppendLine("|---|---|---|---|---|---|");

        foreach (var p in list)
            sb.AppendLine($"| {p.Name} | {p.Style} | {p.Target} | {p.OutputType} | {p.PackageStyle} | {p.ProjectRefs} |");

        sb.AppendLine();
        sb.AppendLine("## Next Steps");
        sb.AppendLine();
        sb.AppendLine("1. Review high fan-in projects and prepare characterization tests.");
        sb.AppendLine("2. Resolve blocking third-party dependencies with vendors.");
        sb.AppendLine("3. Convert leaf libraries to SDK-style projects (Chapter 3).");

        File.WriteAllText(path, sb.ToString());
        Console.WriteLine($"Report written to {Path.GetFullPath(path)}");
    }
}
