using Demo.Core.Ids;

namespace Demo;

// An example version of IdEncoding
public static class IdEncoding
{
    public static string Encode(IId id)
    {
        return $"{id.GetType().Name}:{id.Value}";
    }

    public static IId Decode(string encoded)
    {
        var parts = encoded.Split(':');
        if (parts.Length != 2)
            throw new ArgumentException("id must have 2 parts");
        
        if (!int.TryParse(parts[1], out var id))
            throw new ArgumentException("invalid id");

        return parts[0] switch
        {
            "SiteId" => SiteId.From(id),
            "TagId" => TagId.From(id),
            _ => throw new ArgumentException($"unknown id type {parts[0]}")
        };
    }

    public static T? Decode<T>(string encoded) where T : struct, IId
    {
        var id = Decode(encoded);

        if (id is T t)
        {
            return t;
        }

        return null;
    }
}
