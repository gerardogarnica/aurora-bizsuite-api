using Aurora.BizSuite.Items.Application.Items.RemoveRelated;

namespace Aurora.BizSuite.Items.Presentation.Items;

internal sealed class RemoveRelatedItem : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete(
            "items/related/remove/{id}",
            async (Guid id, [FromBody] RemoveRelatedItemRequest request, ISender sender) =>
            {
                var command = new RemoveRelatedItemCommand(id, request.RelatedItemId);

                Result result = await sender.Send(command);

                return result.Match(
                    () => Results.Accepted(string.Empty),
                    ApiResponses.Problem);
            })
            .WithName("RemoveRelatedItem")
            .WithTags(EndpointTags.Item)
            .Produces(StatusCodes.Status202Accepted)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }

    internal sealed record RemoveRelatedItemRequest(Guid RelatedItemId);
}