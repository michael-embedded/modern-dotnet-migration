// Modern .NET, Chapter 6, Section 6.2
// Good: no need for async/await when simply passing through
public Task<string> GetNameAsync(int id, CancellationToken ct) =>
    _repository.GetNameAsync(id, ct);
