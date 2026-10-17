// Modern .NET, Chapter 11, Section 11.3
namespace Billing;

public sealed record Loan(DateTime DueDate, decimal Principal);

public sealed class LateFeeOptions { public decimal DailyFee { get; init; } = 2m; }

public sealed class LateFeeService(TimeProvider clock, LateFeeOptions options)
{
    public decimal Calculate(Loan loan)
    {
        var daysLate = (clock.GetLocalNow().Date - loan.DueDate.Date).Days;
        if (daysLate <= 0) return 0;
        var fee = daysLate * options.DailyFee;
        return Math.Min(fee, loan.Principal * 0.25m);
    }
}
