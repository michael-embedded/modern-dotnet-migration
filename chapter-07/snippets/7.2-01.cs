// Modern .NET, Chapter 7, Section 7.2
var handler = new SocketsHttpHandler
{
    PooledConnectionLifetime = TimeSpan.FromMinutes(2),   // re-resolve DNS periodically
    PooledConnectionIdleTimeout = TimeSpan.FromSeconds(30),
    MaxConnectionsPerServer = 50
};

using var client = new HttpClient(handler, disposeHandler: false)
{
    BaseAddress = new Uri("https://api.example.com/"),
    Timeout = TimeSpan.FromSeconds(30)
};
