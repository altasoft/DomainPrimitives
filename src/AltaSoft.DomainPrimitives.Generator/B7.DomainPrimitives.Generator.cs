//#if DEBUG

//using System.Diagnostics;

//#endif

using System.Linq;
using AltaSoft.DomainPrimitives.Generator.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AltaSoft.DomainPrimitives.Generator;

/// <summary>
/// A custom source code generator responsible for generating code for domain primitive types based on their declarations in the source code.
/// </summary>
[Generator]
public sealed class DomainPrimitiveGenerator : IIncrementalGenerator
{
	/// <summary>
	/// Initializes the DomainPrimitiveGenerator and registers it as a source code generator.
	/// </summary>
	/// <param name="context">The generator initialization context.</param>
	public void Initialize(IncrementalGeneratorInitializationContext context)
	{
		//#if DEBUG
		//		Debugger.Launch();
		//#endif

		var classes = context.SyntaxProvider.CreateSyntaxProvider(
				 static (s, _) => IsSyntaxTargetForGeneration(s),
				 static (s, _) => GetSemanticTargetForGeneration(s))
						 .Where(x => x is not null);

		var compilationAndClasses = context.CompilationProvider.Combine(classes.Collect());

		var analyzerOptionsAndClasses = context.AnalyzerConfigOptionsProvider.Combine(compilationAndClasses);

		context.RegisterSourceOutput(analyzerOptionsAndClasses, (compilation, c) =>
			Executor.Execute(c.Right.Left, c.Right.Right!, c.Left, compilation));
	}

	/// <summary>
	/// Determines if a given syntax node represents a semantic target for code generation.
	/// </summary>
	/// <param name="context">The generator syntax context.</param>
	/// <returns>
	/// The <see cref="TypeDeclarationSyntax"/> if the syntax node is a semantic target; otherwise, <c>null</c>.
	/// </returns>
	/// <remarks>
	/// This method analyzes a <see cref="TypeDeclarationSyntax"/> node to determine if it represents a semantic target
	/// for code generation. A semantic target is typically a class, struct, or record declaration that is not abstract
	/// and implements one or more interfaces marked as domain value types.
	/// </remarks>
	/// <seealso cref="DomainPrimitiveGenerator"/>
	private static TypeDeclarationSyntax? GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
	{
		var typeDeclaration = (TypeDeclarationSyntax)context.Node;

		var symbol = context.SemanticModel.GetDeclaredSymbol(typeDeclaration);

		if (symbol is not INamedTypeSymbol c)
			return null;

		if (!c.IsAbstract &&
			c.AllInterfaces.Any(x => x.IsDomainValue()))
		{
			return typeDeclaration;
		}

		return null;
	}

	/// <summary>
	/// Determines if a given syntax node is a valid target for code generation.
	/// </summary>
	/// <param name="syntaxNode">The syntax node to be evaluated.</param>
	/// <returns>
	/// <c>true</c> if the syntax node is a valid target for code generation; otherwise, <c>false</c>.
	/// </returns>
	/// <remarks>
	/// This method checks if the provided syntax node represents a class, struct, or record declaration that has a
	/// non-null base list. Such declarations are considered valid targets for code generation as they can be extended
	/// with additional members.
	/// </remarks>
	/// <seealso cref="DomainPrimitiveGenerator"/>
	private static bool IsSyntaxTargetForGeneration(SyntaxNode syntaxNode) => syntaxNode is
		ClassDeclarationSyntax { BaseList: not null } or
		StructDeclarationSyntax { BaseList: not null } or
		RecordDeclarationSyntax { BaseList: not null };
}