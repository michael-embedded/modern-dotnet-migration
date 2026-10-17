// Modern .NET, Chapter 6, Section 6.8
using Microsoft.Data.Sqlite;

const string connectionString = "Data Source=async-demo.db";

await InitializeAsync();

var total = await GetTotalForCustomerAsync("ada", CancellationToken.None);
Console.WriteLine($"Total for ada: {total:C}");

static async Task InitializeAsync()
{
    await using var conn = new SqliteConnection(connectionString);
    await conn.OpenAsync();

    await using var cmd = conn.CreateCommand();
    cmd.CommandText = """
        CREATE TABLE IF NOT EXISTS Orders (Id INTEGER PRIMARY KEY, Customer TEXT, Total REAL);
        DELETE FROM Orders;
        INSERT INTO Orders (Customer, Total) VALUES ('ada', 40.5), ('ada', 59.5), ('grace', 10);
        """;
    await cmd.ExecuteNonQueryAsync();
}

static async Task<decimal> GetTotalForCustomerAsync(string customer, CancellationToken ct)
{
    await using var conn = new SqliteConnection(connectionString);
    await conn.OpenAsync(ct);

    await using var cmd = conn.CreateCommand();
    cmd.CommandText = "SELECT COALESCE(SUM(Total), 0) FROM Orders WHERE Customer = $customer";
    cmd.Parameters.AddWithValue("$customer", customer);

    var result = await cmd.ExecuteScalarAsync(ct);
    return Convert.ToDecimal(result);
}
