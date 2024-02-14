namespace Aurora.BizSuite.Settings.Application.Options.Create;

public class CreateOptionCommandHandler : IRequestHandler<CreateOptionCommand, OptionModel>
{
    private readonly IOptionRepository _optionRepository;

    public CreateOptionCommandHandler(IOptionRepository optionRepository)
    {
        _optionRepository = optionRepository ?? throw new ArgumentNullException(nameof(optionRepository));
    }

    public async Task<OptionModel> Handle(CreateOptionCommand request, CancellationToken cancellationToken)
    {
        var option = CreateOption(request);

        option = await _optionRepository.InsertAsync(option);

        return option.ToModel();
    }

    private static Option CreateOption(CreateOptionCommand request)
    {
        var option = Option.Create(
            request.Code,
            request.Name,
            request.Description,
            request.Type);

        request.Items.ForEach(x =>
        {
            option.AddItem(
                x.Code,
                x.Description);
        });

        return option;
    }
}