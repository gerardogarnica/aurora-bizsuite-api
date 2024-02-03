namespace Aurora.BizSuite.Settings.Domain;

public static class DomainErrors
{
    public static class OptionErrors
    {
        public static readonly BaseError OptionNotFound = new(
            "Options.NotFound",
            "Option not found.");

        public static readonly BaseError SystemOptionNotAllowedToUpdate = new(
            "Options.SystemOptionNotAllowedToUpdate",
            "System option cannot be updated.");

        public static readonly BaseError OptionItemCodeAlreadyExists = new(
            "Options.OptionItemCodeAlreadyExists",
            "Option item code already exists and cannot be created again.");
    }
}