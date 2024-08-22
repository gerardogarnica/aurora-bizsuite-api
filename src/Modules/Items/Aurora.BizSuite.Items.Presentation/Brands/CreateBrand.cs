using Aurora.BizSuite.Items.Application.Brands.Create;

namespace Aurora.BizSuite.Items.Presentation.Brands;

internal sealed class CreateBrand : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
            "brands",
            async ([FromBody] CreateBrandRequest request, ISender sender) =>
            {
                var command = new CreateBrandCommand(
                    request.Name.Trim(),
                    request.LogoUri?.Trim(),
                    request.Notes?.Trim());

                Result<Guid> result = await sender.Send(command);

                return result.Match(
                    () => Results.Created(string.Empty, result.Value),
                    ApiResponses.Problem);
            })
            .WithName("CreateBrand")
            .WithTags(EndpointTags.Brand)
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }

    internal sealed record CreateBrandRequest(
        string Name,
        string? LogoUri,
        string? Notes);
}