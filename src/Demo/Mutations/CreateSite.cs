using System.ComponentModel;
using Demo.Core.CQRS.Sites;
using Demo.Core.Persistence;
using GraphQL;
using GraphQL.Types;

namespace Demo.Mutations;

public sealed record CreateSiteInput(
    [property: Description("The name of the new site")]
    string Name
);

public record CreateSitePayload(
    [property: Description("The newly created site")]
    Site Site
);

public sealed partial class DemoMutation
{
    private void CreateSite()
    {
        Field<AutoRegisteringObjectGraphType<CreateSitePayload>>("createSite")
            .Description("Creates a new site in the application")
            .Argument<NonNullGraphType<AutoRegisteringInputObjectGraphType<CreateSiteInput>>>("input")
            .ResolveAsync(async ctx =>
            {
                var input = ctx.GetArgument<CreateSiteInput>("input");
                var site = await SendCommand(ctx, new CreateSiteCommand(input.Name));
                return new CreateSitePayload(site);
            });
    }
}