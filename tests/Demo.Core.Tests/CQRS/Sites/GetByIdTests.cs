using Demo.Core.CQRS.Sites;
using Demo.Core.Ids;

namespace Demo.Core.Tests.CQRS.Sites;

public class GetByIdTests(Fixture fixture) : IClassFixture<Fixture>
{
    private static readonly SiteComparer Comparer = new();
    
    private GetSiteByIdQueryHandler CreateHandler()
    {
        return new GetSiteByIdQueryHandler(new TestDbContextFactory(fixture.Options));
    }

    [Fact]
    public async Task GetExisting()
    {
        var handler = CreateHandler();
        var site = await handler.Handle(new GetSiteByIdQuery(SiteId.From(fixture.Sites[0].Id)), default);
        Assert.NotNull(site);
        Assert.Equal(fixture.Sites[0], site, Comparer);
    }

    [Fact]
    public async Task GetMissing()
    {
        var handler = CreateHandler();
        var site = await handler.Handle(new GetSiteByIdQuery(SiteId.From(236236)), default);
        Assert.Null(site);
    }
}