// Modern .NET, Chapter 9, Section 9.4
var versions = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1, 0))
    .HasApiVersion(new ApiVersion(2, 0))
    .ReportApiVersions()
    .Build();

var v1 = app.MapGroup("/api/v{version:apiVersion}/products").WithApiVersionSet(versions).MapToApiVersion(1, 0);
var v2 = app.MapGroup("/api/v{version:apiVersion}/products").WithApiVersionSet(versions).MapToApiVersion(2, 0);

v1.MapGet("/{id:int}", (int id, IProductStore store) =>
    store.Find(id) is { } p ? Results.Ok(new { p.Id, p.Name, p.Price }) : Results.NotFound());

v2.MapGet("/{id:int}", (int id, IProductStore store) =>
    store.Find(id) is { } p
        ? Results.Ok(new { p.Id, p.Name, Price = new { Amount = p.Price, Currency = "USD" } })
        : Results.NotFound());
