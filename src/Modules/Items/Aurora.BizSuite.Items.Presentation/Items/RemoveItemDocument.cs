using Aurora.BizSuite.Items.Application.Items.RemoveResource;
using Aurora.BizSuite.Items.Domain.Items;

namespace Aurora.BizSuite.Items.Presentation.Items;

internal sealed class RemoveItemDocument : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete(
            "items/documents/remove/{id}",
            async (Guid id, [FromBody] RemoveItemDocumentRequest request, ISender sender) =>
            {
                var command = new RemoveItemResourceCommand(
                    id,
                    request.ItemDocumentId,
                    ItemConstants.DocumentResourceTypeName);

                Result result = await sender.Send(command);

                return result.Match(
                    () => Results.Accepted(string.Empty),
                    ApiResponses.Problem);
            })
            .WithName("RemoveItemResource")
            .WithTags(EndpointTags.Item)
            .Produces(StatusCodes.Status202Accepted)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }

    internal sealed record RemoveItemDocumentRequest(Guid ItemDocumentId);
}