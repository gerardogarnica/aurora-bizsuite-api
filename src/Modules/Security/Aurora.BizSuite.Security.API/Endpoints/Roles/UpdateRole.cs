using Aurora.BizSuite.Security.Application.Roles.Update;

namespace Aurora.BizSuite.Security.API.Endpoints.Roles;

public class UpdateRole : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(
            "roles/update",
            async ([FromBody] UpdateRoleCommand request, ISender sender) =>
            {
                var result = await sender.Send(request);

                return result.IsSuccessful
                    ? Results.Accepted(string.Empty)
                    : result.ToBadRequest();
            })
            .WithName("UpdateRole")
            .WithTags(EndpointTags.Role)
            .Produces(StatusCodes.Status202Accepted)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }
}