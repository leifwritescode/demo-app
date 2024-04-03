using GraphQL.Types.Relay.DataObjects;

namespace Demo;

public static class ConnectionUtils
{
    private const string Prefix = "cursor";
    
    public static int CursorToId(string cursor)
    {
        return int.Parse(cursor.Base64Decode()[(Prefix.Length + 1)..]);
    }

    public static string IdToCursor(int offset)
    {
        return $"{Prefix}:{offset}".Base64Encode();
    }

    public static Connection<TSource> ToConnection<TSource>(IList<TSource> data, int count, Func<TSource, int> idFunc)
    {
        var edges = data.Take(count).Select(x => new Edge<TSource>
        {
            Node = x,
            Cursor = IdToCursor(idFunc(x)),
        }).ToList();

        return new Connection<TSource>
        {
            Edges = edges,
            PageInfo = new PageInfo
            {
                StartCursor = edges.FirstOrDefault()?.Cursor,
                EndCursor = edges.LastOrDefault()?.Cursor,
                HasPreviousPage = false, // only forward connections
                HasNextPage = data.Count > count,
            }
        };
    }
}