// Modern .NET, Chapter 7, Section 7.2
public sealed record Product(int Id, string Name, decimal Price);

public sealed class CatalogClient(HttpClient http)
{
    public async Task<Product?> GetProductAsync(int id, CancellationToken ct = default)
    {
        using var response = await http.GetAsync($"products/{id}", ct);

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return null;

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Product>(ct);
    }

    public async Task<Product> CreateProductAsync(Product product, CancellationToken ct = default)
    {
        using var response = await http.PostAsJsonAsync("products", product, ct);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<Product>(ct))!;
    }
}
