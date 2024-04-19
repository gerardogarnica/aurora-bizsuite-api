using Aurora.BizSuite.Security.Application.Users.UpdateRole;

namespace Aurora.BizSuite.Security.API.Endpoints.Users;

public class RemoveRole : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(
            "users/{userId}/roles/remove/{roleId}",
            async (Guid userId, Guid roleId, ISender sender) =>
            {
                var command = new UpdateUserRoleCommand(userId, roleId, false);

                var result = await sender.Send(command);

                return result.IsSuccessful
                    ? Results.Accepted(string.Empty)
                    : result.ToBadRequest();
            })
            .WithName("RemoveRole")
            .WithTags(EndpointTags.User)
            .Produces(StatusCodes.Status202Accepted)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }
}