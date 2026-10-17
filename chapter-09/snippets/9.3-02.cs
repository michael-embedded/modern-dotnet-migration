// Modern .NET, Chapter 9, Section 9.3
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IProductStore, InMemoryProductStore>();

var app = builder.Build();

var products = app.MapGroup("/api/products");

products.MapGet("/", (IProductStore store, string? name, int page = 1, int size = 20) =>
{
    page = Math.Max(page, 1);
    size = Math.Clamp(size, 1, 100);

    var query = store.All().Where(p => name is null || p.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
    var total = query.Count();
    var items = query.OrderBy(p => p.Id).Skip((page - 1) * size).Take(size).ToList();

    return TypedResults.Ok(new PagedResult<Product>(items, page, size, total));
});

products.MapGet("/{id:int}", (int id, IProductStore store) =>
    store.Find(id) is { } p ? Results.Ok(p) : Results.NotFound());

products.MapPost("/", (CreateProduct request, IProductStore store) =>
{
    var product = store.Add(request);
    return Results.Created($"/api/products/{product.Id}", product);   // 201 + Location header
});

products.MapPut("/{id:int}", (int id, CreateProduct request, IProductStore store) =>
    store.Replace(id, request) is { } updated ? Results.Ok(updated) : Results.NotFound());

products.MapDelete("/{id:int}", (int id, IProductStore store) =>
    store.Remove(id) ? Results.NoContent() : Results.NotFound());

app.Run();

public sealed record Product(int Id, string Name, decimal Price);
public sealed record CreateProduct([Required, StringLength(80)] string Name, [Range(0.01, 100000)] decimal Price);
public sealed record PagedResult<T>(IReadOnlyList<T> Items, int Page, int PageSize, int TotalCount);

public interface IProductStore
{
    IEnumerable<Product> All();
    Product? Find(int id);
    Product Add(CreateProduct request);
    Product? Replace(int id, CreateProduct request);
    bool Remove(int id);
}

public sealed class InMemoryProductStore : IProductStore
{
    private readonly ConcurrentDictionary<int, Product> _items = new();
    private int _nextId;

    public InMemoryProductStore()
    {
        Add(new CreateProduct("Keyboard", 49m));
        Add(new CreateProduct("Mouse", 25m));
    }

    public IEnumerable<Product> All() => _items.Values;
    public Product? Find(int id) => _items.GetValueOrDefault(id);

    public Product Add(CreateProduct request)
    {
        var id = Interlocked.Increment(ref _nextId);
        var product = new Product(id, request.Name, request.Price);
        _items[id] = product;
        return product;
    }

    public Product? Replace(int id, CreateProduct request) =>
        _items.ContainsKey(id) ? _items[id] = new Product(id, request.Name, request.Price) : null;

    public bool Remove(int id) => _items.TryRemove(id, out _);
}
