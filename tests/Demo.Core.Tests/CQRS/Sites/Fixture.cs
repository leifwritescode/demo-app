using Demo.Core.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Demo.Core.Tests.CQRS.Sites;

public sealed class Fixture : IDisposable
{
    private readonly TestDbInstance _db = new();
    public DbContextOptions<DemoDbContext> Options => _db.Options;

    public Site[] Sites { get; }

    public Fixture()
    {
        using var context = new DemoDbContext(Options);

        Sites =
        [
            new Site("test0"),
            new Site("test1"),
            new Site("test2")
        ];

        context.Sites.AddRange(Sites);

        context.SaveChanges();
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}