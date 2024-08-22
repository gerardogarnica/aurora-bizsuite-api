using Aurora.BizSuite.Items.Application.Brands.Update;

namespace Aurora.BizSuite.Items.Presentation.Brands;

internal sealed class UpdateBrand : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(
            "brands/{id}",
            async (Guid id, [FromBody] UpdateBrandRequest request, ISender sender) =>
            {
                var command = new UpdateBrandCommand(
                    id,
                    request.Name.Trim(),
                    request.LogoUri?.Trim(),
                    request.Notes?.Trim());

                Result result = await sender.Send(command);

                return result.Match(
                    () => Results.Accepted(string.Empty),
                    ApiResponses.Problem);
            })
            .WithName("UpdateBrand")
            .WithTags(EndpointTags.Brand)
            .Produces<Guid>(StatusCodes.Status202Accepted)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }

    internal sealed record UpdateBrandRequest(
        string Name,
        string? LogoUri,
        string? Notes);
}