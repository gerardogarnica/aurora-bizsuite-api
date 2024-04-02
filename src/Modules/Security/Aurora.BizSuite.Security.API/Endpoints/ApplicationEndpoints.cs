using Aurora.BizSuite.Security.Application.Applications.GetById;
using Aurora.BizSuite.Security.Application.Applications.GetList;

namespace Aurora.BizSuite.Security.API.Endpoints;

public class ApplicationEndpoints : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("aurora/bizsuite/applications")
            .WithTags("Application")
            .WithOpenApi()
            .RequireAuthorization();

        GetApplicationById(group);
        GetApplicationList(group);
    }

    static RouteHandlerBuilder GetApplicationById(RouteGroupBuilder routeGroup)
    {
        return routeGroup.MapGet(
            "/{id}",
            async (Guid id, ISender sender) =>
            {
                var query = new GetApplicationByIdQuery(id);

                var result = await sender.Send(query);

                return result.IsSuccessful
                    ? Results.Ok(result.Value)
                    : Results.NoContent();
            })
            .WithName("GetApplicationById")
            .Produces(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status204NoContent)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }

    static RouteHandlerBuilder GetApplicationList(RouteGroupBuilder routeGroup)
    {
        return routeGroup.MapGet(
            "/",
            async (Guid id, ISender sender) =>
            {
                var query = new GetApplicationListQuery();

                var result = await sender.Send(query);

                return result.IsSuccessful
                    ? Results.Ok(result.Value)
                    : Results.NoContent();
            })
            .WithName("GetApplicationList")
            .Produces(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }
}