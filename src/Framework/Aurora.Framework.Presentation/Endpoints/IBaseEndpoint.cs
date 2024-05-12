using Microsoft.AspNetCore.Routing;

namespace Aurora.Framework.Presentation.Endpoints;

public interface IBaseEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}