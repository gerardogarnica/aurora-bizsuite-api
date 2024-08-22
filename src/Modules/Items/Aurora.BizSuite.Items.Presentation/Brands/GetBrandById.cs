using Aurora.BizSuite.Items.Application.Brands;
using Aurora.BizSuite.Items.Application.Brands.GetById;

namespace Aurora.BizSuite.Items.Presentation.Brands;

internal sealed class GetBrandById : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "brands/{id}",
            async (Guid id, ISender sender) =>
            {
                Result<BrandModel> result = await sender.Send(new GetBrandByIdQuery(id));

                return result.Match(Results.Ok, ApiResponses.Problem);
            })
            .WithName("GetBrandById")
            .WithTags(EndpointTags.Brand)
            .Produces<BrandModel>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status204NoContent)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }
}