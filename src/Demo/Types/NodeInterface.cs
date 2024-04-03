using GraphQL.Types;

namespace Demo.Types;

///
public sealed class NodeInterface : InterfaceGraphType
{
    ///
    public NodeInterface()
    {
        Name = "Node";
        Field<NonNullGraphType<IdGraphType>>("id")
            .Description("Global node Id");
    }
}