using System.Collections.Generic;
using Microsoft.CodeAnalysis;

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
    public INamedTypeSymbol TypeSymbol { get; set; } = default!;

    /// <summary>
    /// Gets or sets the Domain Primitive type.
    /// </summary>
    public DomainPrimitiveUnderlyingType UnderlyingType { get; set; }

    /// <summary>
    /// Gets or sets the named type symbol of the primitive type.
    /// </summary>
    public INamedTypeSymbol PrimitiveTypeSymbol { get; set; } = default!;

    /// <summary>
    /// Gets or sets the list of parent symbols.
    /// </summary>
    public List<INamedTypeSymbol> ParentSymbols { get; set; } = default!;

    /// <summary>
    /// Gets or sets the namespace.
    /// </summary>
    public string Namespace { get; set; } = default!;

    /// <summary>
    /// Gets the class name.
    /// </summary>
    public string ClassName => TypeSymbol.Name;

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
    /// Gets or sets a value indicating whether to generate IParsable methods.
    /// </summary>
    public bool GenerateParsable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to generate implicit operators.
    /// </summary>
    public bool GenerateImplicitOperators { get; set; }

    /// <summary>
    /// Gets or sets the serialization format (if applicable).
    /// </summary>
    public string? SerializationFormat { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to generate ISpanFormattable methods.
    /// </summary>
    public bool GenerateSpanFormattable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to generate IConvertible methods.
    /// </summary>
    public bool GenerateConvertibles { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to generate IUtf8SpanFormattable methods.
    /// </summary>
    public bool GenerateUtf8SpanFormattable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the generate IXmlSerializable methods.
    /// </summary>
    public bool GenerateXmlSerializableMethods { get; set; }
}
