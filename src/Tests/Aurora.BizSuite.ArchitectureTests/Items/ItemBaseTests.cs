namespace Aurora.BizSuite.ArchitectureTests.Items;

public abstract class ItemBaseTests
{
    protected static readonly Assembly ApplicationAssembly = typeof(BizSuite.Items.Application.AssemblyReference).Assembly;
    protected static readonly Assembly DomainAssembly = typeof(BizSuite.Items.Domain.Items.Item).Assembly;
    protected static readonly Assembly InfrastructureAssembly = typeof(BizSuite.Items.Infrastructure.ItemsModuleConfiguration).Assembly;
    protected static readonly Assembly PresentationAssembly = typeof(BizSuite.Items.Presentation.AssemblyReference).Assembly;
}