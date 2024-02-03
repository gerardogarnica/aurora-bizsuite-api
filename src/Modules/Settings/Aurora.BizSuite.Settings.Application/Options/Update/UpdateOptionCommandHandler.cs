namespace Aurora.BizSuite.Settings.Application.Options.Update;

public class UpdateOptionCommandHandler : IRequestHandler<UpdateOptionCommand, Result>
{
    private readonly IOptionRepository _optionRepository;

    public UpdateOptionCommandHandler(IOptionRepository optionRepository)
    {
        _optionRepository = optionRepository;
    }

    public async Task<Result> Handle(UpdateOptionCommand request, CancellationToken cancellationToken)
    {
        var option = await _optionRepository.GetOptionAsync(request.Code);

        if (option == null)
            return Result.Fail(DomainErrors.OptionErrors.OptionNotFound);

        option.Update(request.Name, request.Description);

        _optionRepository.Update(option);

        return Result.Ok();
    }
}