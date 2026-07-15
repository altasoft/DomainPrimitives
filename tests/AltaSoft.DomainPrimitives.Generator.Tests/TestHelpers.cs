using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using AltaSoft.DomainPrimitives.Generator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;

// ReSharper disable ConvertToPrimaryConstructor

#pragma warning disable IDE0290

namespace AltaSoft.DomainPrimitives.Generator.Tests;

internal static class TestHelpers
{
    internal static (ImmutableArray<Diagnostic> Diagnostics, List<string> Output, GeneratorDriver driver) GetGeneratedOutput<T>
        (string source, IEnumerable<Assembly> assembliesToImport, DomainPrimitiveGlobalOptions? globalOptions = null)
        where T : IIncrementalGenerator, new()
    {
        var parseOptions = new CSharpParseOptions(
            LanguageVersion.Latest,
            preprocessorSymbols:
            [
                "NET10_0_OR_GREATER"
            ]);

        var syntaxTree = CSharpSyntaxTree.ParseText(source, parseOptions);

        // Use the full set of assemblies the runtime resolved for this process (via its deps.json),
        // rather than AppDomain.CurrentDomain.GetAssemblies(), which only reflects whichever assemblies
        // happen to already be JIT-loaded. The latter is non-deterministic across environments and can
        // silently omit assemblies like System.ObjectModel (home of TypeConverterAttribute), causing
        // sporadic compile errors depending on process/load-order, not on anything about the source under test.
        var trustedAssemblyPaths = ((string?)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES"))
            ?.Split(Path.PathSeparator) ?? [];

        var referencePaths = trustedAssemblyPaths
            .Concat([
                typeof(T).Assembly.Location,
                typeof(IDomainValue<>).Assembly.Location,
                typeof(System.ComponentModel.DataAnnotations.DisplayAttribute).Assembly.Location
            ])
            .Concat(assembliesToImport.Select(a => a.Location))
            .Where(p => !string.IsNullOrWhiteSpace(p))
            .Distinct(StringComparer.OrdinalIgnoreCase);

        var references = referencePaths.Select(p => (MetadataReference)MetadataReference.CreateFromFile(p));

        var compilation = CSharpCompilation.Create(
            "generator_Test",
            [syntaxTree],
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        var originalTreeCount = compilation.SyntaxTrees.Length;
        var generator = new T();
        var driver = CSharpGeneratorDriver
            .Create(generator)
            .WithUpdatedParseOptions(parseOptions)
            .WithUpdatedAnalyzerConfigOptions(new DomainPrimitiveConfigOptionsProvider(globalOptions ?? new DomainPrimitiveGlobalOptions()));

        var newDriver = driver.RunGeneratorsAndUpdateCompilation(compilation, out var outputCompilation, out var syntaxDiagnostics);
        var trees = outputCompilation.SyntaxTrees.ToList();

        var compilationDiagnostics = outputCompilation.GetDiagnostics()
            .Where(d => d.Severity == DiagnosticSeverity.Error) // Only capture errors
            .ToImmutableArray();

        var diagnostics = ImmutableArray.Create(syntaxDiagnostics.Concat(compilationDiagnostics).ToArray());

        return (diagnostics, trees.Count != originalTreeCount ? trees[1..].ConvertAll(x => x.ToString()) : [], newDriver);
    }

    private sealed class DomainPrimitiveConfigOptionsProvider : AnalyzerConfigOptionsProvider
    {
        public DomainPrimitiveConfigOptionsProvider(DomainPrimitiveGlobalOptions options)
        {
            GlobalOptions = new DomainPrimitivesOptions(options);
        }

        public override AnalyzerConfigOptions GetOptions(SyntaxTree tree)
        {
            throw new NotImplementedException(); //source generators do not need this
        }

        public override AnalyzerConfigOptions GetOptions(AdditionalText textFile)
        {
            throw new NotImplementedException(); //source generators do not need this
        }

        public override AnalyzerConfigOptions GlobalOptions { get; }

        private sealed class DomainPrimitivesOptions : AnalyzerConfigOptions
        {
            private readonly DomainPrimitiveGlobalOptions _options;

            public DomainPrimitivesOptions(DomainPrimitiveGlobalOptions options)
            {
                _options = options;
            }

            public override bool TryGetValue(string key, [NotNullWhen(true)] out string? value)
            {
                switch (key)
                {
                    case "build_property.DomainPrimitiveGenerator_GenerateJsonConverters":
                        value = _options.GenerateJsonConverters.ToString();
                        return true;

                    case "build_property.DomainPrimitiveGenerator_GenerateXmlSerialization":
                        value = _options.GenerateXmlSerialization.ToString();
                        return true;

                    case "build_property.DomainPrimitiveGenerator_GenerateOpenApiHelper":
                        value = _options.GenerateOpenApiHelper.ToString();
                        return true;

                    case "build_property.DomainPrimitiveGenerator_GenerateTypeConverters":
                        value = _options.GenerateTypeConverters.ToString();
                        return true;

                    case "build_property.DomainPrimitiveGenerator_GenerateEntityFrameworkCoreValueConverters":
                        value = _options.GenerateEntityFrameworkCoreValueConverters.ToString();
                        return true;

                    case "build_property.DomainPrimitiveGenerator_GenerateImplicitConversions":
                        value = _options.GenerateImplicitOperators.ToString();
                        return true;

                    case "build_property.DomainPrimitiveGenerator_GenerateNumericOperations":
                        value = _options.GenerateNumericOperators.ToString();
                        return true;
                    default:
                        value = null;
                        return false;
                }
            }
        }
    }
}
