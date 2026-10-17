// Modern .NET, Chapter 9, Section 9.5
products.MapPost("/", (CreateProduct request, IProductStore store) =>
{
    var product = store.Add(request);
    return TypedResults.Created($"/api/products/{product.Id}", product);
})
.WithName("CreateProduct")
.WithSummary("Create a product")
.WithDescription("Creates a product and returns it with its generated identifier.")
.Produces<Product>(StatusCodes.Status201Created)
.ProducesValidationProblem();
