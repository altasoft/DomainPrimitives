using AltaSoft.DomainPrimitives.Generator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
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
		return x is { IsGenericType: true, Name: "IDomainValue" } &&
			   x.ContainingNamespace.ToDisplayString() == "AltaSoft.DomainPrimitives.Abstractions";
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

		builder.Append("<");
		for (var index = 0; index < type.TypeArguments.Length; index++)
		{
			var arg = type.TypeArguments[index];
			builder.Append(arg.Name);

			if (index != type.TypeArguments.Length - 1)
				builder.Append(", ");
		}

		builder.Append(">");

		return builder.ToString();
	}

	//public static bool IsDateOrDateTime(this INamedTypeSymbol? type, INamedTypeSymbol x)
	//{
	//	SpecialType.System_DateTime
	//}

	/// <summary>
	/// Gets the corresponding <see cref="NumericType"/> enum value from the specified named type symbol.
	/// </summary>
	/// <param name="type">The named type symbol for which to retrieve the corresponding numeric type.</param>
	/// <returns>The <see cref="NumericType"/> enum value representing the numeric type.</returns>
	public static NumericType GetNumericTypeFromNamedTypeSymbol(this INamedTypeSymbol type) => s_numericTypes[type.SpecialType];

	/// <summary>
	/// Attempts to retrieve the <see cref="DateType"/> enum value from the specified named type symbol.
	/// </summary>
	/// <param name="type">The named type symbol to check for a date type.</param>
	/// <param name="dateType">When this method returns, contains the <see cref="DateType"/> enum value if the type represents a date type; otherwise, null.</param>
	/// <returns>True if the type represents a date type; otherwise, false.</returns>
	public static bool TryGetDateTypeSymbol(this INamedTypeSymbol type, out DateType? dateType)
	{
		if (type.SpecialType == SpecialType.System_DateTime)
		{
			dateType = DateType.DateTime;
			return true;
		}

		if (type.ToDisplayString() == "System.DateOnly")
		{
			dateType = DateType.DateOnly;
			return true;
		}

		if (type.ToDisplayString() == "System.TimeOnly")
		{
			dateType = DateType.TimeOnly;
			return true;
		}

		if (type.ToDisplayString() == "System.TimeSpan")
		{
			dateType = DateType.TimeSpan;
			return true;
		}

		if (type.ToDisplayString() == "System.DateTimeOffset")
		{
			dateType = DateType.DateTimeOffset;
			return true;
		}

		dateType = null;
		return false;
	}

	/// <summary>
	/// Gets the <see cref="DateType"/> enum value from the specified named type symbol representing a date type.
	/// </summary>
	/// <param name="type">The named type symbol representing a date type.</param>
	/// <returns>The <see cref="DateType"/> enum value representing the date type.</returns>
	public static DateType GetDateTypeFromNamedTypeSymbol(this INamedTypeSymbol type)
	{
		if (!type.TryGetDateTypeSymbol(out var value) || value is null)
			throw new Exception("Invalid value for DateType");

		return value.Value;
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
	/// <returns>A tuple containing the <see cref="PrimitiveCategory"/> enum value representing the primitive type and the corresponding named type symbol.</returns>
	public static (PrimitiveCategory category, INamedTypeSymbol typeSymbol) GetUnderlyingPrimitiveCategory(this INamedTypeSymbol type, List<INamedTypeSymbol> domainTypes)
	{
		while (true)
		{
			switch (type.SpecialType)
			{
				case SpecialType.System_String:
					return (PrimitiveCategory.String, type);

				case SpecialType.System_Boolean:
					return (PrimitiveCategory.Boolean, type);

				case SpecialType.System_Char:
					return (PrimitiveCategory.Char, type);
			}

			if (s_numericTypes.TryGetValue(type.SpecialType, out _)) return (PrimitiveCategory.Numeric, type);

			if (type.ToDisplayString() == "System.Guid") return (PrimitiveCategory.Guid, type);

			if (type.TryGetDateTypeSymbol(out _)) return (PrimitiveCategory.DateTime, type);

			var domainType = type.Interfaces.FirstOrDefault(x => x.IsDomainValue());

			if (domainType is null)
				return (PrimitiveCategory.Other, type);

			// Recurse into the domain type
			if (domainType.TypeArguments[0] is not INamedTypeSymbol primitiveType) throw new Exception("primitiveType is null");

			domainTypes.Add(type);
			type = primitiveType;
		}
	}

	/// <summary>
	/// Checks if the specified named type symbol represents a numeric type and retrieves its corresponding NumericType enum value.
	/// </summary>
	/// <param name="type">The named type symbol to check for numeric type.</param>
	/// <param name="numericType">When this method returns, contains the corresponding NumericType enum value if the type is numeric; otherwise, null.</param>
	/// <returns>True if the specified type is a numeric type; otherwise, false.</returns>
	public static bool IsNumericType(this INamedTypeSymbol type, out NumericType? numericType)
	{
		numericType = null;
		if (s_numericTypes.TryGetValue(type.SpecialType, out var n))
		{
			numericType = n;
			return true;
		}
		return false;
	}

	/// <summary>
	/// Clears all cached types in the internal dictionary.
	/// </summary>
	internal static void ClearTypes()
	{
		s_numericTypes.Clear();
	}

	/// <summary>
	/// Initializes commonly used types from the provided compilation.
	/// </summary>
	internal static void InitializeTypes()
	{
		s_numericTypes.Clear();

		s_numericTypes.Add(SpecialType.System_Byte, NumericType.Byte);
		s_numericTypes.Add(SpecialType.System_SByte, NumericType.SByte);
		s_numericTypes.Add(SpecialType.System_Int16, NumericType.Int16);
		s_numericTypes.Add(SpecialType.System_UInt16, NumericType.UInt16);
		s_numericTypes.Add(SpecialType.System_Int32, NumericType.Int32);
		s_numericTypes.Add(SpecialType.System_UInt32, NumericType.UInt32);
		s_numericTypes.Add(SpecialType.System_Int64, NumericType.Int64);
		s_numericTypes.Add(SpecialType.System_UInt64, NumericType.UInt64);
		s_numericTypes.Add(SpecialType.System_Decimal, NumericType.Decimal);
		s_numericTypes.Add(SpecialType.System_Double, NumericType.Double);
		s_numericTypes.Add(SpecialType.System_Single, NumericType.Single);

		//var i128Type = compilation.GetTypeByMetadataName("System.Int128");
		//if (i128Type is not null)
		//	s_numericTypes.Add(i128Type, NumericType.Int128);

		//i128Type = compilation.GetTypeByMetadataName("System.UInt128");
		//if (i128Type is not null)
		//	s_numericTypes.Add(i128Type, NumericType.UInt128);
	}

	/// <summary>
	/// A dictionary that maps INamedTypeSymbol instances representing numeric types to their corresponding NumericType enum values.
	/// </summary>
	private static readonly Dictionary<SpecialType, NumericType> s_numericTypes = new();

	/// <summary>
	/// Gets the Swagger type and format for a given primitive type.
	/// </summary>
	/// <param name="primitiveType">The named type symbol representing the primitive type.</param>
	/// <returns>A tuple containing the Swagger type and format as strings.</returns>
	public static (string type, string format) GetSwaggerTypeAndFormat(this INamedTypeSymbol primitiveType)
	{
		if (primitiveType.IsNumericType(out var value))
		{
			var format = value.ToString();
			return value is NumericType.Int16 or NumericType.Int32 or NumericType.Int64 // or NumericType.Int128
				? ("integer", format[0] + format.Substring(1))
				: ("number", format[0] + format.Substring(1));
		}

		if (primitiveType.TryGetDateTypeSymbol(out var type))
		{
			if (type == DateType.DateTime)
				return ("string", "date-time");

			if (type == DateType.DateOnly)
				return ("string", "yyyy-MM-dd");

			if (type == DateType.TimeOnly)
				return ("string", "HH:mm:ss");

			if (type == DateType.DateTimeOffset)
				return ("string", "date-time");

			if (type == DateType.TimeSpan)
				return ("integer", "int64");
		}

		return ("string", ""); //todo
	}

	/// <summary>
	/// Gets a friendly name for the named type symbol, including nullable types.
	/// </summary>
	/// <param name="type">The named type symbol to get the friendly name from.</param>
	/// <param name="nullableOfTType">The named type symbol representing nullable&lt;T&gt;.</param>
	/// <returns>The friendly name of the type, including nullable types if applicable.</returns>
	public static string GetFriendlyName(this INamedTypeSymbol type, INamedTypeSymbol nullableOfTType)
	{
		var ns = type.ContainingNamespace?.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat.WithGlobalNamespaceStyle(SymbolDisplayGlobalNamespaceStyle.OmittedAsContaining))!;

		if (TypeAliases.TryGetValue(ns + "." + type.MetadataName, out var result))
		{
			return result;
		}

		var friendlyName = type.MetadataName;

		if (!type.IsGenericType)
			return friendlyName;

		if (type.IsNullableValueType(nullableOfTType, out var underlyingType))
			return underlyingType!.GetFriendlyName(nullableOfTType) + '?';

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
	/// Checks if the named type symbol represents a nullable value type and retrieves its underlying type symbol.
	/// </summary>
	/// <param name="type">The named type symbol to check.</param>
	/// <param name="nullableOfTType">The named type symbol representing nullable&lt;T&gt;.</param>
	/// <param name="underlyingTypeSymbol">The underlying type symbol if the type is nullable; otherwise, null.</param>
	/// <returns>True if the type is nullable; otherwise, false.</returns>
	public static bool IsNullableValueType(this INamedTypeSymbol type, INamedTypeSymbol nullableOfTType, out INamedTypeSymbol? underlyingTypeSymbol)
	{
		if (type.IsGenericType && type.ConstructedFrom.Equals(nullableOfTType, SymbolEqualityComparer.Default))
		{
			underlyingTypeSymbol = (INamedTypeSymbol)type.TypeArguments[0];
			return true;
		}

		underlyingTypeSymbol = null;
		return false;
	}

	/// <summary>
	/// A dictionary that provides aliases for common .NET framework types, mapping their full names to shorter aliases.
	/// </summary>
	private static readonly Dictionary<string, string> TypeAliases = new()
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