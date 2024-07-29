namespace Todo.Api;

public abstract record LinkedResponse
{
    public List<Link> Links { get; set; } = [];
}

public class Link
{
    public string Href { get; set; }
    public string Rel { get; set; }
    public string Method { get; set; }

    public Link()
    {
    }

    public Link(string href, string rel, string method)
    {
        Href = href;
        Rel = rel;
        Method = method;
    }
}

public class LinkResourceBase
{
    public LinkResourceBase()
    {
    }

    public List<Link> Links { get; set; } = [];
}

public class LinkCollectionWrapper<T> : LinkResourceBase
{
    public IEnumerable<T> Value { get; set; } = [];

    public LinkCollectionWrapper()
    {
    }

    public LinkCollectionWrapper(IEnumerable<T> value)
    {
        Value = value;
    }
}