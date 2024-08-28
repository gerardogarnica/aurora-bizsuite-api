using Aurora.BizSuite.Items.Application.Items.SetPrimaryUnit;

namespace Aurora.BizSuite.Items.Presentation.Items;

internal sealed class SetPrimaryItemUnit : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(
            "items/units/setprimary/{id}",
            async (Guid id, [FromBody] SetPrimaryItemUnitRequest request, ISender sender) =>
            {
                var command = new SetPrimaryItemUnitCommand(id, request.ItemUnitId);

                Result result = await sender.Send(command);

                return result.Match(
                    () => Results.Accepted(string.Empty),
                    ApiResponses.Problem);
            })
            .WithName("SetPrimaryItemUnit")
            .WithTags(EndpointTags.Item)
            .Produces(StatusCodes.Status202Accepted)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }

    internal sealed record SetPrimaryItemUnitRequest(Guid ItemUnitId);
}