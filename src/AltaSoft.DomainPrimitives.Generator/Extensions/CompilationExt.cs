using AltaSoft.DomainPrimitives.Generator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace AltaSoft.DomainPrimitives.Generator.Extensions;

/// <summary>
/// Extension methods for working with Roslyn's Compilation and related types.
/// </summary>
internal static class CompilationExt
{
    /// <summary>
    /// Checks if the given named type symbol implements the IDomainValue interface.
    /// </summary>
    /// <param name="x">The named type symbol to check.</param>
    /// <returns>True if the type implements the IDomainValue interface; otherwise, false.</returns>
    public static bool IsDomainValue(this INamedTypeSymbol x)
    {
        return x is { IsGenericType: true, Name: "IDomainValue" } && x.ContainingNamespace.ToDisplayString() == "AltaSoft.DomainPrimitives";
    }

    /// <summary>
    /// Gets members of a specific type for a given ITypeSymbol.
    /// </summary>
    /// <typeparam name="TMember">The type of members to retrieve.</typeparam>
    /// <param name="self">The ITypeSymbol to retrieve members from.</param>
    /// <returns>An IEnumerable of members of the specified type.</returns>
    public static IEnumerable<TMember> GetMembersOfType<TMember>(this ITypeSymbol? self) where TMember : ISymbol
    {
        return self?.GetMembers().OfType<TMember>() ?? Enumerable.Empty<TMember>();
    }

    #region Accessibility

    /// <summary>
    /// Checks if the symbol has public accessibility.
    /// </summary>
    /// <param name="symbol">The symbol to check.</param>
    /// <returns>True if the symbol has public accessibility; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPublic(this ISymbol symbol) => symbol.DeclaredAccessibility == Accessibility.Public;

    #endregion Accessibility

    /// <summary>
    /// Gets the modifiers for the named type symbol.
    /// </summary>
    /// <param name="self">The named type symbol to retrieve modifiers from.</param>
    /// <returns>The modifiers as a string, or null if the type is null or has no modifiers.</returns>
    public static string? GetModifiers(this INamedTypeSymbol? self)
    {
        var declaringSyntax = self?.DeclaringSyntaxReferences;
        if (self is null || declaringSyntax is null or { Length: 0 })
            return null;

        foreach (var syntax in declaringSyntax)
        {
            if (syntax.GetSyntax() is TypeDeclarationSyntax typeDeclaration && typeDeclaration.GetClassName() == self.GetClassNameWithArguments())
            {
                var modifiers = typeDeclaration.Modifiers.ToString();
                if (typeDeclaration is RecordDeclarationSyntax)
                    modifiers += " record";

                return modifiers;
            }
        }

        return null;
    }

    /// <summary>
    /// Gets the class name including generic arguments as a string.
    /// </summary>
    /// <param name="type">The named type symbol to get the class name from.</param>
    /// <returns>The class name including generic arguments as a string.</returns>
    public static string GetClassNameWithArguments(this INamedTypeSymbol? type)
    {
        if (type is null)
            return string.Empty;

        var builder = new StringBuilder(type.Name);

        if (type.TypeArguments.Length == 0)
            return builder.ToString();

        builder.Append('<');
        for (var index = 0; index < type.TypeArguments.Length; index++)
        {
            var arg = type.TypeArguments[index];
            builder.Append(arg.Name);

            if (index != type.TypeArguments.Length - 1)
                builder.Append(", ");
        }

        builder.Append('>');

        return builder.ToString();
    }

    /// <summary>
    /// Checks if the specified named type symbol implements the specified interface by its full name.
    /// </summary>
    /// <param name="type">The named type symbol to check for interface implementation.</param>
    /// <param name="interfaceFullName">The full name of the interface to check for implementation.</param>
    /// <returns>True if the type implements the interface; otherwise, false.</returns>
    public static bool ImplementsInterface(this INamedTypeSymbol type, string interfaceFullName)
    {
        return type.Interfaces.Any(x => x.ContainingNamespace.ToDisplayString() + "." + x.Name == interfaceFullName);
    }

    /// <summary>
    /// Gets the underlying primitive type information associated with the specified named type symbol.
    /// </summary>
    /// <param name="type">The named type symbol for which to retrieve the underlying primitive type information.</param>
    /// <param name="domainTypes">A list of named type symbols representing domain types to track recursion.</param>
    /// <returns>A tuple containing the <see cref="DomainPrimitiveUnderlyingType"/> enum value representing the primitive type and the corresponding named type symbol.</returns>
    public static (DomainPrimitiveUnderlyingType underlyingType, INamedTypeSymbol typeSymbol) GetUnderlyingPrimitiveType(this INamedTypeSymbol type, List<INamedTypeSymbol> domainTypes)
    {
        while (true)
        {
            var underlyingType = type.GetDomainPrimitiveUnderlyingType();

            if (underlyingType != DomainPrimitiveUnderlyingType.Other)
                return (underlyingType, type);

            var domainType = type.Interfaces.FirstOrDefault(x => x.IsDomainValue());

            if (domainType is null)
                return (DomainPrimitiveUnderlyingType.Other, type);

            // Recurse into the domain type
            if (domainType.TypeArguments[0] is not INamedTypeSymbol primitiveType)
                throw new Exception("primitiveType is null");

            domainTypes.Add(type);
            type = primitiveType;
        }
    }

    /// <summary>
    /// Gets the Swagger type and format for a given primitive type.
    /// </summary>
    /// <param name="primitiveType">The named type symbol representing the primitive type.</param>
    /// <returns>A tuple containing the Swagger type and format as strings.</returns>
    public static (string type, string format) GetSwaggerTypeAndFormat(this INamedTypeSymbol primitiveType)
    {
        var underlyingType = primitiveType.GetDomainPrimitiveUnderlyingType();

        if (underlyingType.IsNumeric())
        {
            var format = underlyingType.ToString();
            return underlyingType.IsFloatingPoint()
                ? ("number", format.ToLower(CultureInfo.InvariantCulture))
                : ("integer", format.ToLower(CultureInfo.InvariantCulture));
        }

        return underlyingType switch
        {
            DomainPrimitiveUnderlyingType.Boolean => ("boolean", ""),
            DomainPrimitiveUnderlyingType.Guid => ("string", "uuid"),
            DomainPrimitiveUnderlyingType.Char => ("string", ""),

            DomainPrimitiveUnderlyingType.DateTime => ("string", "date-time"),
            DomainPrimitiveUnderlyingType.DateOnly => ("date", "yyyy-MM-dd"),
            DomainPrimitiveUnderlyingType.TimeOnly => ("string", "HH:mm:ss"),
            DomainPrimitiveUnderlyingType.DateTimeOffset => ("string", "date-time"),
            DomainPrimitiveUnderlyingType.TimeSpan => ("integer", "int64"),

            _ => ("string", "")
        };
    }

    /// <summary>
    /// Gets a friendly name for the named type symbol, including nullable types.
    /// </summary>
    /// <param name="type">The named type symbol to get the friendly name from.</param>
    /// <returns>The friendly name of the type, including nullable types if applicable.</returns>
    public static string GetFriendlyName(this INamedTypeSymbol type)
    {
        var ns = type.ContainingNamespace?.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat.WithGlobalNamespaceStyle(SymbolDisplayGlobalNamespaceStyle.OmittedAsContaining))!;

        if (s_typeAliases.TryGetValue(ns + "." + type.MetadataName, out var result))
        {
            return result;
        }

        var friendlyName = type.MetadataName;

        if (!type.IsGenericType)
            return friendlyName;

        var iBacktick = friendlyName.IndexOf('`');
        if (iBacktick > 0)
            friendlyName = friendlyName.Remove(iBacktick);
        friendlyName += "<";
        var typeParameters = type.TypeArguments;
        for (var i = 0; i < typeParameters.Length; ++i)
        {
            var typeParamName = typeParameters[i].ToString();
            friendlyName += i == 0 ? typeParamName : "," + typeParamName;
        }
        friendlyName += ">";

        return friendlyName;
    }

    /// <summary>
    /// A dictionary that provides aliases for common .NET framework types, mapping their full names to shorter aliases.
    /// </summary>
    private static readonly Dictionary<string, string> s_typeAliases = new()
    {
        { typeof(byte).FullName, "byte" },
        { typeof(sbyte).FullName, "sbyte" },
        { typeof(short).FullName, "short" },
        { typeof(ushort).FullName, "ushort" },
        { typeof(int).FullName, "int" },
        { typeof(uint).FullName, "uint" },
        { typeof(long).FullName, "long" },
        { typeof(ulong).FullName, "ulong" },
        { typeof(float).FullName, "float" },
        { typeof(double).FullName, "double" },
        { typeof(decimal).FullName, "decimal" },
        { typeof(object).FullName, "object" },
        { typeof(bool).FullName, "bool" },
        { typeof(char).FullName, "char" },
        { typeof(string).FullName, "string" },
        { typeof(void).FullName, "void" },
    };
}
