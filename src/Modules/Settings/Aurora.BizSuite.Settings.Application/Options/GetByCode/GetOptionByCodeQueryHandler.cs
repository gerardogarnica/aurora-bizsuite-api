namespace Aurora.BizSuite.Settings.Application.Options.GetByCode;

public class GetOptionByCodeQueryHandler : IRequestHandler<GetOptionByCodeQuery, OptionModel?>
{
    private readonly IOptionRepository _optionRepository;

    public GetOptionByCodeQueryHandler(IOptionRepository optionRepository)
    {
        _optionRepository = optionRepository;
    }

    public async Task<OptionModel?> Handle(GetOptionByCodeQuery request, CancellationToken cancellationToken)
    {
        // Get option
        var option = await _optionRepository.GetOptionAsync(request.Code);
        if (option == null) return null;

        // Return option model with items
        return new OptionModel(
            option.Id.Value,
            option.Code,
            option.Name,
            option.Description,
            option.Equals(OptionType.User),
            option.Items.Select(x => new OptionItemModel(
                x.Id.Value,
                x.Code,
                x.Description,
                x.IsActive)).ToList());
    }
}