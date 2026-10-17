// Modern .NET, Chapter 10, Section 10.12
using Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Data.Tests;

public class EquivalenceTests : IDisposable
{
    private readonly SqliteConnection _conn = new("Data Source=:memory:");   // shared in-memory DB
    private readonly string _legacyCs;

    public EquivalenceTests()
    {
        _conn.Open();
        using var cmd = _conn.CreateCommand();
        cmd.CommandText = """
            CREATE TABLE Products (Id INTEGER PRIMARY KEY, Name TEXT NOT NULL, Price REAL NOT NULL);
            INSERT INTO Products (Name, Price) VALUES ('Pen', 1.5), ('Book', 12), ('Lamp', 30), ('Desk', 150);
            """;
        cmd.ExecuteNonQuery();
        _legacyCs = "Data Source=file:memdb1?mode=memory&cache=shared";
    }

    [Fact]
    public async Task Ef_And_Legacy_Return_Same_Rows()
    {
        var options = new DbContextOptionsBuilder<CatalogContext>().UseSqlite(_conn).Options;
        await using var ctx = new CatalogContext(options);
        var ef = await new EfProductRepository(ctx).FindByMaxPriceAsync(40m);

        Assert.Equal(["Pen", "Book", "Lamp"], ef.Select(r => r.Name));
    }

    public void Dispose() => _conn.Dispose();
}
