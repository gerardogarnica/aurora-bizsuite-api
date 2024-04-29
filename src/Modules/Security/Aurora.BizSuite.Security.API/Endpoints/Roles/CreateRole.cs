using Aurora.BizSuite.Security.Application.Roles.Create;

namespace Aurora.BizSuite.Security.API.Endpoints.Roles;

public class CreateRole : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
            "roles/create",
            async ([FromBody] CreateRoleCommand request, ISender sender) =>
            {
                var result = await sender.Send(request);

                return result.IsSuccessful
                    ? Results.Created(string.Empty, result.Value)
                    : result.ToBadRequest();
            })
            .WithName("CreateRole")
            .WithTags(EndpointTags.Role)
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }
}