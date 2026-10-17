// Modern .NET, Chapter 9, Section 9.6
using System.ComponentModel.DataAnnotations;

public sealed class ValidationFilter<T> : IEndpointFilter where T : class
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        if (context.Arguments.OfType<T>().FirstOrDefault() is { } model)
        {
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(model, new ValidationContext(model), results, validateAllProperties: true))
            {
                var errors = results
                    .SelectMany(r => (r.MemberNames.DefaultIfEmpty("")).Select(m => (Member: m, r.ErrorMessage)))
                    .GroupBy(x => x.Member)
                    .ToDictionary(g => g.Key, g => g.Select(x => x.ErrorMessage ?? "Invalid").ToArray());
                return TypedResults.ValidationProblem(errors);
            }
        }
        return await next(context);
    }
}

// Usage:
// products.MapPost("/", ...).AddEndpointFilter<ValidationFilter<CreateProduct>>();
