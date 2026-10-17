// Modern .NET, Chapter 13, Section 13.4
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo("/keys"))      // a mounted volume; use Redis/Blob/DB in real deployments
    .SetApplicationName("ShopService");
