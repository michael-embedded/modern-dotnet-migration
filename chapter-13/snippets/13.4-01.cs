// Modern .NET, Chapter 13, Section 13.4
var builder = WebApplication.CreateBuilder(args);

// Reads every file in /run/secrets as a key (file name) and value (file content)
if (Directory.Exists("/run/secrets"))
    builder.Configuration.AddKeyPerFile("/run/secrets", optional: true);
