namespace Aurora.Framework;

public readonly record struct FileUri
{
    public string Value { get; }

    private FileUri(string value) => Value = value;

    public static Result<FileUri> Create(string uri)
    {
        if (uri == null)
            return Result.Fail<FileUri>(ValueObjectErrors.InvalidFileUriAddress);

        try
        {
            var uriAddress = new Uri(uri);

            return uriAddress.IsFile
                ? new FileUri(uri)
                : Result.Fail<FileUri>(ValueObjectErrors.InvalidFileUriAddress);
        }
        catch (UriFormatException e)
        {
            return Result.Fail<FileUri>(ValueObjectErrors.FileUriFormatException(e.Message));
        }
        catch (Exception e)
        {
            return Result.Fail<FileUri>(ValueObjectErrors.FileUriGenericException(e.Message));
        }
    }
}