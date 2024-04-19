using Aurora.BizSuite.Security.Application.Identity.Login;
using Aurora.Framework.Identity;

namespace Aurora.BizSuite.Security.API.Endpoints.Identity;

public class Login : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
            "auth/login",
            async ([FromHeader] Guid application, [FromBody] UserCredentials request, ISender sender) =>
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
            .WithTags(EndpointTags.Identity)
            .Produces<IdentityToken>(StatusCodes.Status201Created)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .AllowAnonymous();
    }
}