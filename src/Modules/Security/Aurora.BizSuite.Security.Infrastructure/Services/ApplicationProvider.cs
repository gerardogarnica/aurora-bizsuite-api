using Microsoft.AspNetCore.Http;

namespace Aurora.BizSuite.Security.Infrastructure.Services;

public sealed class ApplicationProvider(IHttpContextAccessor httpContextAccessor)
{
    private const string AppIdClaimTypeName = "app";
    private const string AdminApplicationCode = "25EE60E9-A6A9-45E8-A899-752C4B4576DC";

    public Guid GetApplicationId()
    {
        var principal = httpContextAccessor.HttpContext?.User;
        if (principal is null) return new Guid();

        var appIdClaim = principal.FindFirst(AppIdClaimTypeName);
        if (appIdClaim is null) return new Guid();

        return new Guid(appIdClaim.Value);
    }

    internal bool IsAdminApp()
    {
        return GetApplicationId().ToString() == AdminApplicationCode;
    }
}