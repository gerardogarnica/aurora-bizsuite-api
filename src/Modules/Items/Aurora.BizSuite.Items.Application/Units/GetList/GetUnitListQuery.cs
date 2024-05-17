namespace Aurora.BizSuite.Items.Application.Units.GetList;

public sealed record GetUnitListQuery(
    PagedViewRequest PagedView,
    string? SearchTerms)
    : IQuery<PagedResult<UnitOfMeasurementModel>>;