using Demo;
using Demo.Core;
using Demo.Core.CQRS.Sites;
using GraphQL;
using NodaTime;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (connectionString is null)
    throw new Exception("DefaultConnection string was null");
builder.Services.AddDatabase(connectionString);

builder.Services.AddGraphQL(b => b
    .AddSchema<DemoSchema>()
    .ConfigureExecutionOptions(options =>
    {
        // useful when debugging
        // options.ThrowOnUnhandledException = true;
    })
    .AddSystemTextJson()
    .AddDataLoader()
    .AddGraphTypes(typeof(DemoSchema).Assembly)
);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(PagedSitesQuery).Assembly));

builder.Services.AddSingleton<IClock>(SystemClock.Instance);

var app = builder.Build();

app.UseHttpsRedirection();

app.UseGraphQL();

app.Run();
