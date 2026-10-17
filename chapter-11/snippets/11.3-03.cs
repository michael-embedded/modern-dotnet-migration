// Modern .NET, Chapter 11, Section 11.3
using Billing;
using Microsoft.Extensions.Time.Testing;

namespace Billing.Tests;

public class LateFeeServiceTests
{
    private static LateFeeService Create(DateTimeOffset now, decimal dailyFee = 2m) =>
        new(new FakeTimeProvider(now), new LateFeeOptions { DailyFee = dailyFee });

    [Fact]
    public void NotLate_NoFee()
    {
        var sut = Create(new DateTimeOffset(2026, 3, 10, 12, 0, 0, TimeSpan.Zero));
        Assert.Equal(0m, sut.Calculate(new Loan(new DateTime(2026, 3, 10), 1000m)));
    }

    [Fact]
    public void FiveDaysLate_ChargesDailyFee()
    {
        var sut = Create(new DateTimeOffset(2026, 3, 15, 12, 0, 0, TimeSpan.Zero));
        Assert.Equal(10m, sut.Calculate(new Loan(new DateTime(2026, 3, 10), 1000m)));
    }

    [Fact]
    public void Fee_IsCappedAtQuarterOfPrincipal()
    {
        var sut = Create(new DateTimeOffset(2026, 12, 31, 12, 0, 0, TimeSpan.Zero), dailyFee: 50m);
        Assert.Equal(250m, sut.Calculate(new Loan(new DateTime(2026, 3, 10), 1000m)));
    }
}
