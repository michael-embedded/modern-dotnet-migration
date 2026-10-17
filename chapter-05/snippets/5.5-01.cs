// Modern .NET, Chapter 5, Section 5.5
public static class ServiceLocator
{
    private static readonly Dictionary<Type, object> _services = new();

    public static void Register<T>(T instance) where T : class => _services[typeof(T)] = instance;
    public static T Resolve<T>() where T : class => (T)_services[typeof(T)];
}

public class ReportService
{
    public void Run()
    {
        var repo = ServiceLocator.Resolve<IOrderRepository>();   // hidden dependency
        // ...
    }
}
