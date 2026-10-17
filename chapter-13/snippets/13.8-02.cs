// Modern .NET, Chapter 13, Section 13.8
var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.ShopHost_ApiService>("apiservice")
    .WithHttpHealthCheck("/health");

builder.AddProject<Projects.ShopHost_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
