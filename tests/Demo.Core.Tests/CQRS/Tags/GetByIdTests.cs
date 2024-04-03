using Demo.Core.CQRS.Tags;
using Demo.Core.Ids;

namespace Demo.Core.Tests.CQRS.Tags;

public class GetByIdTests(Fixture fixture) : IClassFixture<Fixture>
{
    private static readonly TagComparer Comparer = new();
    
    private GetTagByIdQueryHandler CreateHandler()
    {
        return new GetTagByIdQueryHandler(new TestDbContextFactory(fixture.Options));
    }

    [Fact]
    public async Task GetExisting()
    {
        var handler = CreateHandler();
        var tag = await handler.Handle(new GetTagByIdQuery(TagId.From(fixture.Tags[0].Id)), default);
        Assert.NotNull(tag);
        Assert.Equal(fixture.Tags[0], tag, Comparer);
    }

    [Fact]
    public async Task GetMissing()
    {
        var handler = CreateHandler();
        var tag = await handler.Handle(new GetTagByIdQuery(TagId.From(236236)), default);
        Assert.Null(tag);
    }
}