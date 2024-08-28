using Aurora.BizSuite.Items.Application.Items.RemoveUnit;

namespace Aurora.BizSuite.Items.Presentation.Items;

internal sealed class RemoveItemUnit : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(
            "items/units/remove/{id}",
            async (Guid id, [FromBody] RemoveItemUnitRequest request, ISender sender) =>
            {
                var command = new RemoveItemUnitCommand(id, request.ItemUnitId);

                Result result = await sender.Send(command);

                return result.Match(
                    () => Results.Accepted(string.Empty),
                    ApiResponses.Problem);
            })
            .WithName("RemoveItemUnit")
            .WithTags(EndpointTags.Item)
            .Produces(StatusCodes.Status202Accepted)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }

    internal sealed record RemoveItemUnitRequest(Guid ItemUnitId);
}