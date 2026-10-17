// Modern .NET, Chapter 8, Section 8.7
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; // Always in production
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.Events.OnRedirectToLogin = ctx => { ctx.Response.StatusCode = 401; return Task.CompletedTask; };
        options.Events.OnRedirectToAccessDenied = ctx => { ctx.Response.StatusCode = 403; return Task.CompletedTask; };
    });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("CanManageProducts", policy => policy.RequireClaim("permission", "products:write"))
    .AddPolicy("AdultsOnly", policy => policy.RequireAssertion(ctx =>
        int.TryParse(ctx.User.FindFirstValue("age"), out var age) && age >= 18));

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.MapPost("/login", async (string user, HttpContext ctx) =>
{
    var claims = new List<Claim>
    {
        new(ClaimTypes.Name, user),
        new("permission", user == "admin" ? "products:write" : "products:read"),
        new("age", "30")
    };
    var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
    await ctx.SignInAsync(principal);
    return Results.Ok($"Signed in as {user}");
});

app.MapGet("/me", (ClaimsPrincipal user) => user.Identity?.Name ?? "anonymous");
app.MapGet("/products", () => "visible to any signed-in user").RequireAuthorization();
app.MapDelete("/products/1", () => "deleted").RequireAuthorization("CanManageProducts");

app.Run();
