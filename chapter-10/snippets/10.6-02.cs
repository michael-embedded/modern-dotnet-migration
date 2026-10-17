// Modern .NET, Chapter 10, Section 10.6
options.UseSqlServer(connectionString, sql => sql.EnableRetryOnFailure(maxRetryCount: 5));

var strategy = db.Database.CreateExecutionStrategy();
await strategy.ExecuteAsync(async () =>
{
    await using var tx = await db.Database.BeginTransactionAsync();
    // ... unit of work ...
    await tx.CommitAsync();
});
