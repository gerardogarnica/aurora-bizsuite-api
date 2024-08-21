using Aurora.BizSuite.Items.Application.Categories.Lock;

namespace Aurora.BizSuite.Items.Presentation.Categories;

internal sealed class LockCategory : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(
            "categories/lock/{id}",
            async (Guid id, ISender sender) =>
            {
                var command = new LockCategoryCommand(id);

                Result result = await sender.Send(command);

                return result.Match(
                    () => Results.Accepted(string.Empty),
                    ApiResponses.Problem);
            })
            .WithName("LockCategory")
            .WithTags(EndpointTags.Category)
            .Produces(StatusCodes.Status202Accepted)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }
}