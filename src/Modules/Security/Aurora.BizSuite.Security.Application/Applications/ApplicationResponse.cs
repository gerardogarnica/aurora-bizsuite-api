namespace Aurora.BizSuite.Security.Application.Applications;

public sealed record ApplicationResponse(
    Guid Id,
    string Name,
    string Description,
    bool HasCustomConfiguration);

internal static class ApplicationExtensions
{
    internal static ApplicationResponse ToApplicationResponse(
        this Domain.Applications.Application application)
    {
        return new ApplicationResponse(
            application.Id.Value,
            application.Name,
            application.Description,
            application.HasCustomConfiguration);
    }
}