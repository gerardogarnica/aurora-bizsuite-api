using Aurora.BizSuite.Items.Application.Units.Update;

namespace Aurora.BizSuite.Items.Presentation.Units;

internal sealed class UpdateUnit : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(
            "units/{id}",
            async (Guid id, [FromBody] UpdateUnitRequest request, ISender sender) =>
            {
                var command = new UpdateUnitCommand(
                    id,
                    request.Name.Trim(),
                    request.Symbol.Trim(),
                    request.Notes?.Trim());

                Result result = await sender.Send(command);

                return result.Match(
                    () => Results.Accepted(string.Empty),
                    ApiResponses.Problem);
            })
            .WithName("UpdateUnit")
            .WithTags(EndpointTags.Unit)
            .Produces<Guid>(StatusCodes.Status202Accepted)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }

    internal sealed record UpdateUnitRequest(
        string Name,
        string Symbol,
        string? Notes);
}