namespace Aurora.BizSuite.Items.Application.Items.UpdateStatus;

public sealed record UpdateItemStatusCommand(Guid ItemId, bool Enable) : ICommand;