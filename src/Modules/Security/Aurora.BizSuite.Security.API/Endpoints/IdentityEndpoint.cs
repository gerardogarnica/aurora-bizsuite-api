using Aurora.BizSuite.Security.Application.Identity.Login;
using Aurora.Framework.Identity;

namespace Aurora.BizSuite.Security.API.Endpoints;

public class IdentityEndpoint : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("aurora/bizsuite/auth")
            .WithTags("Auth")
            .RequireAuthorization();

        AddLogin(group);
    }

    static RouteHandlerBuilder AddLogin(RouteGroupBuilder routeGroup)
    {
        return routeGroup.MapPost(
            "/login",
            async ([FromHeader] string application, [FromBody] UserCredentials request, ISender sender) =>
            {
                var command = new LoginCommand(request.Email, request.Password)
                {
                    Application = application
                };

                var result = await sender.Send(command);

                return result.IsSuccessful
                    ? Results.Created(string.Empty, result.Value)
                    : result.ToBadRequest();
            })
            .WithName("Login")
            .Produces<IdentityToken>(StatusCodes.Status201Created)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .AllowAnonymous();
    }
}