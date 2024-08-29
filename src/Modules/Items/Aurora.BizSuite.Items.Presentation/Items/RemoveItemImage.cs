using Aurora.BizSuite.Items.Application.Items.RemoveResource;
using Aurora.BizSuite.Items.Domain.Items;

namespace Aurora.BizSuite.Items.Presentation.Items;

internal sealed class RemoveItemImage : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete(
            "items/images/remove/{id}",
            async (Guid id, [FromBody] RemoveItemImageRequest request, ISender sender) =>
            {
                var command = new RemoveItemResourceCommand(
                    id,
                    request.ItemImageId,
                    ItemConstants.ImageResourceTypeName);

                Result result = await sender.Send(command);

                return result.Match(
                    () => Results.Accepted(string.Empty),
                    ApiResponses.Problem);
            })
            .WithName("RemoveItemImage")
            .WithTags(EndpointTags.Item)
            .Produces(StatusCodes.Status202Accepted)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }

    internal sealed record RemoveItemImageRequest(Guid ItemImageId);
}