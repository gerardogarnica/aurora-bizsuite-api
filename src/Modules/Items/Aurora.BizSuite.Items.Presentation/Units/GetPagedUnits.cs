using Aurora.BizSuite.Items.Application.Units;
using Aurora.BizSuite.Items.Application.Units.GetList;

namespace Aurora.BizSuite.Items.Presentation.Units;

internal sealed class GetPagedUnits : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "units",
            async (
                [FromQuery] int page,
                [FromQuery] int size,
                [FromQuery] string? searchTerms,
                ISender sender) =>
            {
                var query = new GetUnitListQuery(
                    new PagedViewRequest(page, size),
                    searchTerms);

                Result<PagedResult<UnitOfMeasurementModel>> result = await sender.Send(query);

                return result.Match(Results.Ok, ApiResponses.Problem);
            })
            .WithName("GetPagedUnits")
            .WithTags(EndpointTags.Unit)
            .Produces<PagedResult<UnitOfMeasurementModel>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }
}