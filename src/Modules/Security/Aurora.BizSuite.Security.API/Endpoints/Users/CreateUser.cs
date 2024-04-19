using Aurora.BizSuite.Security.Application.Users.Create;

namespace Aurora.BizSuite.Security.API.Endpoints.Users;

public class CreateUser : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
            "users/create",
            async ([FromBody] CreateUserCommand request, ISender sender) =>
            {
                var result = await sender.Send(request);

                return result.IsSuccessful
                    ? Results.Created(string.Empty, result.Value)
                    : result.ToBadRequest();
            })
            .WithName("CreateUser")
            .WithTags(EndpointTags.User)
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }
}