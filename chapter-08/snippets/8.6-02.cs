// Modern .NET, Chapter 8, Section 8.6
builder.Services.AddHttpContextAccessor();

public sealed class CurrentUser(IHttpContextAccessor accessor) : ICurrentUser
{
    public string? Name => accessor.HttpContext?.User.Identity?.Name;
}
public interface ICurrentUser { string? Name { get; } }
