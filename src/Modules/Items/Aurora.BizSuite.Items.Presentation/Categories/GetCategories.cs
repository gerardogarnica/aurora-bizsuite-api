using Aurora.BizSuite.Items.Application.Categories;
using Aurora.BizSuite.Items.Application.Categories.GetList;

namespace Aurora.BizSuite.Items.Presentation.Categories;

internal sealed class GetCategories : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "categories",
            async (
                [FromQuery] Guid? parentId,
                [FromQuery] string? searchTerms,
                ISender sender) =>
            {
                var query = new GetCategoryListQuery(parentId, searchTerms);

                Result<IReadOnlyCollection<CategoryModel>> result = await sender.Send(query);

                return result.Match(Results.Ok, ApiResponses.Problem);
            })
            .WithName("GetCategories")
            .WithTags(EndpointTags.Category)
            .Produces<PagedResult<CategoryModel>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }
}