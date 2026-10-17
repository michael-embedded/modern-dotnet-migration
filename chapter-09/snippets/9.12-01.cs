// Modern .NET, Chapter 9, Section 9.12
[Fact]
public async Task Create_InvalidBody_ReturnsValidationProblem()
{
    var client = factory.CreateClient();
    var token = await GetTokenAsync(client, "ada", "products.write");
    client.DefaultRequestHeaders.Authorization = new("Bearer", token);

    var response = await client.PostAsJsonAsync("/api/v1/products", new { name = "", price = -5 });

    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    Assert.Equal("application/problem+json", response.Content.Headers.ContentType?.MediaType);
}
