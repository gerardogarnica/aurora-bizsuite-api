using Aurora.BizSuite.Security.Application.Users.Create;

namespace Aurora.BizSuite.Security.API.Endpoints;

public class UserEndpoints : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("aurora/bizsuite/users")
            .WithTags("User")
            .RequireAuthorization();

        AddCreateUser(group);
    }

    static RouteHandlerBuilder AddCreateUser(RouteGroupBuilder routeGroup)
    {
        return routeGroup.MapPost(
            "/create",
            async ([FromBody] CreateUserCommand request, ISender sender) =>
            {
                var command = new CreateUserCommand(
                    request.Email,
                    request.FirstName,
                    request.LastName,
                    request.Notes,
                    request.IsEditable);

                var result = await sender.Send(command);

                return result.IsSuccessful
                    ? Results.Created(string.Empty, result.Value)
                    : result.ToBadRequest();
            })
            .WithName("CreateUser")
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .AllowAnonymous();
    }
}