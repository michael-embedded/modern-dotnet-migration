// Modern .NET, Chapter 8, Section 8.8
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
var app = builder.Build();
app.MapControllers();
app.Run();

public sealed record CreateOrderRequest(
    [Required, EmailAddress] string CustomerEmail,
    [Range(1, 100)] int Quantity,
    [StringLength(200)] string? Note);

[ApiController]
[Route("orders")]
public class OrdersController : ControllerBase
{
    [HttpPost]
    public IActionResult Create([FromBody] CreateOrderRequest request, [FromHeader(Name = "X-Request-Id")] string? requestId)
    {
        // [ApiController] automatically returns 400 with ProblemDetails when validation fails
        return CreatedAtAction(nameof(Get), new { id = 1 }, new { id = 1, request.CustomerEmail, requestId });
    }

    [HttpGet("{id:int}")]
    public IActionResult Get(int id) => Ok(new { id });
}
