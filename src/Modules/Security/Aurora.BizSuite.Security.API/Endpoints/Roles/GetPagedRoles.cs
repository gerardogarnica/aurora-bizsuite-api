using Aurora.BizSuite.Security.Application.Roles.GetList;

namespace Aurora.BizSuite.Security.API.Endpoints.Roles;

public class GetPagedRoles : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "roles/",
            async (
                [FromQuery] int page,
                [FromQuery] int pageSize,
                [FromQuery] string? searchTerms,
                [FromQuery] bool onlyActives,
                ISender sender) =>
            {
                var query = new GetRoleListQuery(
                    new Framework.PagedViewRequest(page, pageSize),
                    searchTerms,
                    onlyActives);

                var result = await sender.Send(query);

                return result.IsSuccessful
                    ? Results.Ok(result.Value)
                    : Results.NoContent();
            })
            .WithName("GetPagedRoles")
            .WithTags(EndpointTags.Role)
            .Produces(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }
}