namespace Aurora.BizSuite.Items.Application.Units.GetList;

internal sealed class GetUnitListQueryHandler(
    IUnitRepository unitRepository)
    : IQueryHandler<GetUnitListQuery, PagedResult<UnitOfMeasurementModel>>
{
    public async Task<Result<PagedResult<UnitOfMeasurementModel>>> Handle(
        GetUnitListQuery request,
        CancellationToken cancellationToken)
    {
        // Get paged units
        var pagedUnits = await unitRepository.GetPagedAsync(
            request.PagedView,
            request.SearchTerms);

        // Return paged result
        return Result.Ok(new PagedResult<UnitOfMeasurementModel>(
            pagedUnits.Items.Select(x => x.ToUnitModel()).ToList(),
            pagedUnits.TotalItems,
            pagedUnits.CurrentPage,
            pagedUnits.TotalPages));
    }
}