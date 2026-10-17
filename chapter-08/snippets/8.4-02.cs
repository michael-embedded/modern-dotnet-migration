// Modern .NET, Chapter 8, Section 8.4
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ShopMvc.Controllers;

public sealed class Product
{
    public int Id { get; set; }

    [Required, StringLength(80)]
    public string Name { get; set; } = "";

    [Range(0.01, 100000)]
    public decimal Price { get; set; }
}

public interface IProductStore
{
    IReadOnlyList<Product> Search(string? query);
    Product? Find(int id);
    void Add(Product product);
}

public sealed class InMemoryProductStore : IProductStore
{
    private readonly List<Product> _items = [new() { Id = 1, Name = "Keyboard", Price = 49 }];
    private int _nextId = 2;

    public IReadOnlyList<Product> Search(string? query) =>
        [.. _items.Where(p => string.IsNullOrEmpty(query) || p.Name.Contains(query, StringComparison.OrdinalIgnoreCase))];

    public Product? Find(int id) => _items.FirstOrDefault(p => p.Id == id);

    public void Add(Product product) { product.Id = _nextId++; _items.Add(product); }
}

public sealed class ProductsController(IProductStore store) : Controller
{
    public IActionResult Index(string? q)
    {
        ViewData["Query"] = q;
        return View(store.Search(q));
    }

    public IActionResult Create() => View(new Product());

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Create(Product model)
    {
        if (!ModelState.IsValid) return View(model);
        store.Add(model);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Details(int id) =>
        store.Find(id) is { } product ? View(product) : NotFound();
}
