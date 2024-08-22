using Aurora.BizSuite.Items.Application.Items;
using Aurora.BizSuite.Items.Application.Items.GetList;
using Aurora.BizSuite.Items.Domain.Items;

namespace Aurora.BizSuite.Items.Presentation.Items;

internal sealed class GetPagedItems : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "items",
            async (
                [FromQuery] int page,
                [FromQuery] int size,
                [FromQuery] Guid? categoryId,
                [FromQuery] Guid? brandId,
                [FromQuery] ItemType? type,
                [FromQuery] ItemStatus? status,
                [FromQuery] string? searchTerms,
                ISender sender) =>
            {
                var query = new GetItemListQuery(
                    new PagedViewRequest(page, size),
                    categoryId,
                    brandId,
                    type,
                    status,
                    searchTerms);

                Result<PagedResult<ItemModel>> result = await sender.Send(query);

                return result.Match(Results.Ok, ApiResponses.Problem);
            })
            .WithName("GetPagedItems")
            .WithTags(EndpointTags.Item)
            .Produces<ItemModel>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }
}