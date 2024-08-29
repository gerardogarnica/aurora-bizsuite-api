using Aurora.BizSuite.Items.Application.Items.AddRelated;

namespace Aurora.BizSuite.Items.Presentation.Items;

internal sealed class AddRelatedItem : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
            "items/related/add/{id}",
            async (Guid id, [FromBody] AddRelatedItemRequest request, ISender sender) =>
            {
                var command = new AddRelatedItemCommand(id, request.ItemId);

                Result result = await sender.Send(command);

                return result.Match(
                    () => Results.Created(),
                    ApiResponses.Problem);
            })
            .WithName("AddRelatedItem")
            .WithTags(EndpointTags.Item)
            .Produces(StatusCodes.Status201Created)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }

    internal sealed record AddRelatedItemRequest(Guid ItemId);
}