using Aurora.BizSuite.Items.Application.Items.AddUnit;

namespace Aurora.BizSuite.Items.Presentation.Items;

internal sealed class AddUnit : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(
            "items/units/add/{id}",
            async (Guid id, [FromBody] AddUnitRequest request, ISender sender) =>
            {
                var command = new AddItemUnitCommand(
                    id,
                    request.UnitId,
                    request.AvailableForReceipt,
                    request.AvailableForDispatch,
                    request.UseDecimals);

                Result result = await sender.Send(command);

                return result.Match(
                    () => Results.Accepted(string.Empty),
                    ApiResponses.Problem);
            })
            .WithName("AddUnit")
            .WithTags(EndpointTags.Item)
            .Produces<Guid>(StatusCodes.Status202Accepted)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }

    internal sealed record AddUnitRequest(
        Guid UnitId,
        bool AvailableForReceipt,
        bool AvailableForDispatch,
        bool UseDecimals);
}