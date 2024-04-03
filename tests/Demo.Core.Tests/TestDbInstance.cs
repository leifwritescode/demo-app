using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Demo.Core.Tests;

public sealed class TestDbInstance : IDisposable
{
    public DbContextOptions<DemoDbContext> Options { get; }
    private readonly SqliteConnection _connection;

    public TestDbInstance()
    {
        // Use a Guid to get a unique db instance per TestDbInstance
        var id = Guid.NewGuid();
        var connectionString = $"Data Source={id};Mode=Memory;Cache=Shared";
        Options = new DbContextOptionsBuilder<DemoDbContext>()
            .UseSqlite(connectionString, o => o.UseNodaTime())
            .EnableSensitiveDataLogging()
            .Options;

        // keep the connection open to stop it being deleted
        _connection = new SqliteConnection(connectionString);
        _connection.Open();
        
        using var context = new DemoDbContext(Options);
        context.Database.EnsureCreated();
        context.SaveChanges();
    }

    public void Dispose()
    {
        _connection.Close();
    }
}