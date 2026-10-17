// Modern .NET, Chapter 12, Section 12.3
using System.Text;

static long Measure(Action work)
{
    work();                                             // warm-up: JIT compilation allocates too
    var before = GC.GetAllocatedBytesForCurrentThread();
    work();
    return GC.GetAllocatedBytesForCurrentThread() - before;
}

var words = Enumerable.Range(0, 1000).Select(i => $"word{i}").ToArray();

var concat = Measure(() => { var s = ""; foreach (var w in words) s += w + ","; });
var builder = Measure(() => { var sb = new StringBuilder(); foreach (var w in words) sb.Append(w).Append(','); _ = sb.ToString(); });
var join = Measure(() => _ = string.Join(',', words));

Console.WriteLine($"Concatenation  : {concat,10:N0} bytes");
Console.WriteLine($"StringBuilder  : {builder,10:N0} bytes");
Console.WriteLine($"string.Join    : {join,10:N0} bytes");

var numbers = Enumerable.Range(1, 10_000).ToArray();

var linq = Measure(() => { _ = numbers.Where(n => n % 2 == 0).Select(n => n * 2).Sum(); });
var loop = Measure(() => { long sum = 0; foreach (var n in numbers) if (n % 2 == 0) sum += n * 2; });

Console.WriteLine($"LINQ pipeline  : {linq,10:N0} bytes");
Console.WriteLine($"Plain loop     : {loop,10:N0} bytes");

object boxed = 0;
var boxing = Measure(() => { for (var i = 0; i < 1000; i++) boxed = i; });   // boxes each int
Console.WriteLine($"Boxing 1000 ints: {boxing,9:N0} bytes");
