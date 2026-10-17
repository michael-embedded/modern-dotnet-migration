// Modern .NET, Chapter 14, Section 14.3
// A narrow, temporary abstraction that lets a shared library compile for both targets
public interface IAppSettings
{
    string? Get(string key);
}

#if NET48
public sealed class AppSettings : IAppSettings
{
    public string? Get(string key) => System.Configuration.ConfigurationManager.AppSettings[key];
}
#else
public sealed class AppSettings(Microsoft.Extensions.Configuration.IConfiguration configuration) : IAppSettings
{
    public string? Get(string key) => configuration[key];
}
#endif
