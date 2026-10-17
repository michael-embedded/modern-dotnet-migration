// Modern .NET, Chapter 11, Section 11.5
using Microsoft.Data.SqlClient;
using Testcontainers.MsSql;

namespace Billing.Tests;

public sealed class SqlServerFixture : IAsyncLifetime
{
    private readonly MsSqlContainer _container = new MsSqlBuilder().Build();

    public string ConnectionString => _container.GetConnectionString();

    public async Task InitializeAsync()
    {
        await _container.StartAsync();
        await using var conn = new SqlConnection(ConnectionString);
        await conn.OpenAsync();
        await using var cmd = conn.CreateCommand();
        cmd.CommandText = """
            CREATE TABLE Accounts (Id INT PRIMARY KEY, Balance DECIMAL(18,2) NOT NULL);
            INSERT INTO Accounts VALUES (1, 100.00), (2, 50.00);
            """;
        await cmd.ExecuteNonQueryAsync();
    }

    public Task DisposeAsync() => _container.DisposeAsync().AsTask();
}

public class AccountDatabaseTests(SqlServerFixture db) : IClassFixture<SqlServerFixture>
{
    [Fact]
    public async Task Transfer_Is_Atomic()
    {
        await using var conn = new SqlConnection(db.ConnectionString);
        await conn.OpenAsync();
        await using var tx = (SqlTransaction)await conn.BeginTransactionAsync();

        await Exec(conn, tx, "UPDATE Accounts SET Balance = Balance - 30 WHERE Id = 1");
        await Exec(conn, tx, "UPDATE Accounts SET Balance = Balance + 30 WHERE Id = 2");
        await tx.CommitAsync();

        await using var check = conn.CreateCommand();
        check.CommandText = "SELECT SUM(Balance) FROM Accounts";
        Assert.Equal(150.00m, (decimal)(await check.ExecuteScalarAsync())!);
    }

    private static async Task Exec(SqlConnection conn, SqlTransaction tx, string sql)
    {
        await using var cmd = new SqlCommand(sql, conn, tx);
        await cmd.ExecuteNonQueryAsync();
    }
}
