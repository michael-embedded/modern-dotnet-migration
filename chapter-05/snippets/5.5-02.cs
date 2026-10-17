// Modern .NET, Chapter 5, Section 5.5
using Microsoft.Extensions.DependencyInjection;

public static class ServiceLocator
{
    private static IServiceProvider? _provider;

    // Called once, from the composition root, after the host is built.
    public static void Initialize(IServiceProvider provider) => _provider = provider;

    [Obsolete("Inject the dependency through the constructor instead.")]
    public static T Resolve<T>() where T : notnull =>
        (_provider ?? throw new InvalidOperationException("ServiceLocator not initialized."))
            .GetRequiredService<T>();
}
