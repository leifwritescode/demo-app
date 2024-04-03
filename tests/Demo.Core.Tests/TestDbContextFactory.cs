using Microsoft.EntityFrameworkCore;

namespace Demo.Core.Tests;

public class TestDbContextFactory(DbContextOptions<DemoDbContext> options): IDbContextFactory<DemoDbContext>
{
    public DemoDbContext CreateDbContext()
    {
        return new DemoDbContext(options);
    }
}