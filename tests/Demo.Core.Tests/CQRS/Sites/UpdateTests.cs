using Demo.Core.CQRS.Sites;
using Demo.Core.Ids;

namespace Demo.Core.Tests.CQRS.Sites;

public class UpdateTests(Fixture fixture) : IClassFixture<Fixture>
{
    private UpdateSiteCommandHandler CreateHandler()
    {
        return new UpdateSiteCommandHandler(new TestDbContextFactory(fixture.Options));
    }

    [Fact]
    public async Task UpdateSuccess()
    {
        var handler = CreateHandler();

        const string newName = "sdfvdsfv";
        var id = SiteId.From(fixture.Sites[2].Id);

        var site = await handler.Handle(new UpdateSiteCommand(id, newName), default);
        Assert.Equal(newName, site.Name);
        
        // fetch from DB
        await using var context = new DemoDbContext(fixture.Options);
        var dbSite = await context.Sites.FindAsync(fixture.Sites[2].Id);
        Assert.NotNull(dbSite);
        Assert.Equal(newName, dbSite.Name);
    }

    [Fact]
    public async Task UpdateFailure()
    {
        var handler = CreateHandler();

        const string newName = "sdfvdsfv";
        var id = SiteId.From(345234);

        await Assert.ThrowsAsync<ArgumentException>(() => 
            handler.Handle(new UpdateSiteCommand(id, newName), default)
        );
    }
}