using Aurora.BizSuite.Items.Application.Categories.Create;

namespace Aurora.BizSuite.Items.Presentation.Categories;

internal sealed class CreateCategory : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
            "categories",
            async (
                [FromBody] CreateCategoryRequest request,
                ISender sender) =>
            {
                var command = new CreateCategoryCommand(
                    request.Name.Trim(),
                    request.Notes?.Trim());

                Result<Guid> result = await sender.Send(command);

                return result.Match(
                    () => Results.Created(string.Empty, result.Value),
                    ApiResponses.Problem);
            })
            .WithName("CreateCategory")
            .WithTags(EndpointTags.Category)
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }

    internal sealed record CreateCategoryRequest(
        string Name,
        string? Notes);
}