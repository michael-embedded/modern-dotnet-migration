// Modern .NET, Chapter 7, Section 7.5
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.Configure<List<CarrierOptions>>(builder.Configuration.GetSection("Carriers"));

using var host = builder.Build();

foreach (var carrier in host.Services.GetRequiredService<IOptions<List<CarrierOptions>>>().Value)
    Console.WriteLine($"{carrier.Name}: {carrier.ApiUrl} ({carrier.TimeoutSeconds}s)");

var connectionString = host.Services.GetRequiredService<IConfiguration>().GetConnectionString("Shop");
Console.WriteLine($"Connection string: {connectionString}");

public sealed class CarrierOptions
{
    public string Name { get; set; } = "";
    public string ApiUrl { get; set; } = "";
    public int TimeoutSeconds { get; set; } = 30;
}
