// Modern .NET, Chapter 9, Section 9.2
app.MapGet("/api/products/{id:int}",
    async Task<Results<Ok<Product>, NotFound>> (int id, IProductService products, CancellationToken ct) =>
        await products.FindAsync(id, ct) is { } p ? TypedResults.Ok(p) : TypedResults.NotFound());
