namespace Aurora.BizSuite.Items.Application.Items.Create;

internal sealed class CreateItemCommandValidator : AbstractValidator<CreateItemCommand>
{
    private readonly IItemRepository _itemRepository;

    public CreateItemCommandValidator(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;

        RuleFor(x => x.Code)
            .NotEmpty().WithBaseError(ItemErrors.CodeIsRequired)
            .MinimumLength(3).WithBaseError(ItemErrors.CodeIsTooShort)
            .MaximumLength(40).WithBaseError(ItemErrors.CodeIsTooLong)
            .MustAsync(BeUniqueCode).WithBaseError(ItemErrors.CodeIsNotUnique);

        RuleFor(x => x.Name)
            .NotEmpty().WithBaseError(ItemErrors.NameIsRequired)
            .MinimumLength(3).WithBaseError(ItemErrors.NameIsTooShort)
            .MaximumLength(100).WithBaseError(ItemErrors.NameIsTooLong);

        RuleFor(x => x.Description)
            .NotEmpty().WithBaseError(ItemErrors.DescriptionIsRequired)
            .MinimumLength(3).WithBaseError(ItemErrors.DescriptionIsTooShort)
            .MaximumLength(1000).WithBaseError(ItemErrors.DescriptionIsTooLong);

        RuleFor(x => x.AlternativeCode)
            .MaximumLength(40).WithBaseError(ItemErrors.AlternativeCodeIsTooLong);

        RuleFor(x => x.Notes)
            .MaximumLength(1000).WithBaseError(ItemErrors.NotesIsTooLong);

        RuleFor(x => x.Tags)
            .Must(x => x.Count <= 100).WithBaseError(ItemErrors.TagsLimitExceeded);

        RuleForEach(x => x.Tags)
            .MaximumLength(40).WithBaseError(ItemErrors.TagsIsTooLong);
    }

    private async Task<bool> BeUniqueCode(string code, CancellationToken cancellationToken = default!)
    {
        return await _itemRepository.GetByCodeAsync(code) is null;
    }
}