// Modern .NET, Chapter 4, Section 4.6
public class Settings
{
    public string Host { get; init; } = "localhost";
    public int Port { get; init; } = 5432;
}

var s = new Settings { Host = "db.internal", Port = 5433 };
// s.Port = 1;   // compile error: init-only
