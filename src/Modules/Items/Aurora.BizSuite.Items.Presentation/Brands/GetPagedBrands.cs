using Aurora.BizSuite.Items.Application.Brands;
using Aurora.BizSuite.Items.Application.Brands.GetList;

namespace Aurora.BizSuite.Items.Presentation.Brands;

internal sealed class GetPagedBrands : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "brands",
            async (
                [FromQuery] int page,
                [FromQuery] int size,
                [FromQuery] string? searchTerms,
                ISender sender) =>
            {
                var query = new GetBrandListQuery(
                    new PagedViewRequest(page, size),
                searchTerms);

                Result<PagedResult<BrandModel>> result = await sender.Send(query);

                return result.Match(Results.Ok, ApiResponses.Problem);
            })
            .WithName("GetPagedBrands")
            .WithTags(EndpointTags.Brand)
            .Produces<PagedResult<BrandModel>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }
}