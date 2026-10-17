// Modern .NET, Chapter 13, Section 13.6
// dotnet new console -n SmokeTest
using System.Diagnostics;

var baseUrl = args.Length > 0 ? args[0] : "http://localhost:8080";
var timeout = TimeSpan.FromSeconds(60);

using var http = new HttpClient { BaseAddress = new Uri(baseUrl), Timeout = TimeSpan.FromSeconds(5) };

var checks = new (string Name, string Path, int ExpectedStatus)[]
{
    ("Liveness",  "/health/live",  200),
    ("Readiness", "/health/ready", 200),
    ("Home page", "/",             200),
};

var failures = 0;
foreach (var (name, path, expected) in checks)
{
    var ok = await WaitFor(path, expected);
    Console.WriteLine($"{(ok ? "PASS" : "FAIL")}  {name,-10} {path}");
    if (!ok) failures++;
}

return failures == 0 ? 0 : 1;

async Task<bool> WaitFor(string path, int expectedStatus)
{
    var sw = Stopwatch.StartNew();
    while (sw.Elapsed < timeout)                           // new instances need a moment to start
    {
        try
        {
            using var response = await http.GetAsync(path);
            if ((int)response.StatusCode == expectedStatus) return true;
        }
        catch (HttpRequestException) { /* not up yet */ }
        await Task.Delay(2000);
    }
    return false;
}
