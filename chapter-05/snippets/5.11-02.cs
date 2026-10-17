// Modern .NET, Chapter 5, Section 5.11
public sealed class GoodCache(IServiceScopeFactory scopes)
{
    public void DoWork()
    {
        using var scope = scopes.CreateScope();
        var state = scope.ServiceProvider.GetRequiredService<RequestState>();
        // use state within this scope only
    }
}
