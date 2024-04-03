using Demo.Core.Persistence;

namespace Demo.Core.Tests.CQRS.Sites;

public class SiteComparer : IEqualityComparer<Site>
{
    public bool Equals(Site? x, Site? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        if (x.GetType() != y.GetType()) return false;
        return x.Id == y.Id && x.Name == y.Name;
    }

    public int GetHashCode(Site obj)
    {
        return HashCode.Combine(obj.Id, obj.Name);
    }
}