using Aurora.Framework;
using FluentAssertions;

namespace Aurora.BizSuite.ArchitectureTests.Items;

public class ItemDomainTests : ItemBaseTests
{
    [Fact]
    public void DomainEvents_ShouldBe_Sealed()
    {
        Types.InAssembly(DomainAssembly)
            .That()
            .ImplementInterface(typeof(IDomainEvent))
            .Or()
            .Inherit(typeof(DomainEvent))
            .Should()
            .BeSealed()
            .GetResult()
            .BeSuccessful();
    }

    [Fact]
    public void DomainEvents_ShouldHave_Suffix()
    {
        Types.InAssembly(DomainAssembly)
            .That()
            .ImplementInterface(typeof(IDomainEvent))
            .Or()
            .Inherit(typeof(DomainEvent))
            .Should()
            .HaveNameEndingWith("DomainEvent")
            .GetResult()
            .BeSuccessful();
    }

    [Fact]
    public void DomainEntities_ShouldHave_PrivateParameterlessConstructor()
    {
        IEnumerable<Type> entityTypes = Types.InAssembly(DomainAssembly)
            .That()
            .Inherit(typeof(IAggregateRoot))
            .GetTypes();

        var invalidTypes = new List<Type>();
        foreach (Type entityType in entityTypes)
        {
            ConstructorInfo[] constructors = entityType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);

            if (!constructors.Any(c => c.IsPrivate && c.GetParameters().Length == 0))
            {
                invalidTypes.Add(entityType);
            }
        }

        invalidTypes.Should().BeEmpty();
    }

    [Fact]
    public void DomainEntities_ShouldOnlyHave_PrivateConstructors()
    {
        IEnumerable<Type> entityTypes = Types.InAssembly(DomainAssembly)
            .That()
            .Inherit(typeof(IAggregateRoot))
            .GetTypes();

        var invalidTypes = new List<Type>();
        foreach (Type entityType in entityTypes)
        {
            ConstructorInfo[] constructors = entityType.GetConstructors(BindingFlags.Public | BindingFlags.Instance);

            if (constructors.Length != 0)
            {
                invalidTypes.Add(entityType);
            }
        }

        invalidTypes.Should().BeEmpty();
    }
}