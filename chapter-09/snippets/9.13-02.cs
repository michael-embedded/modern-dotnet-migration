// Modern .NET, Chapter 9, Section 9.13
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

BenchmarkRunner.Run<EndpointBenchmarks>();

[MemoryDiagnoser]
public class EndpointBenchmarks
{
    private WebApplication _app = null!;
    private HttpClient _client = null!;

    [GlobalSetup]
    public async Task Setup()
    {
        var builder = WebApplication.CreateBuilder();
        builder.WebHost.UseTestServer();
        builder.Logging.ClearProviders();
        builder.Services.AddControllers().AddApplicationPart(typeof(BenchController).Assembly);

        _app = builder.Build();
        _app.MapGet("/minimal/{id:int}", (int id) => Results.Ok(new Item(id, "widget")));
        _app.MapControllers();

        await _app.StartAsync();
        _client = _app.GetTestClient();
    }

    [GlobalCleanup]
    public async Task Cleanup() => await _app.DisposeAsync();

    [Benchmark(Baseline = true)]
    public async Task<string> Controller() => await _client.GetStringAsync("/controller/42");

    [Benchmark]
    public async Task<string> Minimal() => await _client.GetStringAsync("/minimal/42");
}

public sealed record Item(int Id, string Name);

[ApiController]
[Route("controller")]
public class BenchController : ControllerBase
{
    [HttpGet("{id:int}")]
    public IActionResult Get(int id) => Ok(new Item(id, "widget"));
}
