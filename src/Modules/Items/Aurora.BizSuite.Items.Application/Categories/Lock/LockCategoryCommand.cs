namespace Aurora.BizSuite.Items.Application.Categories.Lock;

public sealed record LockCategoryCommand(Guid CategoryId) : ICommand;