namespace Aurora.BizSuite.Settings.Application.Options.Update;

public sealed record UpdateOptionCommand : IRequest<Result>
{
    public required string Code { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
}