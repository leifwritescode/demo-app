using Demo.Core.CQRS.Sites;

namespace Demo.Core.Tests.CQRS.Sites;

public class CreateTests(Fixture fixture) : IClassFixture<Fixture>
{
    private CreateSiteCommandHandler CreateHandler()
    {
        return new CreateSiteCommandHandler(new TestDbContextFactory(fixture.Options));
    }

    [Fact]
    public async Task CreateTest()
    {
        var handler = CreateHandler();

        const string name = "test";

        var site = await handler.Handle(new CreateSiteCommand(name), default);
        Assert.Equal(name, site.Name);
        
        // fetch from DB
        await using var context = new DemoDbContext(fixture.Options);
        var dbSite = await context.Sites.FindAsync(site.Id);
        Assert.NotNull(dbSite);
        Assert.Equal(name, dbSite.Name);
    }
}