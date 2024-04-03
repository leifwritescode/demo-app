using Demo.Core.Abstractions;
using Demo.Core.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Demo.Core;

public class DemoDbContext : DbContext, IDemoDbContext
{
    public DemoDbContext(DbContextOptions<DemoDbContext> options)
        : base(options)
    {
    }

    public DbSet<Site> Sites => Set<Site>();
    public DbSet<Tag> Tags => Set<Tag>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DemoDbContext).Assembly);
    }
}