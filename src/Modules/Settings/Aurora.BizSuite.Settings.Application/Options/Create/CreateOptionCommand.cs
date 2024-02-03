namespace Aurora.BizSuite.Settings.Application.Options.Create;

public sealed record CreateOptionCommand : IRequest<OptionModel>
{
    public required string Code { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public OptionType Type { get; init; }
    public List<CreateOptionItem> Items { get; init; } = [];
}

public sealed record CreateOptionItem
{
    public required string Code { get; init; }
    public string? Description { get; init; }
}