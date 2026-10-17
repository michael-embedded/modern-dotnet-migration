// Modern .NET, Chapter 6, Section 6.12
using System.Diagnostics;

var folder = Path.Combine(Path.GetTempPath(), "async-convert-demo");
Directory.CreateDirectory(folder);
for (var i = 1; i <= 12; i++)
    File.WriteAllText(Path.Combine(folder, $"file{i}.txt"),
        string.Join(' ', Enumerable.Repeat("lorem ipsum dolor sit amet", i * 200)));

var sw = Stopwatch.StartNew();
var legacyTotal = new LegacyWordCounter().CountWords(folder);
Console.WriteLine($"Legacy : {legacyTotal} words in {sw.ElapsedMilliseconds} ms");

sw.Restart();
using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
var modernTotal = await new WordCounter().CountWordsAsync(folder, maxConcurrency: 4, cts.Token);
Console.WriteLine($"Modern : {modernTotal} words in {sw.ElapsedMilliseconds} ms");

public sealed class LegacyWordCounter
{
    public int CountWords(string folder)
    {
        var total = 0;
        foreach (var file in Directory.GetFiles(folder, "*.txt"))
        {
            var text = File.ReadAllText(file);                       // blocking
            total += text.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
        }
        return total;
    }
}

public sealed class WordCounter
{
    public async Task<int> CountWordsAsync(string folder, int maxConcurrency, CancellationToken ct = default)
    {
        var total = 0;

        await Parallel.ForEachAsync(
            Directory.EnumerateFiles(folder, "*.txt"),
            new ParallelOptions { MaxDegreeOfParallelism = maxConcurrency, CancellationToken = ct },
            async (file, token) =>
            {
                var text = await File.ReadAllTextAsync(file, token);
                var count = text.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
                Interlocked.Add(ref total, count);                   // thread-safe accumulation
            });

        return total;
    }
}
