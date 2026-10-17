// Modern .NET, Chapter 5, Section 5.2
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<IGreeter, ConsoleGreeter>();

using var host = builder.Build();

var greeter = host.Services.GetRequiredService<IGreeter>();
Console.WriteLine(greeter.Greet("Grace"));
