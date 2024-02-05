using Microsoft.AspNetCore.Builder;

namespace Aurora.Framework.Api;

public interface IBaseEndpoints
{
    void AddRoutes(WebApplication app);
}