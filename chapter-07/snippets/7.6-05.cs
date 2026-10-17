// Modern .NET, Chapter 7, Section 7.6
using System.Diagnostics;

var psi = new ProcessStartInfo("dotnet")
{
    RedirectStandardOutput = true,
    UseShellExecute = false
};
psi.ArgumentList.Add("--version");        // each argument added separately; no manual quoting

using var process = Process.Start(psi)!;
var output = await process.StandardOutput.ReadToEndAsync();
await process.WaitForExitAsync();

Console.WriteLine($"dotnet {output.Trim()} (exit code {process.ExitCode})");
