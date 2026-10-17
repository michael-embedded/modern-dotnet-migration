// Modern .NET, Chapter 8, Section 8.10
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IOrderRepository, InMemoryOrderRepository>();
builder.Services.AddAuthentication("Demo").AddScheme<Microsoft.AspNetCore.Authentication.AuthenticationSchemeOptions, DemoAuthHandler>("Demo", null);
builder.Services.AddAuthorization();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/api/orders/{id:int}", async (int id, ClaimsPrincipal user, IOrderRepository repo, CancellationToken ct) =>
{
    var order = await repo.FindAsync(id, ct);
    if (order is null) return Results.NotFound();
    return order.Owner == user.Identity?.Name ? Results.Ok(order) : Results.Forbid();
}).RequireAuthorization();

app.Run();

public sealed record Order(int Id, string Owner, decimal Total);

public interface IOrderRepository { Task<Order?> FindAsync(int id, CancellationToken ct); }

public sealed class InMemoryOrderRepository : IOrderRepository
{
    private readonly Order[] _orders = [new(1, "ada", 40m), new(2, "grace", 90m)];
    public Task<Order?> FindAsync(int id, CancellationToken ct) => Task.FromResult(_orders.FirstOrDefault(o => o.Id == id));
}

// A trivial demo scheme: the "X-User" header becomes the identity. NEVER do this in production.
public sealed class DemoAuthHandler(
    Microsoft.Extensions.Options.IOptionsMonitor<Microsoft.AspNetCore.Authentication.AuthenticationSchemeOptions> options,
    ILoggerFactory logger, System.Text.Encodings.Web.UrlEncoder encoder)
    : Microsoft.AspNetCore.Authentication.AuthenticationHandler<Microsoft.AspNetCore.Authentication.AuthenticationSchemeOptions>(options, logger, encoder)
{
    protected override Task<Microsoft.AspNetCore.Authentication.AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue("X-User", out var name))
            return Task.FromResult(Microsoft.AspNetCore.Authentication.AuthenticateResult.NoResult());

        var identity = new ClaimsIdentity([new Claim(ClaimTypes.Name, name.ToString())], "Demo");
        var ticket = new Microsoft.AspNetCore.Authentication.AuthenticationTicket(new ClaimsPrincipal(identity), "Demo");
        return Task.FromResult(Microsoft.AspNetCore.Authentication.AuthenticateResult.Success(ticket));
    }
}
