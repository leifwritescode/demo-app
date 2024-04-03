using Demo.Core.Persistence;
using Demo.Mutations;
using Demo.Types;
using GraphQL.Types;

namespace Demo;

public class DemoSchema : Schema
{
    public DemoSchema(IServiceProvider provider)
        : base(provider)
    {
        Query = provider.GetRequiredService<DemoQuery>();
        Mutation = provider.GetRequiredService<DemoMutation>();

        RegisterTypeMapping<Site, SiteType>();
        RegisterTypeMapping<Tag, TagType>();
    }
    
    private void RegisterTypeMapping<TClr, TGraph>()
    {
        base.RegisterTypeMapping(typeof(TClr), typeof(TGraph));
    }
}