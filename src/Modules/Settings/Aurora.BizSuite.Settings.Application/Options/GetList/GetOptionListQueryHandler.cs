namespace Aurora.BizSuite.Settings.Application.Options.GetList;

public class GetOptionListQueryHandler : IRequestHandler<GetOptionListQuery, PagedResult<OptionModel>>
{
    private readonly IOptionRepository _optionRepository;

    public GetOptionListQueryHandler(IOptionRepository optionRepository)
    {
        _optionRepository = optionRepository;
    }

    public async Task<PagedResult<OptionModel>> Handle(GetOptionListQuery request, CancellationToken cancellationToken)
    {
        // Get paged options
        var options = await _optionRepository.GetPagedOptionAsync(request.Paged, request.SearchCriteria);

        // Return paged result
        return new PagedResult<OptionModel>(
            options.Items.Select(x => x.ToModel()).ToList(),
            options.TotalItems,
            options.CurrentPage,
            options.TotalPages);
    }
}