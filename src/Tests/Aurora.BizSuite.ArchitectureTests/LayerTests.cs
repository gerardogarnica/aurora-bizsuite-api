namespace Aurora.BizSuite.ArchitectureTests;

public class LayerTests : BaseLayerTests
{
    [Fact]
    public void CompaniesModule_ShouldNotHaveDependencyOn_AnyOtherModule()
    {
        string[] otherModules = [InventoryNamespace, ItemsNamespace];

        List<Assembly> companiesAssemblies =
        [
            typeof(Companies.Domain.Companies.Company).Assembly,
            Companies.Application.AssemblyReference.Assembly,
            Companies.Presentation.AssemblyReference.Assembly,
            typeof(Companies.Infrastructure.CompaniesModuleConfiguration).Assembly
        ];

        Types.InAssemblies(companiesAssemblies)
            .Should()
            .NotHaveDependencyOnAny(otherModules)
            .GetResult()
            .BeSuccessful();
    }

    [Fact]
    public void ItemsModule_ShouldNotHaveDependencyOn_AnyOtherModule()
    {
        string[] otherModules = [CompaniesNamespace, InventoryNamespace];

        List<Assembly> itemsAssemblies =
        [
            typeof(BizSuite.Items.Domain.Items.Item).Assembly,
            BizSuite.Items.Application.AssemblyReference.Assembly,
            BizSuite.Items.Presentation.AssemblyReference.Assembly,
            typeof(BizSuite.Items.Infrastructure.ItemsModuleConfiguration).Assembly
        ];

        Types.InAssemblies(itemsAssemblies)
            .Should()
            .NotHaveDependencyOnAny(otherModules)
            .GetResult()
            .BeSuccessful();
    }
}