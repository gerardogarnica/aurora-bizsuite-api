using Microsoft.AspNetCore.Http;

namespace Aurora.BizSuite.Security.Infrastructure.Services;

public sealed class ApplicationProvider(IHttpContextAccessor httpContextAccessor)
{
    private const string AppIdClaimTypeName = "app";

    public Guid GetApplicationId()
    {
        var principal = httpContextAccessor.HttpContext?.User;
        if (principal is null) return new Guid();

        var appIdClaim = principal.FindFirst(AppIdClaimTypeName);
        if (appIdClaim is null) return new Guid();

        return new Guid(appIdClaim.Value);
    }
}