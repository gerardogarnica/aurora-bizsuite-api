using Aurora.BizSuite.Security.Application.Users.Create;
using Aurora.BizSuite.Security.Application.Users.Update;

namespace Aurora.BizSuite.Security.API.Endpoints;

public class UserEndpoints : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("aurora/bizsuite/users")
            .WithTags("User")
            .RequireAuthorization();

        CreateUser(group);
        UpdateUser(group);
    }

    static RouteHandlerBuilder CreateUser(RouteGroupBuilder routeGroup)
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

    static RouteHandlerBuilder UpdateUser(RouteGroupBuilder routeGroup)
    {
        return routeGroup.MapPut(
            "/update",
            async ([FromBody] UpdateUserCommand request, ISender sender) =>
            {
                var command = new UpdateUserCommand(
                    request.Email,
                    request.FirstName,
                    request.LastName,
                    request.Notes);

                var result = await sender.Send(command);

                return result.IsSuccessful
                    ? Results.Accepted(string.Empty)
                    : result.ToBadRequest();
            })
            .WithName("UpdateUser")
            .Produces(StatusCodes.Status202Accepted)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .AllowAnonymous();
    }
}