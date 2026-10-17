// Modern .NET, Chapter 6, Section 6.7
var counter = new UnsafeCounter();
var safe = new SafeCounter();

Parallel.For(0, 1_000_000, _ =>
{
    counter.Increment();
    safe.Increment();
});

Console.WriteLine($"Unsafe : {counter.Value}  (expected 1000000)");
Console.WriteLine($"Safe   : {safe.Value}");

class UnsafeCounter
{
    public int Value;
    public void Increment() => Value++;     // read-modify-write: not atomic
}

class SafeCounter
{
    private int _value;
    public int Value => Volatile.Read(ref _value);
    public void Increment() => Interlocked.Increment(ref _value);
}
