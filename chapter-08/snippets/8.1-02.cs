// Modern .NET, Chapter 8, Section 8.1
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
app.Run();

[ApiController]
[Route("hello")]
public class HelloController : ControllerBase
{
    [HttpGet("{name}")]
    public IActionResult Get(string name) => Ok(new { message = $"Hello, {name}!", server = "Kestrel" });
}
