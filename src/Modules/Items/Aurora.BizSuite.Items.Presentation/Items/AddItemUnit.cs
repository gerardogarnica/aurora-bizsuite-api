using Aurora.BizSuite.Items.Application.Items.AddUnit;

namespace Aurora.BizSuite.Items.Presentation.Items;

internal sealed class AddItemUnit : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
            "items/units/add/{id}",
            async (Guid id, [FromBody] AddItemUnitRequest request, ISender sender) =>
            {
                var command = new AddItemUnitCommand(
                    id,
                    request.UnitId,
                    request.AvailableForReceipt,
                    request.AvailableForDispatch,
                    request.UseDecimals);

                Result result = await sender.Send(command);

                return result.Match(
                    () => Results.Created(),
                    ApiResponses.Problem);
            })
            .WithName("AddItemUnit")
            .WithTags(EndpointTags.Item)
            .Produces(StatusCodes.Status201Created)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }

    internal sealed record AddItemUnitRequest(
        Guid UnitId,
        bool AvailableForReceipt,
        bool AvailableForDispatch,
        bool UseDecimals);
}