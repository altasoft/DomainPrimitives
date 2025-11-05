using System;
using System.Linq;
using System.Threading;
using AltaSoft.DomainPrimitives.Generator.Extensions;
using AltaSoft.DomainPrimitives.Generator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

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
        //        System.Diagnostics.Debugger.Launch();
        //#endif

        var domainPrimitivesToGenerate = context.SyntaxProvider.CreateSyntaxProvider(
                predicate: static (node, _) => IsSyntaxTargetForGeneration(node),
                transform: static (ctx, cancellationToken) => GetSemanticTargetForGeneration(ctx, cancellationToken))
            .Where(static x => x is not null);

        var assemblyNames = context.CompilationProvider.Select((c, _) => c.AssemblyName ?? throw new InvalidOperationException("Assembly name must be provided"));

        var globalOptions = context.AnalyzerConfigOptionsProvider.Select((c, _) => GetGlobalOptions(c));

        var allData = domainPrimitivesToGenerate.Collect().Combine(assemblyNames).Combine(globalOptions);

        context.RegisterSourceOutput(allData, static (spc, pair) => Executor.Execute(in pair.Left.Left, in pair.Left.Right, in pair.Right, in spc));
    }

    /// <summary>
    /// Determines if a given syntax node represents a semantic target for code generation.
    /// </summary>
    /// <param name="context">The generator syntax context.</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>
    /// The <see cref="TypeDeclarationSyntax"/> if the syntax node is a semantic target; otherwise, <c>null</c>.
    /// </returns>
    /// <remarks>
    /// This method analyzes a <see cref="TypeDeclarationSyntax"/> node to determine if it represents a semantic target
    /// for code generation. A semantic target is typically a class, struct, or record declaration that is not abstract
    /// and implements one or more interfaces marked as domain value types.
    /// </remarks>
    /// <seealso cref="DomainPrimitiveGenerator"/>
    private static INamedTypeSymbol? GetSemanticTargetForGeneration(GeneratorSyntaxContext context, CancellationToken cancellationToken)
    {
        var typeSyntax = (TypeDeclarationSyntax)context.Node;

        var symbol = context.SemanticModel.GetDeclaredSymbol(typeSyntax, cancellationToken);

        if (symbol is not INamedTypeSymbol typeSymbol)
            return null;

        return !typeSymbol.IsAbstract && typeSymbol.AllInterfaces.Any(x => x.IsDomainValue()) ? typeSymbol : null;
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
    private static bool IsSyntaxTargetForGeneration(SyntaxNode syntaxNode)
    {
        return syntaxNode is
            ClassDeclarationSyntax { BaseList: not null } or
            StructDeclarationSyntax { BaseList: not null } or
            RecordDeclarationSyntax { BaseList: not null };
    }

    /// <summary>
    /// Gets the global options for the AltaSoft.DomainPrimitiveGenerator generator.
    /// </summary>
    /// <param name="analyzerOptions">The AnalyzerConfigOptionsProvider to access analyzer options.</param>
    /// <returns>The DomainPrimitiveGlobalOptions for the generator.</returns>
    private static DomainPrimitiveGlobalOptions GetGlobalOptions(AnalyzerConfigOptionsProvider analyzerOptions)
    {
        var result = new DomainPrimitiveGlobalOptions();

        if (analyzerOptions.GlobalOptions.TryGetValue("build_property.DomainPrimitiveGenerator_GenerateJsonConverters", out var value)
            && bool.TryParse(value, out var generateJsonConverters))
        {
            result.GenerateJsonConverters = generateJsonConverters;
        }

        if (analyzerOptions.GlobalOptions.TryGetValue("build_property.DomainPrimitiveGenerator_GenerateImplicitConversions", out var implicitConversions)
            && bool.TryParse(implicitConversions, out var generateImplicitConversions))
        {
            result.GenerateImplicitConversions = generateImplicitConversions;
        }

        if (analyzerOptions.GlobalOptions.TryGetValue("build_property.DomainPrimitiveGenerator_DefaultNumericOperationsEnabled", out var defaultNumericOps)
            && bool.TryParse(defaultNumericOps, out var defaultNumericEnabled))
        {
            result.DefaultNumericOperationsEnabled = defaultNumericEnabled;
        }

        if (analyzerOptions.GlobalOptions.TryGetValue("build_property.DomainPrimitiveGenerator_GenerateTypeConverters", out value)
            && bool.TryParse(value, out var generateTypeConverter))
        {
            result.GenerateTypeConverters = generateTypeConverter;
        }

        if (analyzerOptions.GlobalOptions.TryGetValue("build_property.DomainPrimitiveGenerator_SafeDefaultStructSemantics", out value)
            && bool.TryParse(value, out var safeDefaults))
        {
            result.SafeDefaultStructSemantics = safeDefaults;
        }

        if (analyzerOptions.GlobalOptions.TryGetValue("build_property.DomainPrimitiveGenerator_GenerateSwaggerConverters", out value)
            && bool.TryParse(value, out var generateSwaggerConverters))
        {
            result.GenerateSwaggerConverters = generateSwaggerConverters;
        }

        if (analyzerOptions.GlobalOptions.TryGetValue("build_property.DomainPrimitiveGenerator_GenerateXmlSerialization", out value)
            && bool.TryParse(value, out var generateXmlSerialization))
        {
            result.GenerateXmlSerialization = generateXmlSerialization;
        }

        if (analyzerOptions.GlobalOptions.TryGetValue("build_property.DomainPrimitiveGenerator_GenerateEntityFrameworkCoreValueConverters", out value)
            && bool.TryParse(value, out var generateEntityFrameworkValueConverters))
        {
            result.GenerateEntityFrameworkCoreValueConverters = generateEntityFrameworkValueConverters;
        }

        return result;
    }
}
