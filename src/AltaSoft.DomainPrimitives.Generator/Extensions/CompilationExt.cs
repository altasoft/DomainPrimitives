using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
	/// Gets explicit conversion methods for a given named type symbol.
	/// </summary>
	/// <param name="self">The named type symbol.</param>
	/// <returns>An IEnumerable of IMethodSymbol representing explicit conversion methods.</returns>
	public static IEnumerable<IMethodSymbol> GetExplicitMethods(this INamedTypeSymbol self)
	{
		return self.GetMembersOfType<IMethodSymbol>().Where(x => x.MethodKind == MethodKind.Conversion)
			.Where(op =>
				!op.ReturnsVoid && // Conversion operators have a non-void return type
				op.ExplicitInterfaceImplementations.Length == 0 && // Exclude explicit interface implementations
				op.Parameters.Length == 1 && // Usually one parameter for conversion operators
				op.IsStatic && // Static methods
				op.Name.StartsWith("op_Explicit"));
	}

	/// <summary>
	/// Gets implicit conversion methods for a given named type symbol.
	/// </summary>
	/// <param name="self">The named type symbol.</param>
	/// <returns>An IEnumerable of IMethodSymbol representing implicit conversion methods.</returns>
	public static IEnumerable<IMethodSymbol> GetImplicitMethods(this INamedTypeSymbol self)
	{
		return self.GetMembersOfType<IMethodSymbol>().Where(x => x.MethodKind == MethodKind.Conversion)
			.Where(op =>
				!op.ReturnsVoid && // Conversion operators have a non-void return type
				op.ExplicitInterfaceImplementations.Length == 0 && // Exclude explicit interface implementations
				op.Parameters.Length == 1 && // Usually one parameter for conversion operators
				op.IsStatic && // Static methods
				op.Name.StartsWith("op_Implicit"));
	}

	/// <summary>
	/// Checks if the given named type symbol implements the IDomainValue interface.
	/// </summary>
	/// <param name="x">The named type symbol to check.</param>
	/// <returns>True if the type implements the IDomainValue interface; otherwise, false.</returns>
	public static bool IsDomainValue(this INamedTypeSymbol x)
	{
		return x.IsGenericType && x.ContainingNamespace.ToDisplayString() + "." + x.Name == "AltaSoft.DomainPrimitives.Abstractions.IDomainValue";
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

	/// <summary>
	/// Gets properties of a given ITypeSymbol
	/// </summary>
	/// <param name="self">The ITypeSymbol to retrieve properties from.</param>
	/// <returns>An IEnumerable of IPropertySymbol representing the properties.</returns>
	public static IEnumerable<IPropertySymbol> GetProperties(this ITypeSymbol self) => GetProperties(self, null, DefaultFlags);

	/// <summary>
	/// Gets properties of a given ITypeSymbol based on the provided conditions.
	/// </summary>
	/// <param name="self">The ITypeSymbol to retrieve properties from.</param>
	/// <param name="where">Optional condition to filter properties.</param>
	/// <returns>An IEnumerable of IPropertySymbol representing the properties.</returns>
	public static IEnumerable<IPropertySymbol> GetProperties(this ITypeSymbol self, Func<IPropertySymbol, bool>? where) => GetProperties(self, where, DefaultFlags);

	/// <summary>
	/// Gets properties of a given ITypeSymbol based on the provided conditions.
	/// </summary>
	/// <param name="self">The ITypeSymbol to retrieve properties from.</param>
	/// <param name="bindingAttr">Binding flags for property access.</param>
	/// <returns>An IEnumerable of IPropertySymbol representing the properties.</returns>
	public static IEnumerable<IPropertySymbol> GetProperties(this ITypeSymbol self, Func<IPropertySymbol, bool>? condition, BindingFlags bindingAttr)
	{
		return self.GetMembersOfType<IPropertySymbol>().Where(x =>
		{
			if (self.IsRecord && x.DeclaringSyntaxReferences.Length == 0)
			{
				return false;
			}

			// Skip if:
			if (
				// we want a static property and this is not static
				((BindingFlags.Static & bindingAttr) != 0 && !x.IsStatic) ||
				// we want an instance property and this is static
				((BindingFlags.Instance & bindingAttr) != 0 && x.IsStatic))
			{
				return false;
			}

			if (condition is not null && !condition(x))
			{
				return false;
			}
			return ((BindingFlags.Public & bindingAttr) != 0 && x.IsPublic()) ||
				   (BindingFlags.NonPublic & bindingAttr) != 0;
		});
	}

	#region Accessibility

	/// <summary>
	/// Checks if the symbol has private accessibility.
	/// </summary>
	/// <param name="symbol">The symbol to check.</param>
	/// <returns>True if the symbol has private accessibility; otherwise, false.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool IsPrivate(this ISymbol symbol) => symbol.DeclaredAccessibility == Accessibility.Private;

	/// <summary>
	/// Checks if the symbol has protected accessibility.
	/// </summary>
	/// <param name="symbol">The symbol to check.</param>
	/// <returns>True if the symbol has protected accessibility; otherwise, false.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool IsProtected(this ISymbol symbol) => symbol.DeclaredAccessibility == Accessibility.Protected;

	/// <summary>
	/// Checks if the symbol has internal accessibility.
	/// </summary>
	/// <param name="symbol">The symbol to check.</param>
	/// <returns>True if the symbol has internal accessibility; otherwise, false.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool IsInternal(this ISymbol symbol) => symbol.DeclaredAccessibility == Accessibility.Internal;

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
	public static NumericType GetFromNamedTypeSymbol(this INamedTypeSymbol type) => s_numericTypes[type];

	/// <summary>
	/// Attempts to retrieve the <see cref="DateType"/> enum value from the specified named type symbol.
	/// </summary>
	/// <param name="type">The named type symbol to check for a date type.</param>
	/// <param name="dateType">When this method returns, contains the <see cref="DateType"/> enum value if the type represents a date type; otherwise, null.</param>
	/// <returns>True if the type represents a date type; otherwise, false.</returns>
	public static bool TryGetDateTypeSymbol(this INamedTypeSymbol type, out DateType? dateType)
	{
		if (type.Equals(_dateTimeType, SymbolEqualityComparer.Default))
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
	public static DateType GetDateTypeTypeSymbol(this INamedTypeSymbol type)
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
	/// <returns>A tuple containing the <see cref="DomainPrimitiveType"/> enum value representing the primitive type and the corresponding named type symbol.</returns>
	public static (DomainPrimitiveType type, INamedTypeSymbol symbol) GetUnderlyingPrimitiveType(this INamedTypeSymbol type, List<INamedTypeSymbol> domainTypes)
	{
		if (s_numericTypes.TryGetValue(type, out _))
			return (DomainPrimitiveType.Numeric, type);

		if (type.Equals(_stringType, SymbolEqualityComparer.Default))
			return (DomainPrimitiveType.String, type);

		if (type.TryGetDateTypeSymbol(out _))
			return (DomainPrimitiveType.DateTime, type);

		if (type.Equals(_boolType, SymbolEqualityComparer.Default))
			return (DomainPrimitiveType.Boolean, type);

		if (type.Equals(_charType, SymbolEqualityComparer.Default))
			return (DomainPrimitiveType.Char, type);

		if (type.ToDisplayString() == "System.Guid")
			return (DomainPrimitiveType.Guid, type);

		var domainType = type.Interfaces.FirstOrDefault(x => x.IsDomainValue());

		if (domainType is null)
			return (DomainPrimitiveType.Other, type);

		var primitiveType = domainType.TypeArguments[0] as INamedTypeSymbol;
		domainTypes.Add(type);

		return primitiveType!.GetUnderlyingPrimitiveType(domainTypes);
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
		if (s_numericTypes.TryGetValue(type, out var n))
		{
			numericType = n;
			return true;
		}
		return false;
	}

	private static INamedTypeSymbol _stringType = default!;
	private static INamedTypeSymbol _boolType = default!;
	private static INamedTypeSymbol _charType = default!;
	private static INamedTypeSymbol _dateTimeType = default!;

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
	/// <param name="compilation">The Roslyn Compilation instance.</param>
	internal static void InitializeTypes(Compilation compilation)
	{
		_stringType = compilation.GetSpecialType(SpecialType.System_String);
		_dateTimeType = compilation.GetSpecialType(SpecialType.System_DateTime);
		_boolType = compilation.GetSpecialType(SpecialType.System_Boolean);
		_charType = compilation.GetSpecialType(SpecialType.System_Char);

		s_numericTypes.Clear();

		s_numericTypes.Add(compilation.GetSpecialType(SpecialType.System_Byte), NumericType.Byte);
		s_numericTypes.Add(compilation.GetSpecialType(SpecialType.System_SByte), NumericType.SByte);
		s_numericTypes.Add(compilation.GetSpecialType(SpecialType.System_Int16), NumericType.Int16);
		s_numericTypes.Add(compilation.GetSpecialType(SpecialType.System_UInt16), NumericType.UInt16);
		s_numericTypes.Add(compilation.GetSpecialType(SpecialType.System_Int32), NumericType.Int32);
		s_numericTypes.Add(compilation.GetSpecialType(SpecialType.System_UInt32), NumericType.UInt32);
		s_numericTypes.Add(compilation.GetSpecialType(SpecialType.System_Int64), NumericType.Int64);
		s_numericTypes.Add(compilation.GetSpecialType(SpecialType.System_UInt64), NumericType.UInt64);
		s_numericTypes.Add(compilation.GetSpecialType(SpecialType.System_Decimal), NumericType.Decimal);
		s_numericTypes.Add(compilation.GetSpecialType(SpecialType.System_Double), NumericType.Double);
		s_numericTypes.Add(compilation.GetSpecialType(SpecialType.System_Single), NumericType.Single);
	}

	/// <summary>
	/// A dictionary that maps INamedTypeSymbol instances representing numeric types to their corresponding NumericType enum values.
	/// </summary>
	private static readonly Dictionary<INamedTypeSymbol, NumericType> s_numericTypes = new(SymbolEqualityComparer.Default);

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
			if (value is NumericType.Int16 or NumericType.Int32 or NumericType.Int64)
				return ("integer", format[0] + format.Substring(1));

			return ("number", format[0] + format.Substring(1));
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

		string friendlyName = type.MetadataName;

		if (type.IsGenericType)
		{
			if (type.IsNullableValueType(nullableOfTType, out var underlyingType))
			{
				return underlyingType!.GetFriendlyName(nullableOfTType) + '?';
			}

			var iBacktick = friendlyName.IndexOf('`');
			if (iBacktick > 0)
			{
				friendlyName = friendlyName.Remove(iBacktick);
			}
			friendlyName += "<";
			var typeParameters = type.TypeArguments;
			for (var i = 0; i < typeParameters.Length; ++i)
			{
				var typeParamName = typeParameters[i].ToString();
				friendlyName += i == 0 ? typeParamName : "," + typeParamName;
			}
			friendlyName += ">";
		}

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

	private const BindingFlags DefaultFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;

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