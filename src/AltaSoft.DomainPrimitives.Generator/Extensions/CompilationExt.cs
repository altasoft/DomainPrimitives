using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using AltaSoft.DomainPrimitives.Generator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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
        return x is { IsGenericType: true, Name: "IDomainValue" } && string.Equals(x.ContainingNamespace.ToDisplayString(), "AltaSoft.DomainPrimitives", StringComparison.Ordinal);
    }

    /// <summary>
    /// Gets members of a specific type for a given ITypeSymbol.
    /// </summary>
    /// <typeparam name="TMember">The type of members to retrieve.</typeparam>
    /// <param name="self">The ITypeSymbol to retrieve members from.</param>
    /// <returns>An IEnumerable of members of the specified type.</returns>
    public static IEnumerable<TMember> GetMembersOfType<TMember>(this ITypeSymbol? self) where TMember : ISymbol
    {
        return self?.GetMembers().OfType<TMember>() ?? [];
    }

    #region Accessibility

    /// <summary>
    /// Checks if the symbol has public accessibility.
    /// </summary>
    /// <param name="symbol">The symbol to check.</param>
    /// <returns>True if the symbol has public accessibility; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPublic(this ISymbol symbol) => symbol.DeclaredAccessibility == Accessibility.Public;

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
            if (syntax.GetSyntax() is TypeDeclarationSyntax typeDeclaration && string.Equals(typeDeclaration.GetClassName(), self.GetClassNameWithArguments(), StringComparison.Ordinal))
            {
                var modifiers = typeDeclaration.Modifiers.ToString();
                if (typeDeclaration is RecordDeclarationSyntax)
                    modifiers += " record";

                return modifiers;
            }
        }

        return null;
    }

    public static string GetAccessibility(this INamedTypeSymbol symbol)
    {
        return symbol.DeclaredAccessibility switch
        {
            Accessibility.Public => "public",
            Accessibility.Private => "private",
            Accessibility.ProtectedAndInternal => "protected internal",
            Accessibility.Protected => "protected",
            Accessibility.Internal => "internal",
            Accessibility.ProtectedOrInternal => "private protected", // Since C# 7.2
            _ => "internal" // Default for top-level types
        };
    }

    #endregion Accessibility
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
    /// Determines whether the specified <see cref="DomainPrimitiveUnderlyingType"/> implements  <see cref="IConvertible"/>
    /// </summary>
    /// <param name="self">The <see cref="DomainPrimitiveUnderlyingType"/> to check.</param>
    /// <returns>
    ///   <c>true</c> if the specified <see cref="DomainPrimitiveUnderlyingType"/> is IConvertible; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsIConvertible(this DomainPrimitiveUnderlyingType self)
    {
        return self switch
        {
            DomainPrimitiveUnderlyingType.Guid => false,
            DomainPrimitiveUnderlyingType.TimeSpan => false,
            DomainPrimitiveUnderlyingType.DateTimeOffset => false,
            DomainPrimitiveUnderlyingType.Other => false,
            _ => true
        };
    }

    /// <param name="domainPrimitiveType">The named type symbol representing the primitive type.</param>
    extension(INamedTypeSymbol domainPrimitiveType)
    {

        /// <summary>
        /// Checks if the specified named type symbol implements the specified interface by its full name.
        /// </summary>
        /// <param name="interfaceFullName">The full name of the interface to check for implementation.</param>
        /// <returns>True if the type implements the interface; otherwise, false.</returns>
        public bool ImplementsInterface(string interfaceFullName)
        {
            return domainPrimitiveType.Interfaces.Any(x => string.Equals(x.ContainingNamespace.ToDisplayString() + "." + x.Name, interfaceFullName, StringComparison.Ordinal));
        }

        /// <summary>
        /// Gets the underlying primitive type information associated with the specified named type symbol.
        /// </summary>
        /// <param name="domainTypes">A list of named type symbols representing domain types to track recursion.</param>
        /// <returns>A tuple containing the <see cref="DomainPrimitiveUnderlyingType"/> enum value representing the primitive type and the corresponding named type symbol.</returns>
        public (DomainPrimitiveUnderlyingType underlyingType, INamedTypeSymbol typeSymbol) GetUnderlyingPrimitiveType(List<INamedTypeSymbol> domainTypes)
        {
            while (true)
            {
                var underlyingType = domainPrimitiveType.GetDomainPrimitiveUnderlyingType();

                if (underlyingType != DomainPrimitiveUnderlyingType.Other)
                    return (underlyingType, domainPrimitiveType);

                var domainType = domainPrimitiveType.Interfaces.FirstOrDefault(x => x.IsDomainValue());

                if (domainType is null)
                    return (DomainPrimitiveUnderlyingType.Other, domainPrimitiveType);

                // Recurse into the domain type
                if (domainType.TypeArguments[0] is not INamedTypeSymbol primitiveType)
                    throw new InvalidOperationException("primitiveType is null");

                domainTypes.Add(domainPrimitiveType);
                domainPrimitiveType = primitiveType;
            }
        }

        /// <summary>
        /// Gets the Old type and format for a given primitive type.
        /// </summary>
        /// <returns>A tuple containing the Swagger type and format as strings.</returns>
        public (string type, string format) GetOldOpenApiTypeAndFormat()
        {
            var underlyingType = domainPrimitiveType.GetDomainPrimitiveUnderlyingType();

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
                DomainPrimitiveUnderlyingType.DateOnly => ("string", "date"),
                DomainPrimitiveUnderlyingType.TimeOnly => ("string", "HH:mm:ss"),
                DomainPrimitiveUnderlyingType.DateTimeOffset => ("string", "date-time"),
                DomainPrimitiveUnderlyingType.TimeSpan => ("integer", "int64"),

                _ => ("string", "")
            };
        }

        /// <summary>
        /// Gets the OpenApiType and format for a given primitive type.
        /// </summary>
        /// <returns>A tuple containing the OpenApi type and format as strings.</returns>
        public (string type, string format) GetOpenApiTypeAndFormat()
        {
            var underlyingType = domainPrimitiveType.GetDomainPrimitiveUnderlyingType();

            //mapping is retrieved from: https://github.com/dotnet/aspnetcore/blob/main/src/OpenApi/src/Extensions/JsonNodeSchemaExtensions.cs#L27
            return underlyingType switch
            {
                DomainPrimitiveUnderlyingType.String => ("JsonSchemaType.String", ""),

                DomainPrimitiveUnderlyingType.Boolean => ("JsonSchemaType.Boolean", ""),
                DomainPrimitiveUnderlyingType.Guid => ("JsonSchemaType.String", "uuid"),
                DomainPrimitiveUnderlyingType.Char => ("JsonSchemaType.String", "char"),

                DomainPrimitiveUnderlyingType.DateTime => ("JsonSchemaType.String", "date-time"),
                DomainPrimitiveUnderlyingType.DateTimeOffset => ("JsonSchemaType.String", "date-time"),
                DomainPrimitiveUnderlyingType.DateOnly => ("JsonSchemaType.String", "date"),
                DomainPrimitiveUnderlyingType.TimeOnly => ("JsonSchemaType.String", "time"), // ISO 8601 time format
                DomainPrimitiveUnderlyingType.TimeSpan => ("JsonSchemaType.Integer", "int64"),

                //integer type
                DomainPrimitiveUnderlyingType.SByte => ("JsonSchemaType.Integer", "int8"),
                DomainPrimitiveUnderlyingType.Byte => ("JsonSchemaType.Integer", "uint8"),
                DomainPrimitiveUnderlyingType.Int16 => ("JsonSchemaType.Integer", "int16"),
                DomainPrimitiveUnderlyingType.UInt16 => ("JsonSchemaType.Integer", "uint16"),
                DomainPrimitiveUnderlyingType.Int32 => ("JsonSchemaType.Integer", "int32"),
                DomainPrimitiveUnderlyingType.UInt32 => ("JsonSchemaType.Integer", "uint32"),
                DomainPrimitiveUnderlyingType.Int64 => ("JsonSchemaType.Integer", "int64"),
                DomainPrimitiveUnderlyingType.UInt64 => ("JsonSchemaType.Integer", "uint64"),

                //floating points
                DomainPrimitiveUnderlyingType.Single => ("JsonSchemaType.Number", "float"),
                DomainPrimitiveUnderlyingType.Double => ("JsonSchemaType.Number", "double"),
                // decimal
                DomainPrimitiveUnderlyingType.Decimal => ("JsonSchemaType.Number", "double"),

                _ => ("JsonSchemaType.String", "")
            };
        }

        /// <summary>
        /// Gets a friendly name for the named type symbol, including nullable types.
        /// </summary>
        /// <returns>The friendly name of the type, including nullable types if applicable.</returns>
        public string GetFriendlyName()
        {
            var ns = domainPrimitiveType.ContainingNamespace?.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat.WithGlobalNamespaceStyle(SymbolDisplayGlobalNamespaceStyle.OmittedAsContaining))!;

            if (s_typeAliases.TryGetValue(ns + "." + domainPrimitiveType.MetadataName, out var result))
            {
                return result;
            }

            var friendlyName = new StringBuilder(domainPrimitiveType.MetadataName);

            if (!domainPrimitiveType.IsGenericType)
                return friendlyName.ToString();

            var iBacktick = friendlyName.ToString().IndexOf('`');
            if (iBacktick > 0)
                friendlyName.Length = iBacktick;
            friendlyName.Append('<');
            var typeParameters = domainPrimitiveType.TypeArguments;
            for (var i = 0; i < typeParameters.Length; ++i)
            {
                if (i > 0)
                    friendlyName.Append(',');
                friendlyName.Append(typeParameters[i]);
            }
            friendlyName.Append('>');

            return friendlyName.ToString();
        }
    }

    /// <summary>
    /// A dictionary that provides aliases for common .NET framework types, mapping their full names to shorter aliases.
    /// </summary>
    private static readonly Dictionary<string, string> s_typeAliases = new(StringComparer.Ordinal)
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
