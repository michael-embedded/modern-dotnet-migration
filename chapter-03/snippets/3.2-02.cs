// Modern .NET, Chapter 3, Section 3.2
public static string Describe()
{
#if NET48
    return "Running on .NET Framework 4.8";
#elif NET10_0_OR_GREATER
    return "Running on modern .NET 10+";
#else
    return "Unknown target";
#endif
}
