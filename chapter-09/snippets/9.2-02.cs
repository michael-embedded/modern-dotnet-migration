// Modern .NET, Chapter 9, Section 9.2
[ApiController]
[Route("api/products")]
public class ProductsController(IProductService products) : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> Get(int id, CancellationToken ct) =>
        await products.FindAsync(id, ct) is { } p ? Ok(p) : NotFound();
}
