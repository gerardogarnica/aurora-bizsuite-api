using Aurora.BizSuite.Items.Application.Items.RemoveDescription;

namespace Aurora.BizSuite.Items.Presentation.Items;

internal sealed class RemoveItemDescription : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete(
            "items/descriptions/remove/{id}",
            async (Guid id, [FromBody] RemoveItemDescriptionRequest request, ISender sender) =>
            {
                var command = new RemoveItemDescriptionCommand(id, request.ItemDescriptionId);

                Result result = await sender.Send(command);

                return result.Match(
                    () => Results.Accepted(string.Empty),
                    ApiResponses.Problem);
            })
            .WithName("RemoveItemDescription")
            .WithTags(EndpointTags.Item)
            .Produces(StatusCodes.Status202Accepted)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }

    internal sealed record RemoveItemDescriptionRequest(Guid ItemDescriptionId);
}