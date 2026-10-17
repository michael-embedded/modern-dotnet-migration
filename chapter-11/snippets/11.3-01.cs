// Modern .NET, Chapter 11, Section 11.3
public class LateFeeService
{
    public decimal Calculate(Loan loan)
    {
        var daysLate = (DateTime.Now.Date - loan.DueDate.Date).Days;      // hidden clock
        if (daysLate <= 0) return 0;
        var fee = daysLate * Settings.DailyFee;                           // hidden static config
        return Math.Min(fee, loan.Principal * 0.25m);
    }
}
