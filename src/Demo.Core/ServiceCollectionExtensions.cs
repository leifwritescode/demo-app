using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
    {
        return services.AddDbContextFactory<DemoDbContext>(options =>
            options.UseSqlite(connectionString, o =>
            {
                o.MigrationsAssembly("Demo.Core");
                // https://docs.microsoft.com/en-au/ef/core/querying/single-split-queries
                o.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
                // https://learn.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency
                // o.EnableRetryOnFailure();
                o.UseNodaTime();
            })
        );
    }
}