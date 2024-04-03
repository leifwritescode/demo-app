using Demo.Core.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Demo.Core.CQRS.Sites;

public record CreateSiteCommand(string Name) : IRequest<Site>;

public class CreateSiteCommandHandler(IDbContextFactory<DemoDbContext> contextFactory)
    : IRequestHandler<CreateSiteCommand, Site>
{
    public async Task<Site> Handle(CreateSiteCommand request, CancellationToken cancellationToken)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        var site = new Site(request.Name);
        await context.Sites.AddAsync(site, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return site;
    }
}