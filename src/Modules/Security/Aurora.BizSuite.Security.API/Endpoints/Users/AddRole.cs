using Aurora.BizSuite.Security.Application.Users.UpdateRole;

namespace Aurora.BizSuite.Security.API.Endpoints.Users;

public class AddRole : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(
            "users/{userId}/roles/add/{roleId}",
            async (Guid userId, Guid roleId, ISender sender) =>
            {
                var command = new UpdateUserRoleCommand(userId, roleId, true);

                var result = await sender.Send(command);

                return result.IsSuccessful
                    ? Results.Accepted(string.Empty)
                    : result.ToBadRequest();
            })
            .WithName("AddRole")
            .WithTags(EndpointTags.User)
            .Produces(StatusCodes.Status202Accepted)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }
}