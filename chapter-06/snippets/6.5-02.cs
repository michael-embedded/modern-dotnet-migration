// Modern .NET, Chapter 6, Section 6.5
var numbers = Enumerable.Range(1, 5_000_000).ToArray();
long sum = 0;

Parallel.For(0, numbers.Length,
    () => 0L,                                                  // per-thread local state
    (i, state, local) => local + numbers[i],                   // no shared writes
    local => Interlocked.Add(ref sum, local));                 // merge once per thread

Console.WriteLine(sum);
