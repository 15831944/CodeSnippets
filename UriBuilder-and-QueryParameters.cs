public static Uri Append(this Uri uri, params string[] paths)
{
    return new Uri(paths.Aggregate(uri.AbsoluteUri, (current, path) =>
        string.Format("{0}/{1}", current.TrimEnd('/'), path.TrimStart('/'))));
}

// Query string parameters
public class QueryParameters
{
    private Dictionary<string, string> _parms = new Dictionary<string, string>();

    public void Add(string key, string val)
    {
        if (_parms.ContainsKey(key))
        {
            throw new InvalidOperationException(string.Format("The key {0} already exists.", key));
        }
        _parms.Add(key, val);
    }

    public string this[string key]
    {
        get { return _parms[key]; }
        set { this.Add(key, value); }
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (var kvp in _parms)
        {
            if (sb.Length > 0) { sb.Append("&"); }
            sb.AppendFormat("{0}={1}",
                HttpUtility.UrlEncode(kvp.Key),
                HttpUtility.UrlEncode(kvp.Value));
        }
        return sb.ToString();
    }
}

// Usage:
var parms = new ParameterCollection();
parms.Add("key", "value");

var builder = new UriBuilder(urls);
builder.Query = parameters.ToString();
