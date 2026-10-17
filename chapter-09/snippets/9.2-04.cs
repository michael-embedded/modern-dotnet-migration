// Modern .NET, Chapter 9, Section 9.2
public static class ProductEndpoints
{
    public static RouteGroupBuilder MapProductEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/products").WithTags("Products");

        group.MapGet("/", (IProductService s, CancellationToken ct) => s.ListAsync(ct));
        group.MapGet("/{id:int}", GetById);
        group.MapPost("/", Create);

        return group;
    }

    private static async Task<Results<Ok<Product>, NotFound>> GetById(int id, IProductService s, CancellationToken ct) =>
        await s.FindAsync(id, ct) is { } p ? TypedResults.Ok(p) : TypedResults.NotFound();

    private static async Task<Created<Product>> Create(CreateProduct request, IProductService s, CancellationToken ct)
    {
        var product = await s.CreateAsync(request, ct);
        return TypedResults.Created($"/api/products/{product.Id}", product);
    }
}
