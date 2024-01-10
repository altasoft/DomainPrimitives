using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;

namespace AltaSoft.DomainPrimitives.Generator.Models;

/// <summary>
/// Represents data used by the code generator for generating Domain Primitive types.
/// </summary>
internal sealed class GeneratorData
{
	/// <summary>
	/// Gets or sets the field name.
	/// </summary>
	public string FieldName { get; set; } = default!;

	/// <summary>
	/// Gets or sets a value indicating whether to generate GetHashCode method.
	/// </summary>
	public bool GenerateHashCode { get; set; }

	/// <summary>
	/// Gets or sets the friendly name of the primitive type.
	/// </summary>
	public string PrimitiveTypeFriendlyName { get; set; } = default!;

	/// <summary>
	/// Gets or sets the type symbol.
	/// </summary>
	public INamedTypeSymbol Type { get; set; } = default!;

	/// <summary>
	/// Gets or sets the Domain Primitive type.
	/// </summary>
	public DomainPrimitiveType DomainPrimitiveType { get; set; }

	/// <summary>
	/// Gets or sets the named type symbol of the primitive type.
	/// </summary>
	public INamedTypeSymbol PrimitiveNamedTypeSymbol { get; set; } = default!;

	/// <summary>
	/// Gets or sets the list of parent symbols.
	/// </summary>
	public List<INamedTypeSymbol> ParentSymbols { get; set; } = default!;

	/// <summary>
	/// Gets or sets the numeric type (if applicable).
	/// </summary>
	public NumericType? NumericType { get; set; }

	//public string UnderlyingFriendlyName { get; set; } = default!;

	/// <summary>
	/// Gets or sets the namespace.
	/// </summary>
	public string Namespace { get; set; } = default!;

	/// <summary>
	/// Gets the class name.
	/// </summary>
	public string ClassName => Type.Name;

	/// <summary>
	/// Gets or sets a value indicating whether to generate subtraction operators.
	/// </summary>
	public bool GenerateSubtractionOperators { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether to generate addition operators.
	/// </summary>
	public bool GenerateAdditionOperators { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether to generate division operators.
	/// </summary>
	public bool GenerateDivisionOperators { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether to generate multiplication operators.
	/// </summary>
	public bool GenerateMultiplyOperators { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether to generate modulus operator.
	/// </summary>
	public bool GenerateModulusOperator { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether to generate comparison methods.
	/// </summary>
	public bool GenerateComparison { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether to generate parsable methods.
	/// </summary>
	public bool GenerateParsable { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether to generate comparable methods.
	/// </summary>
	public bool GenerateComparable { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether to generate implicit operators.
	/// </summary>
	public bool GenerateImplicitOperators { get; set; }

	/// <summary>
	/// Gets or sets the DateType (if applicable).
	/// </summary>
	public DateType? DateType { get; set; }

	/// <summary>
	/// Gets or sets the serialization format (if applicable).
	/// </summary>
	public string? SerializationFormat { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether to generate SpanFormattable methods.
	/// </summary>
	public bool GenerateSpanFormattable { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether to generate equitable operators.
	/// </summary>
	public bool GenerateEquitableOperators { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether to generate IConvertible methods.
	/// </summary>
	public bool GenerateConvertibles { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether to generate IUtf8SpanFormattable methods.
	/// </summary>
	public bool GenerateUtf8SpanFormattable { get; set; }

	/// <summary>
	/// Gets the field name for convertible types, including conversions for DateOnly and TimeOnly.
	/// </summary>
	public string FieldNameForConvertible => DateType is DomainPrimitives.Generator.Models.DateType.DateOnly or DomainPrimitives.Generator.Models.DateType.TimeOnly
		? $"{FieldName}.ToDateTime()"
		: $"{FieldName}";

	/// <summary>
	/// Gets the friendly name of the primitive type.
	/// </summary>
	/// <returns>The friendly name of the primitive type. First letter will be uppercase</returns>
	public string GetPrimitiveTypeFriendlyName()
	{
		return DomainPrimitiveType switch
		{
			DomainPrimitiveType.Numeric => NumericType.ToString(),
			DomainPrimitiveType.String => "String",
			DomainPrimitiveType.DateTime => DateType!.ToString(),
			DomainPrimitiveType.Boolean => "Boolean",
			DomainPrimitiveType.Char => "Char",
			DomainPrimitiveType.Guid => "Guid",
			_ => throw new Exception($"DomainPrimitive Type {DomainPrimitiveType} is not yet supported")
		};
	}

	/// <summary>
	/// Gets the friendly name of the primitive type in camel case.
	/// </summary>
	/// <returns>The friendly name of the primitive type in camel case. LowerCase will be available only for numbers and string</returns>
	public string GetPrimitiveTypeFriendlyNameInCamelCase()
	{
		var friendlyName = GetPrimitiveTypeFriendlyName();
		if (DomainPrimitiveType is DomainPrimitiveType.Numeric or DomainPrimitiveType.String)
			return char.ToLower(friendlyName[0]) + friendlyName.Substring(1);

		return friendlyName;
	}
}