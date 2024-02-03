namespace Aurora.BizSuite.Settings.Application.Options.GetList;

public sealed record GetOptionListQuery : IRequest<PagedResult<OptionModel>>
{
    public PagedViewRequest Paged { get; init; } = new PagedViewRequest(0, 0);
    public string? SearchCriteria { get; init; }
}