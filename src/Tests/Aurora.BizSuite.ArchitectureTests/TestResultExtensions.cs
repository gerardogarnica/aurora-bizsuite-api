using FluentAssertions;
using NetArchTest.Rules;

namespace Aurora.BizSuite.ArchitectureTests;

internal static class TestResultExtensions
{
    internal static void BeSuccessful(this TestResult result)
    {
        result.FailingTypes?
            .Should()
            .BeEmpty();
    }
}