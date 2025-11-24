using System.Runtime.CompilerServices;
using DiffEngine;

namespace AltaSoft.DomainPrimitives.Generator.Tests;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Init()
    {
        VerifySourceGenerators.Initialize();
        DiffTools.UseOrder(DiffTool.VisualStudioCode, DiffTool.VisualStudio);

    }
}
