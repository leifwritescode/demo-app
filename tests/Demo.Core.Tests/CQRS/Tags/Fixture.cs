using Demo.Core.Persistence;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace Demo.Core.Tests.CQRS.Tags;

public sealed class Fixture : IDisposable
{
    private readonly TestDbInstance _db = new();
    public DbContextOptions<DemoDbContext> Options => _db.Options;

    public Site[] Sites { get; }
    public Tag[] Tags { get; }

    public Fixture()
    {
        using var context = new DemoDbContext(Options);

        Sites =
        [
            new Site("test0"),
            new Site("test1"),
        ];

        Tags =
        [
            new Tag("tag0")
            {
                Site = Sites[0],
                Unit = "m",
                CreatedAt = Instant.FromUtc(2024, 04, 02, 00, 00, 00),
            },
            new Tag("tag1")
            {
                Site = Sites[0],
                Unit = "bar",
                CreatedAt = Instant.FromUtc(2024, 04, 02, 01, 00, 00),
            },
            new Tag("tag2")
            {
                Site = Sites[0],
                Unit = "Hz",
                CreatedAt = Instant.FromUtc(2024, 04, 02, 02, 00, 00),
            },
        ];

        context.Sites.AddRange(Sites);
        context.Tags.AddRange(Tags);

        context.SaveChanges();
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}