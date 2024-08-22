using Aurora.BizSuite.Items.Application.Items.Create;
using Aurora.BizSuite.Items.Domain.Items;

namespace Aurora.BizSuite.Items.Presentation.Items;

internal sealed class CreateItem : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
            "items",
            async ([FromBody] CreateItemRequest request, ISender sender) =>
            {
                var command = new CreateItemCommand(
                    request.Code,
                    request.Name,
                    request.Description,
                    request.CategoryId,
                    request.BrandId,
                    request.Type,
                    request.AlternativeCode,
                    request.Notes,
                    request.Tags);

                Result<Guid> result = await sender.Send(command);

                return result.Match(
                    () => Results.Created(string.Empty, result.Value),
                    ApiResponses.Problem);
            })
            .WithName("CreateItem")
            .WithTags(EndpointTags.Item)
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }

    internal sealed record CreateItemRequest(
        string Code,
        string Name,
        string Description,
        Guid CategoryId,
        Guid BrandId,
        ItemType Type,
        string? AlternativeCode,
        string? Notes,
        List<string> Tags);
}