using Aurora.BizSuite.Security.Application.Users.Update;

namespace Aurora.BizSuite.Security.API.Endpoints.Users;

public class UpdateUser : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(
            "users/update",
            async ([FromBody] UpdateUserCommand request, ISender sender) =>
            {
                var result = await sender.Send(request);

                return result.IsSuccessful
                    ? Results.Accepted(string.Empty)
                    : result.ToBadRequest();
            })
            .WithName("UpdateUser")
            .WithTags(EndpointTags.User)
            .Produces(StatusCodes.Status202Accepted)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }
}