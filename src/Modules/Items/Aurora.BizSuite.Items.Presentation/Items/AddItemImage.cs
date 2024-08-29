using Aurora.BizSuite.Items.Application.Items.AddResource;
using Aurora.BizSuite.Items.Domain.Items;

namespace Aurora.BizSuite.Items.Presentation.Items;

internal sealed class AddItemImage : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
            "items/images/add/{id}",
            async (Guid id, [FromBody] AddItemImageRequest request, ISender sender) =>
            {
                var command = new AddItemResourceCommand(
                    id,
                    request.Uri,
                    ItemConstants.ImageResourceTypeName,
                    string.Empty);

                Result result = await sender.Send(command);

                return result.Match(
                    () => Results.Created(),
                    ApiResponses.Problem);
            })
            .WithName("AddItemImage")
            .WithTags(EndpointTags.Item)
            .Produces(StatusCodes.Status201Created)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }

    internal sealed record AddItemImageRequest(string Uri);
}