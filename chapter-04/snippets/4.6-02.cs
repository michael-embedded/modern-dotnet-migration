// Modern .NET, Chapter 4, Section 4.6
public sealed class SmtpOptions
{
    public required string Host { get; init; }
    public required int Port { get; init; }
    public string? Username { get; init; }
    public bool UseTls { get; init; } = true;
}

var ok = new SmtpOptions { Host = "smtp.example.com", Port = 587 };
// var bad = new SmtpOptions { Host = "x" };   // compile error: Port is required
