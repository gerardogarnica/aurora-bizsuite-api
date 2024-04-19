using Aurora.BizSuite.Security.Application.Users.GetById;

namespace Aurora.BizSuite.Security.API.Endpoints.Users;

public class GetUserById : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "users/{id}",
            async (Guid id, ISender sender) =>
            {
                var query = new GetUserByIdQuery(id);

                var result = await sender.Send(query);

                return result.IsSuccessful
                    ? Results.Ok(result.Value)
                    : Results.NoContent();
            })
            .WithName("GetUserById")
            .WithTags(EndpointTags.User)
            .Produces(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status204NoContent)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }
}