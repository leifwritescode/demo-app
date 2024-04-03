using Demo.Core.Ids;
using Demo.Core.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Demo.Core.CQRS.Tags;

public record GetTagByIdQuery(TagId Id) : IRequest<Tag?>;

public class GetTagByIdQueryHandler(IDbContextFactory<DemoDbContext> contextFactory)
    : IRequestHandler<GetTagByIdQuery, Tag?>
{
    public async Task<Tag?> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        return await context.Tags.FindAsync([request.Id.Value], cancellationToken: cancellationToken);
    }
}