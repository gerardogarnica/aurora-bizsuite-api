using NetArchTest.Rules;
using System.Reflection;

namespace Aurora.BizSuite.ArchitectureTests;

public class LayerTests : BaseLayerTests
{
    [Fact]
    public void ItemsModule_ShouldNotHaveDependencyOn_AnyOtherModule()
    {
        string[] otherModules = [];

        List<Assembly> itemsAssemblies =
        [
            typeof(Items.Domain.Items.Item).Assembly,
            Items.Application.AssemblyReference.Assembly,
            Items.Presentation.AssemblyReference.Assembly,
            typeof(Items.Infrastructure.DependencyInjection).Assembly
        ];

        Types.InAssemblies(itemsAssemblies)
            .Should()
            .NotHaveDependencyOnAny(otherModules)
            .GetResult()
            .BeSuccessful();
    }
}