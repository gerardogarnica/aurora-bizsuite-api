using Aurora.BizSuite.Items.Application.Units.Create;

namespace Aurora.BizSuite.Items.Presentation.Units;

internal sealed class CreateUnit : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
            "units",
            async ([FromBody] CreateUnitRequest request, ISender sender) =>
            {
                var command = new CreateUnitCommand(
                    request.Name.Trim(),
                    request.Symbol.Trim(),
                    request.Notes?.Trim());

                Result<Guid> result = await sender.Send(command);

                return result.Match(
                    () => Results.Created(string.Empty, result.Value),
                    ApiResponses.Problem);
            })
            .WithName("CreateUnit")
            .WithTags(EndpointTags.Unit)
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }

    internal sealed record CreateUnitRequest(
        string Name,
        string Symbol,
        string? Notes);
}