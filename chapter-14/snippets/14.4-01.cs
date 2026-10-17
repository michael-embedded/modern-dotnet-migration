// Modern .NET, Chapter 14, Section 14.4
// dotnet add package NetArchTest.Rules   (in a test project referencing your modules)
using NetArchTest.Rules;

public class ArchitectureTests
{
    [Fact]
    public void Billing_DoesNotDependOn_ShippingInternals()
    {
        var result = Types.InAssembly(typeof(Billing.BillingModule).Assembly)
            .That().ResideInNamespace("Billing")
            .ShouldNot().HaveDependencyOn("Shipping.Internal")
            .GetResult();

        Assert.True(result.IsSuccessful,
            "Billing must use Shipping only through its public contracts: " +
            string.Join(", ", result.FailingTypeNames ?? []));
    }
}
