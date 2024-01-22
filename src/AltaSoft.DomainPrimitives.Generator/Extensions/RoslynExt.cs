using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

// https://www.meziantou.net/working-with-types-in-a-roslyn-analyzer.htm

namespace AltaSoft.DomainPrimitives.Generator.Extensions;

/// <summary>
/// A collection of extension methods for working with Roslyn syntax and symbols.
/// </summary>
internal static class RoslynExtensions
{
    /// <summary>
    /// Gets the location of the attribute data within the source code.
    /// </summary>
    /// <param name="self">The attribute data to retrieve the location for.</param>
    /// <returns>The location of the attribute data in the source code, or null if not found.</returns>
    public static Location? GetAttributeLocation(this AttributeData self)
    {
        var syntaxReference = self.ApplicationSyntaxReference;

        var syntax = (AttributeSyntax?)syntaxReference?.GetSyntax();

        return syntax?.GetLocation();
    }

    /// <summary>
    /// Checks if the symbol has a default constructor (parameterless constructor) defined and retrieves its location.
    /// </summary>
    /// <param name="self">The symbol to check for a default constructor.</param>
    /// <param name="location">When this method returns, contains the location of the default constructor, if found; otherwise, null.</param>
    /// <returns>True if a default constructor is found; otherwise, false.</returns>
    public static bool HasDefaultConstructor(this ISymbol? self, out Location? location)
    {
        var constructors = self.GetConstructorsFromSyntaxTree();

        var ctor = constructors?.FirstOrDefault(x => x.ParameterList.Parameters.Count == 0);
        location = ctor?.GetLocation();
        return ctor is not null;
    }

    /// <summary>
    /// Retrieves a list of constructor declarations associated with the symbol from the syntax tree.
    /// </summary>
    /// <param name="self">The symbol for which to retrieve constructor declarations.</param>
    /// <returns>A list of constructor declarations or null if none are found.</returns>
    public static List<ConstructorDeclarationSyntax>? GetConstructorsFromSyntaxTree(this ISymbol? self)
    {
        var declaringSyntaxReferences = self?.DeclaringSyntaxReferences;

        if (self is null || declaringSyntaxReferences is null or { Length: 0 })
            return null;

        List<ConstructorDeclarationSyntax>? result = null;

        foreach (var syntax in declaringSyntaxReferences)
        {
            if (syntax.GetSyntax() is TypeDeclarationSyntax classDeclaration && classDeclaration.GetClassFullName() == self.ToString())
            {
                var constructors = classDeclaration.Members.OfType<ConstructorDeclarationSyntax>();

                (result ??= []).AddRange(constructors);
            }
        }
        return result;
    }

    /// <summary>
    /// Gets the namespace of the specified type declaration syntax.
    /// </summary>
    /// <param name="self">The type declaration syntax to retrieve the namespace from.</param>
    /// <returns>The namespace of the type declaration or null if not found.</returns>
    public static string? GetNamespace(this TypeDeclarationSyntax self)
    {
        return self.Parent is not BaseNamespaceDeclarationSyntax ns ? null : ns.GetNamespace();
    }

    /// <summary>
    /// Gets the fully qualified name of the specified type declaration syntax, including its namespace.
    /// </summary>
    /// <param name="self">The type declaration syntax to retrieve the fully qualified name from.</param>
    /// <returns>The fully qualified name of the type declaration.</returns>
    public static string GetClassFullName(this TypeDeclarationSyntax self)
    {
        return self.GetNamespace() + "." + self.GetClassName();
    }

    /// <summary>
    /// Gets the namespace from the specified base namespace declaration syntax.
    /// </summary>
    /// <param name="self">The base namespace declaration syntax to retrieve the namespace from.</param>
    /// <returns>The namespace from the base namespace declaration.</returns>
    public static string GetNamespace(this BaseNamespaceDeclarationSyntax self) => self.Name.ToString();

    /// <summary>
    /// Gets the name of the class specified in the type declaration syntax.
    /// </summary>
    /// <param name="proxy">The type declaration syntax to retrieve the class name from.</param>
    /// <returns>The name of the class.</returns>
    public static string GetClassName(this TypeDeclarationSyntax proxy)
    {
        return proxy.Identifier.Text + proxy.TypeParameterList;
    }
}