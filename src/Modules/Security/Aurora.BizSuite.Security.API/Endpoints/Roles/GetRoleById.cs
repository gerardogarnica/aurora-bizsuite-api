using Aurora.BizSuite.Security.Application.Roles.GetById;

namespace Aurora.BizSuite.Security.API.Endpoints.Roles;

public class GetRoleById : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "roles/{id}",
            async (Guid id, ISender sender) =>
            {
                var query = new GetRoleByIdQuery(id);

                var result = await sender.Send(query);

                return result.IsSuccessful
                    ? Results.Ok(result.Value)
                    : Results.NoContent();
            })
            .WithName("GetRoleById")
            .WithTags(EndpointTags.Role)
            .Produces(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status204NoContent)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }
}