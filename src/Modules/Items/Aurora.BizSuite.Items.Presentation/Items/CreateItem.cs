using Aurora.BizSuite.Items.Application.Items.Create;

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
                    ToType(request.Type),
                    request.MainUnitId,
                    request.AlternativeCode,
                    request.Notes);

                Result<Guid> result = await sender.Send(command);

                return result.Match(
                    () => Results.Created(string.Empty, result.Value),
                    ApiResponses.Problem);
            })
            .WithName("CreateProduct")
            .WithTags(EndpointTags.Item)
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }

    private static Domain.Items.ItemType ToType(CreateItemType type) => type switch
    {
        CreateItemType.Product => Domain.Items.ItemType.Product,
        CreateItemType.Service => Domain.Items.ItemType.Service,
        _ => Domain.Items.ItemType.Product,
    };

    internal sealed record CreateItemRequest(
        string Code,
        string Name,
        string Description,
        Guid CategoryId,
        CreateItemType Type,
        Guid MainUnitId,
        string? AlternativeCode,
        string? Notes);

    internal enum CreateItemType
    {
        Product,
        Service
    }
}