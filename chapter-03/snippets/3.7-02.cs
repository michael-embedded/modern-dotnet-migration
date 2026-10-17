// Modern .NET, Chapter 3, Section 3.7
// dotnet new console -n WarningRatchet
using System.Diagnostics;
using System.Text.RegularExpressions;

var solution = args.Length > 0 ? args[0] : ".";
var baselinePath = args.Length > 1 ? args[1] : "warning-baseline.txt";

var psi = new ProcessStartInfo("dotnet", $"build \"{solution}\" -c Release --no-incremental -nologo")
{
    RedirectStandardOutput = true,
    RedirectStandardError = true,
    UseShellExecute = false
};

using var process = Process.Start(psi)!;
var output = await process.StandardOutput.ReadToEndAsync();
await process.WaitForExitAsync();

if (process.ExitCode != 0)
{
    Console.Error.WriteLine("Build failed.");
    Console.Error.WriteLine(output);
    return 1;
}

// Distinct warning lines (the build prints each warning more than once).
var warnings = Regex.Matches(output, @"^.*: warning [A-Z]+\d+:.*$", RegexOptions.Multiline)
    .Select(m => m.Value.Trim())
    .Distinct()
    .Count();

Console.WriteLine($"Current warnings: {warnings}");

if (!File.Exists(baselinePath))
{
    File.WriteAllText(baselinePath, warnings.ToString());
    Console.WriteLine($"No baseline found. Created {baselinePath} with {warnings}.");
    return 0;
}

var baseline = int.Parse(File.ReadAllText(baselinePath).Trim());
Console.WriteLine($"Baseline warnings: {baseline}");

if (warnings > baseline)
{
    Console.Error.WriteLine($"Warnings increased by {warnings - baseline}. Fix them or justify them.");
    return 2;
}

if (warnings < baseline)
{
    File.WriteAllText(baselinePath, warnings.ToString());
    Console.WriteLine($"Improved! Baseline lowered to {warnings}.");
}

return 0;
