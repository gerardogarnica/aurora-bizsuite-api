using Aurora.BizSuite.Items.Application.Categories.Update;

namespace Aurora.BizSuite.Items.Presentation.Categories;

internal sealed class UpdateCategory : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(
            "categories/{id}",
            async (
                Guid id,
                [FromBody] UpdateCategoryRequest request,
                ISender sender) =>
            {
                var command = new UpdateCategoryCommand(
                    id,
                    request.Name,
                    request.Notes);

                Result result = await sender.Send(command);

                return result.Match(
                    () => Results.Accepted(string.Empty),
                    ApiResponses.Problem);
            })
            .WithName("UpdateCategory")
            .WithTags(EndpointTags.Category)
            .Produces(StatusCodes.Status202Accepted)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }

    internal sealed record UpdateCategoryRequest(
        string Name,
        string? Notes);
}