using Demo.Core.CQRS.Tags;
using Demo.Core.Ids;
using Demo.Core.Persistence;
using GraphQL;
using GraphQL.Types;
using MediatR;

namespace Demo.Types;

public sealed class SiteType : ObjectGraphType<Site>
{
    public SiteType()
    {
        Name = "Site";
        Description = "An industrial site";

        Interface<NodeInterface>();

        Field<NonNullGraphType<IdGraphType>>("id")
            .Resolve(x => IdEncoding.Encode(SiteId.From(x.Source.Id)));
        Field(x => x.Name)
            .Description("The name of the site");
        
        Connection<TagType>("tags")
            .Description("List tags for this site")
            .PageSize(100)
            .ResolveAsync(async ctx =>
            {
                var first = ctx.First ?? throw new ExecutionError("invalid first value");
                var after = ctx.After is null ? 0 : ConnectionUtils.CursorToId(ctx.After);

                var mediator = ctx.GetServiceProvider().GetRequiredService<IMediator>();

                var tags = await mediator.Send(new PagedTagsBySiteIdQuery(SiteId.From(ctx.Source.Id), first + 1, after), ctx.CancellationToken);

                return ConnectionUtils.ToConnection(tags, first, x => x.Id);
            });
    }
}