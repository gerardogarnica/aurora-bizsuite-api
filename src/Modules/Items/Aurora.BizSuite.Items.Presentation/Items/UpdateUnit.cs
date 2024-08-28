using Aurora.BizSuite.Items.Application.Items.UpdateUnit;

namespace Aurora.BizSuite.Items.Presentation.Items;

internal sealed class UpdateUnit : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(
            "items/units/update/{id}",
            async (Guid id, [FromBody] UpdateItemUnitRequest request, ISender sender) =>
            {
                var command = new UpdateItemUnitCommand(
                    id,
                    request.ItemUnitId,
                    request.AvailableForReceipt,
                    request.AvailableForDispatch,
                    request.UseDecimals);

                Result result = await sender.Send(command);

                return result.Match(
                    () => Results.Accepted(string.Empty),
                    ApiResponses.Problem);
            })
            .WithName("UpdateItemUnit")
            .WithTags(EndpointTags.Item)
            .Produces<Guid>(StatusCodes.Status202Accepted)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }

    internal sealed record UpdateItemUnitRequest(
        Guid ItemUnitId,
        bool AvailableForReceipt,
        bool AvailableForDispatch,
        bool UseDecimals);
}