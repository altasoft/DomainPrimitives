using AltaSoft.DomainPrimitives.Abstractions;
using AltaSoft.DomainPrimitives.Generator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace AltaSoft.DomainPrimitives.Generator.Tests;

internal static class TestHelpers
{
	internal static (ImmutableArray<Diagnostic> Diagnostics, List<string> Output, GeneratorDriver driver) GetGeneratedOutput<T>
		(string source, DomainPrimitiveGlobalOptions? globalOptions = null)
		where T : IIncrementalGenerator, new()
	{
		var syntaxTree = CSharpSyntaxTree.ParseText(source);
		var references = AppDomain.CurrentDomain.GetAssemblies()
			.Where(_ => !_.IsDynamic && !string.IsNullOrWhiteSpace(_.Location))
			.Select(_ => MetadataReference.CreateFromFile(_.Location))
			.Concat(new[]
			{
				MetadataReference.CreateFromFile(typeof(T).Assembly.Location),
				MetadataReference.CreateFromFile(typeof(IDomainValue<>).Assembly.Location),
				MetadataReference.CreateFromFile(typeof(System.ComponentModel.DataAnnotations.DisplayAttribute).Assembly.Location),
			});

		var compilation = CSharpCompilation.Create(
			"generator_Test",
			new[] { syntaxTree },
			references,
			new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

		var originalTreeCount = compilation.SyntaxTrees.Length;
		var generator = new T();
		var driver = CSharpGeneratorDriver.Create(generator)
			.WithUpdatedAnalyzerConfigOptions(new DomainPrimitiveConfigOptionsProvider(globalOptions ?? new DomainPrimitiveGlobalOptions()));

		var newDriver = driver.RunGeneratorsAndUpdateCompilation(compilation, out var outputCompilation, out var diagnostics);
		var trees = outputCompilation.SyntaxTrees.ToList();

		return (diagnostics, trees.Count != originalTreeCount ? trees[1..].ConvertAll(x => x.ToString()) : [], newDriver);
	}

	private class DomainPrimitiveConfigOptionsProvider : AnalyzerConfigOptionsProvider
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

					case "build_property.DomainPrimitiveGenerator_GenerateSwaggerConverters":
						value = _options.GenerateSwaggerConverters.ToString();
						return true;

					case "build_property.DomainPrimitiveGenerator_GenerateTypeConverters":
						value = _options.GenerateTypeConverters.ToString();
						return true;

					default:
						value = null;
						return false;
				}
			}
		}
	}
}