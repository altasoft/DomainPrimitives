using System.Runtime.CompilerServices;

namespace AltaSoft.DomainPrimitives.Generator.Tests;

public static class ModuleInitializer
{
	[ModuleInitializer]
	public static void Init()
	{
		VerifySourceGenerators.Initialize();
	}
}