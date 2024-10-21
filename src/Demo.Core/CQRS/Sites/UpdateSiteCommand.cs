using Demo.Core.Ids;
using Demo.Core.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Demo.Core.CQRS.Sites;

public record UpdateSiteCommand(SiteId Id, string Name) : IRequest<Site>;

public class UpdateSiteCommandHandler(IDbContextFactory<DemoDbContext> contextFactory)
    : IRequestHandler<UpdateSiteCommand, Site>
{
    public async Task<Site> Handle(UpdateSiteCommand request, CancellationToken cancellationToken)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        var site = await context.Sites.FindAsync([request.Id.Value], cancellationToken: cancellationToken);
        if (site is null)
            throw new ArgumentException("not found");

        site.Name = request.Name;

        /**
         * the bug here is that we were not saving the changes.
         * there's no need to call context.Sites.Update(site) because the entity is already being tracked by the context.
         */
        await context.SaveChangesAsync(cancellationToken);

        return site;
    }
}