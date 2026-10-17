// Modern .NET, Chapter 5, Section 5.6
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

var builder = Host.CreateApplicationBuilder(args);

builder.Services
    .AddOptions<PricingOptions>()
    .Bind(builder.Configuration.GetSection(PricingOptions.SectionName))
    .ValidateDataAnnotations()
    .ValidateOnStart();          // fail fast at startup, not at 2 a.m.

builder.Services.AddTransient<PriceCalculator>();

using var host = builder.Build();

var calc = host.Services.GetRequiredService<PriceCalculator>();
Console.WriteLine(calc.WithTax(200m));

public sealed class PricingOptions
{
    public const string SectionName = "Pricing";

    [Range(0, 1)]
    public decimal TaxRate { get; init; }

    [Range(0, 10_000)]
    public decimal FreeShippingThreshold { get; init; }

    [Required, StringLength(3, MinimumLength = 3)]
    public string Currency { get; init; } = "USD";
}

public sealed class PriceCalculator(IOptions<PricingOptions> options)
{
    private readonly PricingOptions _pricing = options.Value;

    public decimal WithTax(decimal net) => Math.Round(net * (1 + _pricing.TaxRate), 2);
}
