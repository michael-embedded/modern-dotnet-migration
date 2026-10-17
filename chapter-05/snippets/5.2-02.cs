// Modern .NET, Chapter 5, Section 5.2
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddSingleton<IGreeter, ConsoleGreeter>();
services.AddTransient<Welcome>();

using var provider = services.BuildServiceProvider();

var welcome = provider.GetRequiredService<Welcome>();
welcome.Run("Ada");

public interface IGreeter
{
    string Greet(string name);
}

public sealed class ConsoleGreeter : IGreeter
{
    public string Greet(string name) => $"Hello, {name}!";
}

public sealed class Welcome(IGreeter greeter)
{
    public void Run(string name) => Console.WriteLine(greeter.Greet(name));
}
