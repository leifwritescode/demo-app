using GraphQL;
using GraphQL.Types;
using MediatR;

namespace Demo.Mutations;

public sealed partial class DemoMutation : ObjectGraphType
{
    public DemoMutation()
    {
        Name = "Mutation";
        
        CreateSite();
        UpdateSite();
        
        CreateTag();
        UpdateTag();
    }
    
    private static async Task<T> SendCommand<T>(IResolveFieldContext<object?> ctx, IRequest<T> cmd)
    {
        var mediator = ctx.GetServiceProvider().GetRequiredService<IMediator>();
        return await mediator.Send(cmd, ctx.CancellationToken);
    }
}