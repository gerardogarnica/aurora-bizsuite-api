using Microsoft.AspNetCore.Routing;

namespace Aurora.Framework.Api;

public interface IBaseEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}