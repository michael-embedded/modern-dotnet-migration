// Modern .NET, Chapter 6, Section 6.5
var primes = Enumerable.Range(2, 200_000)
    .AsParallel()
    .Where(IsPrime)
    .Count();

Console.WriteLine(primes);

static bool IsPrime(int n)
{
    if (n < 2) return false;
    for (var i = 2; i * i <= n; i++)
        if (n % i == 0) return false;
    return true;
}
