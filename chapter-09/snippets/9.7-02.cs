// Modern .NET, Chapter 9, Section 9.7
products.MapPost("/", ...).RequireAuthorization("ProductWriter");
products.MapPut("/{id:int}", ...).RequireAuthorization("ProductWriter");
products.MapDelete("/{id:int}", ...).RequireAuthorization("ProductWriter");

if (app.Environment.IsDevelopment())
{
    app.MapPost("/dev/token", (string user, string? scope) =>
    {
        var descriptor = new SecurityTokenDescriptor
        {
            Issuer = "https://shop.example/issuer",
            Audience = "shop-api",
            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
            Subject = new System.Security.Claims.ClaimsIdentity(
            [
                new("sub", user),
                new("scope", scope ?? "products.read")
            ])
        };
        return Results.Ok(new { access_token = new JsonWebTokenHandler().CreateToken(descriptor) });
    });
}
