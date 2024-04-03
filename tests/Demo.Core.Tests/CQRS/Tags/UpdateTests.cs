using Demo.Core.CQRS.Tags;
using Demo.Core.Ids;

namespace Demo.Core.Tests.CQRS.Tags;

public class UpdateTests(Fixture fixture) : IClassFixture<Fixture>
{
    private UpdateTagCommandHandler CreateHandler()
    {
        return new UpdateTagCommandHandler(new TestDbContextFactory(fixture.Options));
    }

    [Fact]
    public async Task UpdateSuccess()
    {
        var handler = CreateHandler();

        var id = TagId.From(fixture.Tags[2].Id);
        const string newName = "sdfvdsfv";
        const string newUnit = "dfvb";

        var tag = await handler.Handle(new UpdateTagCommand(id, newName, newUnit), default);
        // make sure some fields not changed
        Assert.Equal(fixture.Sites[0].Id, tag.SiteId);
        // make sure others are changed
        Assert.Equal(newName, tag.Name);
        Assert.Equal(newUnit, tag.Unit);
        
        // fetch from DB
        await using var context = new DemoDbContext(fixture.Options);
        var dbTag = await context.Tags.FindAsync(id.Value);
        Assert.NotNull(dbTag);
        // make sure some fields not changed
        Assert.Equal(fixture.Sites[0].Id, dbTag.SiteId);
        // make sure others are changed
        Assert.Equal(newName, dbTag.Name);
        Assert.Equal(newUnit, dbTag.Unit);
    }

    [Fact]
    public async Task UpdateFailure()
    {
        var handler = CreateHandler();

        const string newName = "sdfvdsfv";
        var id = TagId.From(345234);

        await Assert.ThrowsAsync<ArgumentException>(() => 
            handler.Handle(new UpdateTagCommand(id, newName, null), default)
        );
    }
}