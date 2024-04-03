using Demo.Core.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Demo.Core.Abstractions;

public interface IDemoDbContext
{
    DbSet<Site> Sites { get; }
    DbSet<Tag> Tags { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}