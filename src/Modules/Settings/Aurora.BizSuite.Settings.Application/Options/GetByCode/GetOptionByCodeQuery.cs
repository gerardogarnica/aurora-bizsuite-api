namespace Aurora.BizSuite.Settings.Application.Options.GetByCode;

public sealed record GetOptionByCodeQuery : IRequest<OptionModel?>
{
    public string Code { get; init; } = string.Empty;
}