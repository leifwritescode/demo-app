using System.ComponentModel;
using Demo.Core.CQRS.Sites;
using Demo.Core.Ids;
using Demo.Core.Persistence;
using GraphQL;
using GraphQL.Types;

namespace Demo.Mutations;

public sealed record UpdateSiteInput(
    [property: Description("The id of the site to update")]
    [Id]
    string SiteId,
    [property: Description("The new name")]
    string Name
);

public record UpdateSitePayload(
    [property: Description("The updated site")]
    Site Site
);

public sealed partial class DemoMutation
{
    private void UpdateSite()
    {
        Field<AutoRegisteringObjectGraphType<UpdateSitePayload>>("updateSite")
            .Description("Update a site details")
            .Argument<NonNullGraphType<AutoRegisteringInputObjectGraphType<UpdateSiteInput>>>("input")
            .ResolveAsync(async ctx =>
            {
                var input = ctx.GetArgument<UpdateSiteInput>("input");

                // decode the id
                var id = IdEncoding.Decode<SiteId>(input.SiteId);
                if (!id.HasValue)
                    throw new ExecutionError($"Invalid siteId");
                
                var site = await SendCommand(ctx, new UpdateSiteCommand(id.Value, input.Name));
                return new UpdateSitePayload(site);
            });
    }
}