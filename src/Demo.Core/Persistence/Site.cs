namespace Demo.Core.Persistence;

public class Site(string name)
{
    public int Id { get; set; }
    public string Name { get; set; } = name;

    // navigation properties
    public IEnumerable<Tag> Tags = null!;
}