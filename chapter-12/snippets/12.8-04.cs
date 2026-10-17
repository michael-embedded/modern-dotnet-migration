// Modern .NET, Chapter 12, Section 12.8
using System.Security.Cryptography;
using System.Text;

var subscribers = new List<byte[]>();                // the leak: unbounded retention
var rng = new Random(1);
var cts = new CancellationTokenSource(TimeSpan.FromMinutes(2));

Console.WriteLine($"PID {Environment.ProcessId}: attach profilers now. Ctrl+C to stop.");

while (!cts.IsCancellationRequested)
{
    subscribers.Add(new byte[10_000]);               // never removed

    var payload = Encoding.UTF8.GetBytes(new string('x', 500) + rng.Next());
    for (var i = 0; i < 200; i++)
        _ = SHA256.HashData(payload);                // the CPU hotspot

    await Task.Delay(5);
}
