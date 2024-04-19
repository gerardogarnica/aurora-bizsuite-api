using Aurora.BizSuite.Security.Application.Users.UpdateStatus;

namespace Aurora.BizSuite.Security.API.Endpoints.Users;

public class InactivateUser : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(
            "users/{userId}/inactivate",
            async (Guid userId, ISender sender) =>
            {
                var command = new UpdateUserStatusCommand(userId, false);

                var result = await sender.Send(command);

                return result.IsSuccessful
                    ? Results.Accepted(string.Empty)
                    : result.ToBadRequest();
            })
            .WithName("InactivateUser")
            .WithTags(EndpointTags.User)
            .Produces(StatusCodes.Status202Accepted)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }
}