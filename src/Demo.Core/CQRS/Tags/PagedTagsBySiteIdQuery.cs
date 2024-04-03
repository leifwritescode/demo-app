using Demo.Core.Ids;
using Demo.Core.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Demo.Core.CQRS.Tags;

public record PagedTagsBySiteIdQuery(SiteId SiteId, int First, int After) : IRequest<List<Tag>>;

public class PagedTagsBySiteIdQueryHandler(IDbContextFactory<DemoDbContext> contextFactory)
    : IRequestHandler<PagedTagsBySiteIdQuery, List<Tag>>
{
    public async Task<List<Tag>> Handle(PagedTagsBySiteIdQuery request, CancellationToken cancellationToken)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        return await context.Tags
            .Where(x => x.SiteId == request.SiteId.Value)
            .Where(x => x.Id > request.After)
            .OrderBy(x => x.Id)
            .Take(request.First)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
