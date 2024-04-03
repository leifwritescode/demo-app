using Demo.Core.CQRS.Sites;
using Demo.Core.CQRS.Tags;
using Demo.Core.Ids;
using Demo.Types;
using GraphQL;
using GraphQL.Types;
using MediatR;

namespace Demo;

public sealed class DemoQuery : ObjectGraphType
{
    public DemoQuery()
    {
        Name = "Query";

        Field<NodeInterface>("node")
            .Argument<NonNullGraphType<IdGraphType>>("id")
            .ResolveAsync(async ctx =>
            {
                IId id;
                try
                {
                    id = IdEncoding.Decode(ctx.GetArgument<string>("id"));
                }
                catch (ArgumentException)
                {
                    throw new ExecutionError("Invalid id");
                }

                var mediator = ctx.GetServiceProvider().GetRequiredService<IMediator>();

                switch (id)
                {
                    case SiteId sid:
                        var site = await mediator.Send(new GetSiteByIdQuery(sid), ctx.CancellationToken);
                        return site;
                    case TagId tid:
                        var tag = await mediator.Send(new GetTagByIdQuery(tid), ctx.CancellationToken);
                        return tag;
                    default:
                        throw new ExecutionError("Invalid id");
                }
            });
        
        Connection<SiteType>("sites")
            .Description("List all sites in the application")
            .PageSize(100)
            .ResolveAsync(async ctx =>
            {
                var first = ctx.First ?? throw new ExecutionError("invalid first value");
                var after = ctx.After is null ? 0 : ConnectionUtils.CursorToId(ctx.After);

                var mediator = ctx.GetServiceProvider().GetRequiredService<IMediator>();

                var sites = await mediator.Send(new PagedSitesQuery(first + 1, after), ctx.CancellationToken);

                return ConnectionUtils.ToConnection(sites, first, x => x.Id);
            });
    }
}