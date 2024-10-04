namespace Aurora.Framework;

public readonly record struct WebUri
{
    public string Value { get; }

    private WebUri(string value) => Value = value;

    public static Result<WebUri> Create(string uri)
    {
        if (uri == null)
            return Result.Fail<WebUri>(ValueObjectErrors.InvalidWebSiteAddress);

        if (!Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            return Result.Fail<WebUri>(ValueObjectErrors.InvalidWebSiteAddress);

        return new WebUri(uri);
    }
}