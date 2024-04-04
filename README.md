# The Intelecy Demo Application

To start the application run `dotnet run` from the `src/Demo` folder. This will start the app in Development mode using 
data from the Development.db sqlite database.

The application data has a simple structure. There are multiple sites, each with an `id` and a `name`. Each site can 
contain multiple tags (where tag in this context refers to a sensor). Each tag has an `id`, `name`, `site_id`, optional 
`unit` and a `created_at` timestamp.

The application has a graphql API which will be available at https://localhost:7141/graphql.
We recommend using the [Altair GraphQL](https://altairgraphql.dev/) client to consume the API. The GraphQL API is documented
and the documentation can be viewed in the client.

When returning a list of 
items, the API uses the Connection Model as described here https://graphql.org/learn/pagination/, but including the 
helper method `items` that directly lists the nodes.

An example query:
```graphql
query ListSites {
  sites {
    items {
      id
      name
      tags {
        items {
          id
          name
          unit
          createdAt
        }
      }
    }
  }
}
```

This query will list all of the sites in the app and all of the tags for each site.

The API uses global ID's and the node interface as described at https://graphql.org/learn/global-object-identification/.

An example query for fetching a single node:
```graphql
query GetById {
  node(id: "SiteId:3") {
    id
    ... on Tag {
      name
      unit
      createdAt
    }
    ... on Site {
      name
    }
  }
}
```

This query will return all of the fields for either a `Tag` or a `Site`. Try using `TagId:2` as the id to see a `Tag`
returned.

An example mutation:
```graphql
mutation CreateSite {
  createSite(input: { name: "Test"}) {
    site {
      id
      name
    }
  }
}
```

This mutation will create a new site in the application.

## Development

The code uses the CQRS model, with the handlers in `src/Demo.Core/CQRS`. Mediator is used to send requests to the 
handlers. The handlers use strongly typed id's provided by https://github.com/SteveDunn/Vogen.

Entity Framework Core is used for persisting data with the database being Sqlite. Configuration for the tables is 
in `src/Demo.Core/Configuration`. The persistence entities are in `src/Demo.Core/Persistence`.

When developing, there is a useful flag that can be enabled in `Program.cs`:
```csharp
 options.ThrowOnUnhandledException = true;
```
This allows the developer to stop GraphQL dotnet from catching and wrapping exceptions.

### Tests

Tests can be run using `dotnet test` or in an IDE.

### Migrations

To update the persistence data, migrations are used in `src/Demo.Core`.

```shell
# create migration
dotnet ef --startup-project ../Demo migrations add Initial

# apply migration (local use, use scripts for prod)
dotnet ef --startup-project ../Demo database update
```
