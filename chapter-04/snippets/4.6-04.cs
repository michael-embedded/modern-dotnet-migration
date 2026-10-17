// Modern .NET, Chapter 4, Section 4.6
public class Product
{
    public required string Name
    {
        get;
        set => field = value?.Trim() ?? throw new ArgumentNullException(nameof(value));
    }
}
