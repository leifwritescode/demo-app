using Demo.Core.Ids;
using Demo.Core.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Demo.Core.CQRS.Sites;

public record GetSiteByIdQuery(SiteId Id) : IRequest<Site?>;

public class GetSiteByIdQueryHandler(IDbContextFactory<DemoDbContext> contextFactory)
    : IRequestHandler<GetSiteByIdQuery, Site?>
{
    public async Task<Site?> Handle(GetSiteByIdQuery request, CancellationToken cancellationToken)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        return await context.Sites.FindAsync([request.Id.Value], cancellationToken: cancellationToken);
    }
}