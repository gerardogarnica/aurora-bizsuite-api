using Aurora.BizSuite.Items.Application.Items;
using Aurora.BizSuite.Items.Application.Items.GetById;

namespace Aurora.BizSuite.Items.Presentation.Items;

internal sealed class GetItemById : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "items/{id}",
            async (Guid id, ISender sender) =>
            {
                Result<ItemModel> result = await sender.Send(new GetItemByIdQuery(id));

                return result.Match(Results.Ok, ApiResponses.Problem);
            })
            .WithName("GetItemById")
            .WithTags(EndpointTags.Item)
            .Produces<ItemModel>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status204NoContent)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }
}