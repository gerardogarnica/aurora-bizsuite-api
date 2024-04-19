using Aurora.BizSuite.Security.Application.Users.GetList;

namespace Aurora.BizSuite.Security.API.Endpoints.Users;

public class GetPagedUsers : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "users/",
            async (
                [FromQuery] int page,
                [FromQuery] int pageSize,
                [FromQuery] Guid? roleId,
                [FromQuery] string? searchTerms,
                [FromQuery] bool onlyActives,
                ISender sender) =>
            {
                var query = new GetUserListQuery(
                    new Framework.PagedViewRequest(page, pageSize),
                    roleId,
                    searchTerms,
                    onlyActives);

                var result = await sender.Send(query);

                return result.IsSuccessful
                    ? Results.Ok(result.Value)
                    : Results.NoContent();
            })
            .WithName("GetPagedUsers")
            .WithTags(EndpointTags.User)
            .Produces(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }
}