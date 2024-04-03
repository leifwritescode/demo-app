using GraphQL;

namespace Demo;

public static class ResolveFieldContextExtensions
{
    public static IServiceProvider GetServiceProvider(this IResolveFieldContext<object?> ctx)
    {
        return ctx.RequestServices ?? throw new InvalidOperationException("No service provider specified. Please set the value of the ExecutionOptions.RequestServices to a valid service provider.");
    }
}
