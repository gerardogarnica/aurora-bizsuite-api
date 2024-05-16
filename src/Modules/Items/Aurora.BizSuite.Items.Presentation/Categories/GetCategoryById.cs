using Aurora.BizSuite.Items.Application.Categories;
using Aurora.BizSuite.Items.Application.Categories.GetById;

namespace Aurora.BizSuite.Items.Presentation.Categories;

internal sealed class GetCategoryById : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "categories/{id}",
            async (Guid id, ISender sender) =>
            {
                Result<CategoryModel> result = await sender.Send(new GetCategoryByIdQuery(id));

                return result.Match(Results.Ok, ApiResponses.Problem);
            })
            .WithName("GetCategoryById")
            .WithTags(EndpointTags.Category)
            .Produces<CategoryModel>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status204NoContent)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }
}