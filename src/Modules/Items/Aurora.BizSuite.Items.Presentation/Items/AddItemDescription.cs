using Aurora.BizSuite.Items.Application.Items.AddDescription;

namespace Aurora.BizSuite.Items.Presentation.Items;

internal sealed class AddItemDescription : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
            "items/descriptions/add/{id}",
            async (Guid id, [FromBody] AddItemDescriptionRequest request, ISender sender) =>
            {
                var command = new AddItemDescriptionCommand(
                    id,
                    request.Type,
                    request.Description);

                Result result = await sender.Send(command);

                return result.Match(
                    () => Results.Created(),
                    ApiResponses.Problem);
            })
            .WithName("AddItemDescription")
            .WithTags(EndpointTags.Item)
            .Produces(StatusCodes.Status201Created)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }

    internal sealed record AddItemDescriptionRequest(
        string Type,
        string Description);
}