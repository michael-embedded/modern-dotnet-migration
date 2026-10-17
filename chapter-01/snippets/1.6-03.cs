// Modern .NET, Chapter 1, Section 1.6
using Microsoft.Data.Sqlite;

await using var connection = new SqliteConnection("Data Source=shop.db");
await connection.OpenAsync();

await Seed(connection);

var repository = new CustomerRepository(connection);
await foreach (var name in repository.GetCustomerNamesAsync())
{
    Console.WriteLine(name);
}

static async Task Seed(SqliteConnection connection)
{
    await using var create = connection.CreateCommand();
    create.CommandText = """
        CREATE TABLE IF NOT EXISTS Customers (Id INTEGER PRIMARY KEY, Name TEXT NOT NULL);
        DELETE FROM Customers;
        INSERT INTO Customers (Name) VALUES ('Ada Lovelace'), ('Grace Hopper'), ('Alan Turing');
        """;
    await create.ExecuteNonQueryAsync();
}

public sealed class CustomerRepository(SqliteConnection connection)
{
    public async IAsyncEnumerable<string> GetCustomerNamesAsync(
        [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken ct = default)
    {
        await using var cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT Name FROM Customers ORDER BY Name";
        await using var reader = await cmd.ExecuteReaderAsync(ct);
        while (await reader.ReadAsync(ct))
        {
            yield return reader.GetString(0);
        }
    }
}
