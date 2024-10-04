namespace Aurora.Framework;

internal static class ValueObjectErrors
{
    internal static BaseError FileUriFormatException(string message) => new(
        "FileUriFormatException",
        $"The file uri address is invalid. Uri format error: {message}");

    internal static BaseError FileUriGenericException(string message) => new(
        "FileUriGenericException",
        $"The file uri address is invalid. Error: {message}");

    internal static BaseError InvalidEmailAddress => new(
        "InvalidEmailAddress",
        "The email address is invalid.");

    internal static BaseError InvalidFileUriAddress => new(
        "InvalidFileUriAddress",
        "The file uri address is invalid.");

    internal static BaseError InvalidPhoneNumber => new(
        "InvalidPhoneNumber",
        "The phone number is invalid.");

    internal static BaseError InvalidWebSiteAddress => new(
        "InvalidWebSiteAddress",
        "The website address is invalid.");
}