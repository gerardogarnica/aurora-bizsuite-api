using Aurora.BizSuite.Items.Application.Items.Update;

namespace Aurora.BizSuite.Items.Presentation.Items;

internal sealed class UpdateItem : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(
            "items/{id}",
            async (Guid id, [FromBody] UpdateItemRequest request, ISender sender) =>
            {
                var command = new UpdateItemCommand(
                    id,
                    request.Name,
                    request.Description,
                    request.BrandId,
                    request.AlternativeCode,
                    request.Notes,
                    request.Tags);

                Result result = await sender.Send(command);

                return result.Match(
                    () => Results.Accepted(string.Empty),
                    ApiResponses.Problem);
            })
            .WithName("UpdateItem")
            .WithTags(EndpointTags.Item)
            .Produces<Guid>(StatusCodes.Status202Accepted)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }

    internal sealed record UpdateItemRequest(
        string Name,
        string Description,
        Guid BrandId,
        string? AlternativeCode,
        string? Notes,
        List<string> Tags);
}