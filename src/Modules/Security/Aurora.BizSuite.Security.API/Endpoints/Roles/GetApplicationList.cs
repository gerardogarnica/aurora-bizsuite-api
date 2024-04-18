using Aurora.BizSuite.Security.Application.Applications.GetList;

namespace Aurora.BizSuite.Security.API.Endpoints.Roles;

public class GetApplicationList : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "roles/applications",
            async (ISender sender) =>
            {
                var query = new GetApplicationListQuery();

                var result = await sender.Send(query);

                return result.IsSuccessful
                    ? Results.Ok(result.Value)
                    : Results.NoContent();
            })
            .WithName("GetApplicationList")
            .WithTags(EndpointTags.Role)
            .Produces(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }
}