using System.Reflection;

namespace Aurora.BizSuite.Companies.Application;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}