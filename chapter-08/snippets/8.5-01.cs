// Modern .NET, Chapter 8, Section 8.5
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
var app = builder.Build();

// Convention-based route, the classic MVC default:
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

// Minimal endpoints with constraints:
app.MapGet("/orders/{id:int:min(1)}", (int id) => $"Order {id}");
app.MapGet("/files/{**path}", (string path) => $"Catch-all: {path}");
app.MapGet("/blog/{year:int:length(4)}/{slug:regex(^[a-z0-9-]+$)}", (int year, string slug) => $"{year}/{slug}");

app.MapControllers();   // attribute-routed controllers
app.Run();

[ApiController]
[Route("api/customers")]
public class CustomersController : ControllerBase
{
    [HttpGet("{id:guid}")]
    public IActionResult Get(Guid id) => Ok(new { id });

    [HttpGet]
    public IActionResult List([FromQuery] int page = 1, [FromQuery] int size = 20) => Ok(new { page, size });
}
