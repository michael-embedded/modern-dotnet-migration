// Modern .NET, Chapter 10, Section 10.12
using Microsoft.Data.Sqlite;

namespace Data;

public sealed class LegacyProductRepository(string connectionString)
{
    public List<(int Id, string Name, decimal Price)> FindByMaxPrice(decimal maxPrice)
    {
        var result = new List<(int, string, decimal)>();
        using var conn = new SqliteConnection(connectionString);
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT Id, Name, Price FROM Products WHERE Price <= $max ORDER BY Price, Id";
        cmd.Parameters.AddWithValue("$max", (double)maxPrice);
        using var r = cmd.ExecuteReader();
        while (r.Read()) result.Add((r.GetInt32(0), r.GetString(1), (decimal)r.GetDouble(2)));
        return result;
    }
}
