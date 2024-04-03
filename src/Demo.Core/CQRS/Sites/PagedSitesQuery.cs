using Demo.Core.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Demo.Core.CQRS.Sites;

public record PagedSitesQuery(int First, int After) : IRequest<List<Site>>;

public class PagedSitesQueryHandler(IDbContextFactory<DemoDbContext> contextFactory)
    : IRequestHandler<PagedSitesQuery, List<Site>>
{
    public async Task<List<Site>> Handle(PagedSitesQuery request, CancellationToken cancellationToken)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        return await context.Sites
            .Where(x => x.Id > request.After)
            .OrderBy(x => x.Id)
            .Take(request.First)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}