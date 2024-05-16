using System.Reflection;

namespace Aurora.BizSuite.Items.Presentation;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}