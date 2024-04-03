using Demo.Core.CQRS.Tags;
using Demo.Core.Ids;

namespace Demo.Core.Tests.CQRS.Tags;

public class PagedTagsTests(Fixture fixture) : IClassFixture<Fixture>
{
    private static readonly TagComparer Comparer = new();
    
    private PagedTagsBySiteIdQueryHandler CreateHandler()
    {
        return new PagedTagsBySiteIdQueryHandler(new TestDbContextFactory(fixture.Options));
    }

    [Fact]
    public async Task GetAllTest()
    {
        var handler = CreateHandler();
        var siteId = SiteId.From(fixture.Sites[0].Id);
        var tags = await handler.Handle(new PagedTagsBySiteIdQuery(siteId, 10, 0), default);
        
        Assert.Equal(3, tags.Count);
        Assert.Equal(fixture.Tags, tags, Comparer);
    }
    
    [Fact]
    public async Task GetPagedTest()
    {
        var handler = CreateHandler();
        var siteId = SiteId.From(fixture.Sites[0].Id);
        var tags = await handler.Handle(new PagedTagsBySiteIdQuery(siteId, 2, 0), default);
        
        Assert.Equal(2, tags.Count);
        Assert.Equal(fixture.Tags[..2], tags, Comparer);
    }
    
    [Fact]
    public async Task GetPagedAfterTest()
    {
        var handler = CreateHandler();
        var siteId = SiteId.From(fixture.Sites[0].Id);
        var tags = await handler.Handle(new PagedTagsBySiteIdQuery(siteId, 2, fixture.Sites[1].Id), default);
        
        Assert.Single(tags);
        Assert.Equal(fixture.Tags[2], tags[0], Comparer);
    }
}