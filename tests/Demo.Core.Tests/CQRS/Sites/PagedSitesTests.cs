using Demo.Core.CQRS.Sites;

namespace Demo.Core.Tests.CQRS.Sites;

public class PagedSitesTests(Fixture fixture) : IClassFixture<Fixture>
{
    private static readonly SiteComparer Comparer = new();
    
    private PagedSitesQueryHandler CreateHandler()
    {
        return new PagedSitesQueryHandler(new TestDbContextFactory(fixture.Options));
    }

    [Fact]
    public async Task GetAllTest()
    {
        var handler = CreateHandler();
        var sites = await handler.Handle(new PagedSitesQuery(10, 0), default);
        
        Assert.Equal(3, sites.Count);
        Assert.Equal(fixture.Sites, sites, Comparer);
    }
    
    [Fact]
    public async Task GetPagedTest()
    {
        var handler = CreateHandler();
        var sites = await handler.Handle(new PagedSitesQuery(2, 0), default);
        
        Assert.Equal(2, sites.Count);
        Assert.Equal(fixture.Sites[..2], sites, Comparer);
    }
    
    [Fact]
    public async Task GetPagedAfterTest()
    {
        var handler = CreateHandler();
        var sites = await handler.Handle(new PagedSitesQuery(2, fixture.Sites[1].Id), default);
        
        Assert.Single(sites);
        Assert.Equal(fixture.Sites[2], sites[0], Comparer);
    }
}