# DomainPrimitives for C#  

[![Version](https://img.shields.io/nuget/v/AltaSoft.DomainPrimitives?label=Version&color=0c3c60&style=for-the-badge&logo=nuget)](https://www.nuget.org/profiles/AltaSoft)
[![Dot NET 8+](https://img.shields.io/static/v1?label=DOTNET&message=8%2B&color=0c3c60&style=for-the-badge)](https://dotnet.microsoft.com)

# Table of Contents

- [Introduction](#introduction)
- [Key Features](#key-features)
- [What's New](#whats-new)
- [Generator Features](#generator-features)
- [Supported Underlying types](#supported-underlying-types)
- [Getting Started](#getting-started)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Creating your Domain type](#creating-your-domain-type)
- [OpenAPI Integration](#openapi-integration)
- [Managing Generated Operators for numeric types](#managing-generated-operators-for-numeric-types)
- [Managing Serialization Format for date-related types](#managing-serialization-format-for-date-related-types)
- [Enhanced DateTime Interoperability](#enhanced-datetime-interoperability)
- [Json Conversion](#json-conversion)
- [Transform Method](#transform-method)
- [Contributions](#contributions)
- [Contact](#contact)
- [License](#license)

## Introduction

Welcome to **AltaSoft.DomainPrimitives** - a C# toolkit purposefully designed to accelerate the development of domain-specific primitives within your applications. This streamlined solution empowers developers to efficiently encapsulate fundamental domain logic. Through this toolkit, you'll significantly reduce code complexity while improving the maintainability of your project.
## Key Features
* **Simplified Primitive Creation** - Utilize source generators to swiftly create domain-specific primitives with ease and precision.
* **Versatile Underlying Type Support** - Embrace a wide array of underlying types, catering to diverse application requirements.
* **Enhanced Code Quality** - Create clean, maintainable, and thoroughly testable code through encapsulation and robust design principles.


With `AltaSoft.DomainPrimitives`, experience an accelerated development process while upholding code quality standards. This toolkit empowers developers to focus on the core business logic without compromising on precision or efficiency.

## What's New

### .NET 10 Support & Infrastructure Improvements

The library now supports .NET 8, .NET 9, and .NET 10, with .NET 7 support deprecated in favor of newer framework versions.

**Key Updates:**

* **Multi-Framework Support:** Target frameworks updated to `net8.0`, `net9.0`, and `net10.0`
* **Central Package Management:** Introduced `Directory.Packages.props` for centralized NuGet package version management
* **Source Link Support:** Enhanced debugging experience with embedded source code and deterministic builds in CI/CD pipelines
* **OpenAPI Extensions:** New `AltaSoft.DomainPrimitives.OpenApiExtensions` package for .NET 9+ minimal API integration with native OpenAPI support
* **Generation Control Flags:** Fine-grained control over code generation with new MSBuild properties:
  - `DomainPrimitiveGenerator_GenerateImplicitOperators` - Control implicit operator generation
  - `DomainPrimitiveGenerator_GenerateNumericOperators` - Control numeric operator generation for numeric types
  - `DomainPrimitiveGenerator_GenerateOpenApiHelper` - Control OpenAPI helper generation
* **Enhanced Testing:** Updated test infrastructure with latest xUnit and verification tools
* **Improved Code Generation:** Better handling of uninitialized domain primitives and enhanced operator generation
 
## Generator Features 

The **AltaSoft.DomainPrimitives.Generator** offers a diverse set of features:

* **Implicit Operators:** Streamlines type conversion to/from the underlying primitive type, including nullable conversions. [Example](#implicit-usage-of-domaintype)
* **Enhanced Date/Time Conversions:** `DateOnly` and `TimeOnly` domain primitives include additional implicit conversion operators to/from `DateTime` for seamless interoperability.
* **Specialized Constructor Generation:**  Automatically validates and constructs instances of this domain type. This constructor, tailored for the domain primitive, utilizes the underlying type as a parameter, ensuring the value's correctness within the domain.
* **TryCreate method:** Introduces a TryCreate method that attempts to create an instance of the domain type and returns a bool indicating the success or failure of the creation process, along with any validation errors.
* **JsonConverters:** Handles JSON serialization and deserialization for the underlying type, including property name serialization support. [Example](#json-conversion)
* **TypeConverters:** Assists in type conversion to/from it's underlying type. [Please refer to generated type converter below](#type-converter)
* **Swagger Custom Type Mappings:** Facilitates easy integration with Swagger by treating the primitive type as it's underlying type, with full nullable support. [Please refer to generated swagger helper below](#swagger-mappers)
* **OpenAPI Schema Transformers:** For .NET 9+ applications using minimal APIs with OpenAPI support, the new `AltaSoft.DomainPrimitives.OpenApiExtensions` package provides schema transformers that automatically configure OpenAPI documentation. [See OpenAPI Integration](#openapi-integration)
* **Interface Implementations:** All DomainPrimitives implement comprehensive interfaces for full framework integration:
  - `IEquatable<T>`, `IComparable`, `IComparable<T>` for equality and comparison operations
  - `IConvertible` for type conversion support
  - `IParsable<T>` for parsing from strings
  - `ISpanFormattable` and `IUtf8SpanFormattable` (NET8+) for efficient formatting
  - Numeric types implement `IAdditionOperators<T>`, `ISubtractionOperators<T>`, etc. as appropriate
* **NumberType Operations:** Automatically generates basic arithmetic and comparison operators, by implementing Static abstract interfaces. [More details regarding numeric types](#managing-generated-operators-for-numeric-types)
* **IParsable Implementation:** Automatically generates parsing for non-string types.
* **XML Serialiaziton** Generates IXmlSerializable interface implementation, to serialize and deserialize from/to xml.
* **EntityFrameworkCore ValueConverters** Facilitates seamless integration with EntityFrameworkCore by using ValueConverters to treat the primitive type as its underlying type. For more details, refer to [EntityFrameworkCore ValueConverters](EntityFrameworkCoreExample.md)

## Supported Underlying types 
1. `string`
2. `Guid`
3. `byte`
4. `sbyte`
5. `short` (Int16)
6. `ushort` (UInt16)
7. `int`
8. `uint`
9. `long`
10. `ulong`
11. `decimal`
12. `double`
13. `float`
14. `bool`
15. `char`
16. `TimeSpan`
17. `DateTime`
18. `DateTimeOffset`
19. `DateOnly`
20. `TimeOnly`

### Example Primitive Types

You can create domain primitive types for any supported underlying type. Here are some examples:

```csharp
// String-based primitives
public readonly partial struct EmailAddress : IDomainValue<string> { /* validation */ }
public readonly partial struct ProductCode : IDomainValue<string> { /* validation */ }

// Numeric primitives  
public readonly partial struct Age : IDomainValue<int> { /* validation */ }
public readonly partial struct Price : IDomainValue<decimal> { /* validation */ }
public readonly partial struct Weight : IDomainValue<double> { /* validation */ }
public readonly partial struct Score : IDomainValue<float> { /* validation */ }

// Date/Time primitives
public readonly partial struct BirthDate : IDomainValue<DateOnly> { /* validation */ }
public readonly partial struct BusinessHours : IDomainValue<TimeOnly> { /* validation */ }
public readonly partial struct CreatedAt : IDomainValue<DateTime> { /* validation */ }

// Identifier primitives
public readonly partial struct CustomerId : IDomainValue<Guid> { /* validation */ }
public readonly partial struct OrderNumber : IDomainValue<long> { /* validation */ }
```


## Getting Started

### Prerequisites
*	.NET 8 or higher
*	NuGet Package Manager

### Installation

To use **AltaSoft.DomainPrimitives**, install two NuGet packages:

1. `AltaSoft.DomainPrimitives`
2. `AltaSoft.DomainPrimitives.Generator`

In your project file add references as follows:

```xml
<ItemGroup>
  <PackageReference Include="AltaSoft.DomainPrimitives" Version="x.x.x" />
  <PackageReference Include="AltaSoft.DomainPrimitives.Generator" Version="x.x.x" PrivateAssets="all" />
</ItemGroup>
```

### Optional Packages

**For Swagger/Swashbuckle integration:**
```xml
<PackageReference Include="AltaSoft.DomainPrimitives.SwaggerExtensions" Version="x.x.x" />
```

**For OpenAPI integration (.NET 9+ minimal APIs):**
```xml
<PackageReference Include="AltaSoft.DomainPrimitives.OpenApiExtensions" Version="x.x.x" />
```

**For XML data types support:**
```xml
<PackageReference Include="AltaSoft.DomainPrimitives.XmlDataTypes" Version="x.x.x" />
```


## **Creating your Domain type**
For optimal performance, it is recommended to use `readonly struct`, especially when wrapping value types. If the type is a `reference` type, consider using `class` over `struct`.

```csharp
public readonly partial struct PositiveInteger : IDomainValue<int>
{
    /// <inheritdoc/>
    public static PrimitiveValidationResult Validate(int value)
    {
        if (value <= 0)
            return PrimitiveValidationResult.Error("value is non-positive");

        return PrimitiveValidationResult.Ok;
    }
}
```

This will automatically generate by default 4 classes
## **PositiveInteger.Generated**
```csharp
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by 'AltaSoft DomainPrimitives Generator'.
//     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#nullable enable

using System;
using System.Numerics;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using AltaSoft.DomainPrimitives;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using AltaSoft.DomainPrimitives.XmlDataTypes.Converters;
using System.ComponentModel;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace AltaSoft.DomainPrimitives.XmlDataTypes;

[JsonConverter(typeof(PositiveIntegerJsonConverter))]
[TypeConverter(typeof(PositiveIntegerTypeConverter))]
[UnderlyingPrimitiveType(typeof(int))]
[DebuggerDisplay("{_value}")]
public readonly partial struct PositiveInteger : IEquatable<PositiveInteger>
        , IComparable
        , IComparable<PositiveInteger>
        , IAdditionOperators<PositiveInteger, PositiveInteger, PositiveInteger>
        , ISubtractionOperators<PositiveInteger, PositiveInteger, PositiveInteger>
        , IMultiplyOperators<PositiveInteger, PositiveInteger, PositiveInteger>
        , IDivisionOperators<PositiveInteger, PositiveInteger, PositiveInteger>
        , IModulusOperators<PositiveInteger, PositiveInteger, PositiveInteger>
        , IComparisonOperators<PositiveInteger, PositiveInteger, bool>
        , IParsable<PositiveInteger>
        , IConvertible
        , IXmlSerializable
#if NET8_0_OR_GREATER
        , IUtf8SpanFormattable
#endif
{
    /// <inheritdoc/>
     public Type GetUnderlyingPrimitiveType() => typeof(int);
    /// <inheritdoc/>
     public object GetUnderlyingPrimitiveValue() => (int)this;

    private int _valueOrThrow => _isInitialized ? _value : throw new InvalidDomainValueException("The domain value has not been initialized", this);
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly int _value;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly bool _isInitialized;

    /// <summary>
    /// Initializes a new instance of the <see cref="PositiveInteger"/> class by validating the specified <see cref="int"/> value using <see cref="Validate"/> static method.
    /// </summary>
    /// <param name="value">The value to be validated.</param>
    public PositiveInteger(int value) : this(value, true)
    {
    }

    private PositiveInteger(int value, bool validate) 
    {
        if (validate)
        {
            ValidateOrThrow(value);
        }
        _value = value;
        _isInitialized = true;
    }

    /// <inheritdoc/>
    [Obsolete("Domain primitive cannot be created using empty Constructor", true)]
    public PositiveInteger()
    {
    }

    /// <summary>
    /// Tries to create an instance of AsciiString from the specified value.
    /// </summary>
    /// <param name="value">The value to create PositiveInteger from</param>
    /// <param name="result">When this method returns, contains the created PositiveInteger if the conversion succeeded, or null if the conversion failed.</param>
    /// <returns>true if the conversion succeeded; otherwise, false.</returns>
    public static bool TryCreate(int value, [NotNullWhen(true)] out PositiveInteger? result)
    {
        return TryCreate(value, out result, out _);
    }

    /// <summary>
    /// Tries to create an instance of AsciiString from the specified value.
    /// </summary>
    /// <param name="value">The value to create PositiveInteger from</param>
    /// <param name="result">When this method returns, contains the created PositiveInteger if the conversion succeeded, or null if the conversion failed.</param>
    /// <param name="errorMessage">When this method returns, contains the error message if the conversion failed; otherwise, null.</param>
    /// <returns>true if the conversion succeeded; otherwise, false.</returns>
    public static bool TryCreate(int value,[NotNullWhen(true)]  out PositiveInteger? result, [NotNullWhen(false)]  out string? errorMessage)
    {
        var validationResult = Validate(value);
        if (!validationResult.IsValid)
        {
            result = null;
            errorMessage = validationResult.ErrorMessage;
            return false;
        }

        result = new (value, false);
        errorMessage = null;
        return true;
    }

    /// <summary>
    ///  Validates the specified value and throws an exception if it is not valid.
    /// </summary>
    /// <param name="value">The value to validate</param>
    /// <exception cref="InvalidDomainValueException">Thrown when the value is not valid.</exception>
    public void ValidateOrThrow(int value)
    {
        var result = Validate(value);
        if (!result.IsValid)
        	throw new InvalidDomainValueException(result.ErrorMessage, this);
    }


    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj) => obj is PositiveInteger other && Equals(other);
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(PositiveInteger other)
    {
        if (!_isInitialized || !other._isInitialized)
            return false;
        return _value.Equals(other._value);
    }
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(PositiveInteger left, PositiveInteger right) => left.Equals(right);
    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(PositiveInteger left, PositiveInteger right) => !(left == right);

    /// <inheritdoc/>
    public int CompareTo(object? obj)
    {
        if (obj is null)
            return 1;

        if (obj is PositiveInteger c)
            return CompareTo(c);

        throw new ArgumentException("Object is not a PositiveInteger", nameof(obj));
    }

    /// <inheritdoc/>
    public int CompareTo(PositiveInteger other)
    {
        if (!other._isInitialized)
            return 1;
        if (!_isInitialized)
            return -1;
        return _value.CompareTo(other._value);
    }

    /// <summary>
    /// Implicit conversion from <see cref = "int"/> to <see cref = "PositiveInteger"/>
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator PositiveInteger(int value) => new(value);

    /// <summary>
    /// Implicit conversion from <see cref = "int"/> (nullable) to <see cref = "PositiveInteger"/> (nullable)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NotNullIfNotNull(nameof(value))]
    public static implicit operator PositiveInteger?(int? value) => value is null ? null : new(value.Value);

    /// <summary>
    /// Implicit conversion from <see cref = "PositiveInteger"/> to <see cref = "int"/>
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator int(PositiveInteger value) => (int)value._valueOrThrow;

    /// <summary>
    /// Implicit conversion from <see cref = "PositiveInteger"/> (nullable) to <see cref = "int"/> (nullable)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NotNullIfNotNull(nameof(value))]
    public static implicit operator int?(PositiveInteger? value) => value is null ? null : (int?)value.Value._valueOrThrow;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PositiveInteger operator +(PositiveInteger left, PositiveInteger right) => new(left._valueOrThrow + right._valueOrThrow);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PositiveInteger operator -(PositiveInteger left, PositiveInteger right) => new(left._valueOrThrow - right._valueOrThrow);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PositiveInteger operator *(PositiveInteger left, PositiveInteger right) => new(left._valueOrThrow * right._valueOrThrow);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PositiveInteger operator /(PositiveInteger left, PositiveInteger right) => new(left._valueOrThrow / right._valueOrThrow);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PositiveInteger operator %(PositiveInteger left, PositiveInteger right) => new(left._valueOrThrow % right._valueOrThrow);

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(PositiveInteger left, PositiveInteger right) => left._valueOrThrow < right._valueOrThrow;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(PositiveInteger left, PositiveInteger right) => left._valueOrThrow <= right._valueOrThrow;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(PositiveInteger left, PositiveInteger right) => left._valueOrThrow > right._valueOrThrow;

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(PositiveInteger left, PositiveInteger right) => left._valueOrThrow >= right._valueOrThrow;


    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PositiveInteger Parse(string s, IFormatProvider? provider) => int.Parse(s, provider);

    /// <inheritdoc/>
    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out PositiveInteger result)
    {
        if (!int.TryParse(s, provider, out var value))
        {
            result = default;
            return false;
        }

        if (TryCreate(value, out var created))
        {
            result = created.Value;
            return true;
        }

        result = default;
        return false;
    }

#if NET8_0_OR_GREATER
    /// <inheritdoc cref="IUtf8SpanFormattable.TryFormat"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryFormat(Span<byte> utf8Destination, out int bytesWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
    {
        return ((IUtf8SpanFormattable)_valueOrThrow).TryFormat(utf8Destination, out bytesWritten, format, provider);
    }
#endif

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode() => _valueOrThrow.GetHashCode();

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    TypeCode IConvertible.GetTypeCode() => ((IConvertible)(Int32)_valueOrThrow).GetTypeCode();

    /// <inheritdoc/>
    bool IConvertible.ToBoolean(IFormatProvider? provider) => ((IConvertible)(Int32)_valueOrThrow).ToBoolean(provider);

    /// <inheritdoc/>
    byte IConvertible.ToByte(IFormatProvider? provider) => ((IConvertible)(Int32)_valueOrThrow).ToByte(provider);

    /// <inheritdoc/>
    char IConvertible.ToChar(IFormatProvider? provider) => ((IConvertible)(Int32)_valueOrThrow).ToChar(provider);

    /// <inheritdoc/>
    DateTime IConvertible.ToDateTime(IFormatProvider? provider) => ((IConvertible)(Int32)_valueOrThrow).ToDateTime(provider);

    /// <inheritdoc/>
    decimal IConvertible.ToDecimal(IFormatProvider? provider) => ((IConvertible)(Int32)_valueOrThrow).ToDecimal(provider);

    /// <inheritdoc/>
    double IConvertible.ToDouble(IFormatProvider? provider) => ((IConvertible)(Int32)_valueOrThrow).ToDouble(provider);

    /// <inheritdoc/>
    short IConvertible.ToInt16(IFormatProvider? provider) => ((IConvertible)(Int32)_valueOrThrow).ToInt16(provider);

    /// <inheritdoc/>
    int IConvertible.ToInt32(IFormatProvider? provider) => ((IConvertible)(Int32)_valueOrThrow).ToInt32(provider);

    /// <inheritdoc/>
    long IConvertible.ToInt64(IFormatProvider? provider) => ((IConvertible)(Int32)_valueOrThrow).ToInt64(provider);

    /// <inheritdoc/>
    sbyte IConvertible.ToSByte(IFormatProvider? provider) => ((IConvertible)(Int32)_valueOrThrow).ToSByte(provider);

    /// <inheritdoc/>
    float IConvertible.ToSingle(IFormatProvider? provider) => ((IConvertible)(Int32)_valueOrThrow).ToSingle(provider);

    /// <inheritdoc/>
    string IConvertible.ToString(IFormatProvider? provider) => ((IConvertible)(Int32)_valueOrThrow).ToString(provider);

    /// <inheritdoc/>
    object IConvertible.ToType(Type conversionType, IFormatProvider? provider) => ((IConvertible)(Int32)_valueOrThrow).ToType(conversionType, provider);

    /// <inheritdoc/>
    ushort IConvertible.ToUInt16(IFormatProvider? provider) => ((IConvertible)(Int32)_valueOrThrow).ToUInt16(provider);

    /// <inheritdoc/>
    uint IConvertible.ToUInt32(IFormatProvider? provider) => ((IConvertible)(Int32)_valueOrThrow).ToUInt32(provider);

    /// <inheritdoc/>
    ulong IConvertible.ToUInt64(IFormatProvider? provider) => ((IConvertible)(Int32)_valueOrThrow).ToUInt64(provider);

    /// <inheritdoc/>
    public XmlSchema? GetSchema() => null;

    /// <inheritdoc/>
    public void ReadXml(XmlReader reader)
    {
        var value = reader.ReadElementContentAs<int>();
        ValidateOrThrow(value);
        System.Runtime.CompilerServices.Unsafe.AsRef(in _value) = value;
        System.Runtime.CompilerServices.Unsafe.AsRef(in _isInitialized) = true;
    }

    /// <inheritdoc/>
    public void WriteXml(XmlWriter writer) => writer.WriteValue(((int)_valueOrThrow).ToXmlString());

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString() => _valueOrThrow.ToString();
}

```
##  **JsonConverter**
```csharp
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by 'AltaSoft DomainPrimitives Generator'.
//     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#nullable enable

using AltaSoft.DomainPrimitives;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;
using System.Text.Json.Serialization.Metadata;


namespace AltaSoft.DomainPrimitives.Converters;

/// <summary>
/// JsonConverter for <see cref = "PositiveInteger"/>
/// </summary>
public sealed class PositiveIntegerJsonConverter : JsonConverter<PositiveInteger>
{
	/// <inheritdoc/>
	public override PositiveInteger Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		try
		{
			return JsonInternalConverters.Int32Converter.Read(ref reader, typeToConvert, options);
		}
		catch (InvalidDomainValueException ex)
		{
			throw new JsonException(ex.Message);
		}
	}

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, PositiveInteger value, JsonSerializerOptions options)
	{
		JsonInternalConverters.Int32Converter.Write(writer, (int)value, options);
	}

	/// <inheritdoc/>
	public override PositiveInteger ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		try
		{
			return JsonInternalConverters.Int32Converter.ReadAsPropertyName(ref reader, typeToConvert, options);
		}
		catch (InvalidDomainValueException ex)
		{
			throw new JsonException(ex.Message);
		}
	}

	/// <inheritdoc/>
	public override void WriteAsPropertyName(Utf8JsonWriter writer, PositiveInteger value, JsonSerializerOptions options)
	{
		JsonInternalConverters.Int32Converter.WriteAsPropertyName(writer, (int)value, options);
	}
}

```
## **Type Converter**
```csharp
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by 'AltaSoft DomainPrimitives Generator'.
//     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#nullable enable

using AltaSoft.DomainPrimitives;
using System;
using System.ComponentModel;
using System.Globalization;


namespace AltaSoft.DomainPrimitives.Converters;

/// <summary>
/// TypeConverter for <see cref = "PositiveInteger"/>
/// </summary>
public sealed class PositiveIntegerTypeConverter : Int32Converter
{
	/// <inheritdoc/>
	public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
	{
		var result = base.ConvertFrom(context, culture, value);
		if (result is null)
			return null;
		try
		{
			return new PositiveInteger((int)result);
		}
		catch (InvalidDomainValueException ex)
		{
			throw new FormatException("Cannot parse PositiveInteger", ex);
		}
	}
}
```
## **Swagger Mappers**

A single file for all domainPrimitives containing all type mappings is generated.
**Please note that you need to manually add Swashbuckle.AspNetCore.SwaggerGen nuget package to the project**

```csharp
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by 'AltaSoft DomainPrimitives Generator'.
//     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#nullable enable

using AltaSoft.DomainPrimitives;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;

namespace AltaSoft.DomainPrimitives.Converters.Extensions;

/// <summary>
/// Helper class providing methods to configure Swagger mappings for DomainPrimitive types of AltaSoft.DomainPrimitives
/// </summary>
public static class SwaggerTypeHelper
{
	/// <summary>
	/// Adds Swagger mappings for specific custom types to ensure proper OpenAPI documentation generation.
	/// </summary>
	/// <param name="options">The SwaggerGenOptions instance to which mappings are added.</param>
	/// <remarks>
	/// The method adds Swagger mappings for the following types:
	/// <see cref="PositiveInteger"/>
	/// </remarks>
	public static void AddSwaggerMappings(this SwaggerGenOptions options)
	{
		options.MapType<PositiveInteger>(() => new OpenApiSchema
		{
			Type = "integer",
			Format = "int32",
			Title = "PositiveInteger",
			Description = @"A domain primitive type representing a positive integer."
		});
		options.MapType<PositiveInteger?>(() => new OpenApiSchema
		{
			Type = "integer",
			Format = "int32",
			Nullable = true,
			Title = "Nullable<PositiveInteger>",
			Description = @"A domain primitive type representing a positive integer."
		});
	}
```

### OpenApiHelper

In addition to the Swagger mappings, the generator also creates an `OpenApiHelper` class with a dictionary-based approach for OpenAPI schema definitions:

```csharp
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by 'AltaSoft DomainPrimitives Generator'.
//     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#nullable enable

using AltaSoft.DomainPrimitives;
using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using Microsoft.OpenApi;

namespace AltaSoft.DomainPrimitives.Converters.Helpers;

/// <summary>
/// Helper class providing methods to configure OpenApiSchema mappings for DomainPrimitive types
/// </summary>
public static class OpenApiHelper
{
    /// <summary>
    /// Mapping of DomainPrimitive types to OpenApiSchema definitions.
    /// </summary>
    public static FrozenDictionary<Type, OpenApiSchema> Schemas = new Dictionary<Type, OpenApiSchema>()
    {
        {
            typeof(PositiveInteger),
            new OpenApiSchema
            {
                Type = JsonSchemaType.Integer,
                Format = "int32",
                Title = "PositiveInteger"
            }
        }
    }.ToFrozenDictionary();
}
```

This helper provides a frozen dictionary for efficient lookups and can be used for custom OpenAPI integration scenarios.

## OpenAPI Integration

For .NET 9+ applications using minimal APIs with OpenAPI support, the `AltaSoft.DomainPrimitives.OpenApiExtensions` package provides enhanced integration:

### Installation

```xml
<PackageReference Include="AltaSoft.DomainPrimitives.OpenApiExtensions" Version="x.x.x" />
```

### Usage with Minimal APIs

```csharp
var builder = WebApplication.CreateBuilder(args);

// Add OpenAPI services
builder.Services.AddOpenApi();

// Register domain primitive OpenAPI schema transformers
builder.Services.AddDomainPrimitiveOpenApiSchemaTransformers();

var app = builder.Build();

app.MapOpenApi();
app.Run();
```

The OpenAPI extensions automatically register schema transformers that ensure domain primitives are properly represented in your OpenAPI documentation, mapping them to their underlying primitive types while preserving nullable information.

## Specialized ToString method 
By Default IDomainValue uses its underlying type's ToString method however this can be overriden by implementing a method specified below

```csharp 
static virtual string ToString(T value) => value.ToString() ?? string.Empty;
```

## Managing Generated Operators for numeric types

Mathematical operators for particular numeric types can be customized using the `SupportedOperationsAttribute`. If left unspecified, all operators are generated by default (as shown below). Once this attribute is applied, manual specification of the operators becomes mandatory. Note that for `byte`, `sbyte`, `short`, and `ushort` types, mathematical operators will not be generated by default.

**Special Note for `byte`, `sbyte`, `short`, and `ushort` types:** When mathematical operators are enabled for these smaller integer types (via `SupportedOperationsAttribute`), an additional private constructor accepting an `int` parameter is automatically generated to handle overflow scenarios properly during mathematical operations.

### Default numeric types Generated Operators 
1. `byte, sbyte` => `None`
2. `short, ushort` => `None`
3. `int, uint` => `+ - / * %`
4. `long, ulong` => `+ - / * %`
5. `float` => `+ - / * %`
6. `double` => `+ - / * %`
7. `decimal` => `+ - / * %`

### using `SupportedOperationsAttribute`

```csharp
[SupportedOperations(Addition = false, Division = false, Modulus = false, Multiplication = true, Subtraction = true)]
public readonly partial struct PositiveInteger : IDomainValue<int>
{
	 /// <inheritdoc/>
    public static PrimitiveValidationResult Validate(int value)
    {
        if (value <= 0)
            return PrimitiveValidationResult.Error("value is non-positive");

        return PrimitiveValidationResult.Ok;
    }
}
```
### For further customization of the operators, consider implementing specific interfaces. This action will override the generated operators for the respective domain type:

```csharp
public readonly partial struct PositiveInteger :
	IDomainValue<int>,
	IAdditionOperators<PositiveInteger, PositiveInteger, PositiveInteger>
{
	 /// <inheritdoc/>
    public static PrimitiveValidationResult Validate(int value)
    {
        if (value <= 0)
            return PrimitiveValidationResult.Error("value is non-positive");

        return PrimitiveValidationResult.Ok;
    }
	
	// custom + operator
	public static PositiveInteger operator +(PositiveInteger left, PositiveInteger right)
	{
		return (left._value + right._value + 1);
	}
}
```

## Managing Serialization Format for date-related types

Certain date-related types like `DateTime`, `DateOnly`, `TimeOnly`, `DateTimeOffset`, and `TimeSpan` can modify their serialization/deserialization format using the `SerializationFormatAttribute`. 
For instance, consider the `GDay` type, which represents an XML gDay value. It implements the `IDomainValue<DateOnly>` interface and utilizes the `SerializationFormatAttribute` to specify a serialization format.
```csharp

/// <summary>
/// Represents an XML GDay value object, providing operations for parsing and handling gDay values.
/// </summary>
[SerializationFormat("dd")]
public readonly partial struct GDay : IDomainValue<DateOnly>
{
	 /// <inheritdoc/>
    public static PrimitiveValidationResult Validate(int value)
    {
        return PrimitiveValidationResult.Ok;
    }

	/// <inheritdoc/>
	public static DateOnly Default => default;

	// Customized string representation of DateOnly
	/// <inheritdoc/>
	public static string ToString(DateOnly value) => value.ToString("dd");
}
```

## Enhanced DateTime Interoperability

Domain primitives based on `DateOnly` and `TimeOnly` include additional implicit conversion operators for seamless interoperability with `DateTime`:

### DateOnly Domain Primitives
```csharp
public readonly partial struct MyDate : IDomainValue<DateOnly>
{
    public static PrimitiveValidationResult Validate(DateOnly value) => PrimitiveValidationResult.Ok;
}

// Usage examples:
var myDate = new MyDate(DateOnly.FromDateTime(DateTime.Now));

// Additional conversions available:
DateTime dateTime = myDate;        // Implicit conversion to DateTime
MyDate fromDateTime = DateTime.Now; // Implicit conversion from DateTime
```

### TimeOnly Domain Primitives  
```csharp
public readonly partial struct MyTime : IDomainValue<TimeOnly>
{
    public static PrimitiveValidationResult Validate(TimeOnly value) => PrimitiveValidationResult.Ok;
}

// Usage examples:
var myTime = new MyTime(TimeOnly.FromDateTime(DateTime.Now));

// Additional conversions available:
DateTime dateTime = myTime;        // Implicit conversion to DateTime  
MyTime fromDateTime = DateTime.Now; // Implicit conversion from DateTime
```

# Disable Generation of Converters and Operators

To disable the generation of Converters, Swagger Mappers, XML serialization, or operators, add the following properties to your .csproj file:

```xml
  <PropertyGroup>
    <!-- Disable specific converters -->
    <DomainPrimitiveGenerator_GenerateJsonConverters>false</DomainPrimitiveGenerator_GenerateJsonConverters>
    <DomainPrimitiveGenerator_GenerateTypeConverters>false</DomainPrimitiveGenerator_GenerateTypeConverters>
    <DomainPrimitiveGenerator_GenerateSwaggerConverters>false</DomainPrimitiveGenerator_GenerateSwaggerConverters>
    <DomainPrimitiveGenerator_GenerateXmlSerialization>false</DomainPrimitiveGenerator_GenerateXmlSerialization>
    
    <!-- Disable OpenAPI helper generation (default: true) -->
    <DomainPrimitiveGenerator_GenerateOpenApiHelper>false</DomainPrimitiveGenerator_GenerateOpenApiHelper>
    
    <!-- Disable implicit operators (default: true) -->
    <DomainPrimitiveGenerator_GenerateImplicitOperators>false</DomainPrimitiveGenerator_GenerateImplicitOperators>
    
    <!-- Disable numeric operators for numeric types (default: true) -->
    <DomainPrimitiveGenerator_GenerateNumericOperators>false</DomainPrimitiveGenerator_GenerateNumericOperators>
  </PropertyGroup>
```

:warning: Please note that `DomainPrimitiveGenerator_GenerateXmlSerialization` value by default is `false`.

# Additional Features 
1.  **PrimitiveValidationResult:** Offers an Ok result and a string containing the error message. It also includes an implicit operator to automatically convert a string to an error value.
 [PrimitiveValidationResult](src/AltaSoft.DomainPrimitives/PrimitiveValidationResult.cs) 


2. **AltaSoft.DomainPrimitives.StringLengthAttribute** can be used to specify minimum and maximum length restrictions on DomainPrimitives 
	```csharp
	[StringLength(minimumLength:1, maximumLength:100, validate:false)]
	public partial class AsciiString : IDomainValue<string>
	{
		/// <inheritdoc/>
		public static PrimitiveValidationResult Validate(string value)
		{
			var input = value.AsSpan();

			// ReSharper disable once ForCanBeConvertedToForeach
			for (var i = 0; i < input.Length; i++)
			{
				if (!char.IsAscii(input[i]))
					return "value contains non-ascii characters";
			}

			return PrimitiveValidationResult.Ok;
		}
	}
	```
	The Validate property can be used to automatically enforce boolean and string length validations within domain primitives.<br/>
	Additionally, this attribute can be utilized by **ORMs** to impose string length restrictions in the database.


3. **Chaining Primitive Types**

	* Chaining of primitive types is possible. For instance, considering the `PositiveInteger` and `BetweenOneAnd100` DomainPrimitives:

    ```csharp
    public readonly partial struct PositiveInteger : IDomainValue<int>
	{
	/// <inheritdoc/>
    public static PrimitiveValidationResult Validate(int value)
    {
        if (value <= 0)
            return PrimitiveValidationResult.Error("value is non-positive");

        return PrimitiveValidationResult.Ok;
    }
	}

    public readonly partial struct BetweenOneAnd100 : IDomainValue<PositiveInteger>
    {
		public static PrimitiveValidationResult Validate(PositiveInteger value)
		{
			if (value < 100)
				return "Value must be less than 100"; //implicit operator to convert string to PrimitiveValidationResult

			return PrimitiveValidationResult.Ok;		
		}
    }
    ```
4. 
	Defined type `BetweenOneAnd100` automatically inherits restrictions from PositiveInteger. Operators restricted in PositiveInteger are also inherited. Further restrictions on operators can be added using the `SupportedOperationsAttribute`:	
	
    ```csharp
	[SupportedOperations(Addition=false)]
	public readonly partial struct BetweenOneAnd100 : IDomainValue<PositiveInteger>
	{
		public static PrimitiveValidationResult Validate(PositiveInteger value)
		{
			if (value < 100)
				return "Value must be less than 100";

			return PrimitiveValidationResult.Ok;		
		}
	}
	```


# Restrictions 

1. **Implementation of IDomainValue Interface**
	* DomainPrimitives are mandated to implement the `IDomainValue<T>` interface to ensure adherence to domain-specific constraints and behaviors.

2. **Constructor Limitation**
	 * No constructors should be explicitly defined within DomainPrimitives. Doing so will result in a compiler error.

3. **Prohibition of Public Properties or Fields**
	* DomainPrimitive types should not contain any explicitly defined public properties or fields. The backing field will be automatically generated.
		* If any property or field is explicitly named `_value`, `_valueOrDefault`, or `_isInitialized`, a compiler error will be triggered.

# Examples 

## Implicit Usage of DomainType

```csharp
public readonly partial struct PositiveAmount : IDomainValue<decimal>
{
	public static PrimitiveValidationResult Validate(decimal value)
	{
		if (value <= 0m)
			return "Must be a a positive number";

		return PrimitiveValidationResult.Ok;			
	}

}

public static class Example
{
	public static void ImplicitConversion()
	{
		var amount = new PositiveAmount(100m);
		PositiveAmount amount2 = 100m; // implicitly converted to PositiveAmount

		//implicilty casted to decimal
		decimal amountInDecimal = amount + amount2;        
	}
}

```
# Json Conversion 

```csharp 
[SupportedOperations] // no mathematical operators should be generated
public readonly partial struct CustomerId : IDomainValue<int>
{
	public static PrimitiveValidationResult Validate(int value)
	{
		if (value <= 0)
			return "Must be a positive number";

		return PrimitiveValidationResult.Ok;		
	}
}

public sealed class Transaction
{
	public CustomerId FromId { get; set; }
	public CustomerId? ToId { get; set; }
	public PositiveAmount Amount { get; set; }
	public PositiveAmount? Fees { get; set; }
}

public static void JsonSerializationAndDeserialization()
{
	var amount = new Transaction()
        {
            Amount = 100.523m,
            Fees = null,
            FromId = 1,
            ToId = null
        };

    var jsonValue = JsonSerializer.Serialize(amount); //this will produce the same result as changing customerId to int and PositiveAmount to decimal
    var newValue = JsonSerializer.Deserialize<Transaction>(jsonValue)
}
```
`Serialized Json`
```json
{
    "FromId": 1,
    "ToId": null,
    "Amount": 100.523,
    "Fees": null
}
```


# Transform Method

In `AltaSoft.DomainPrimitives`, you can optionally define a static method named `Transform` inside your domain primitive to automatically preprocess input values before validation or instantiation.

## ✅ Signature
```csharp
public static T Transform(T value) 
```
- `T` must match the value type of your domain primitive (e.g., `string`, `int`, etc.).
- The method can be `private`, `internal`, or `public`.
- It **must be static** and **accept a single parameter** of type `T`.

## 🎯 When It's Invoked

If present, the `Transform` method is automatically called before:

- Running `Validate(value)`
- Invoking the constructor or `TryCreate(...)` method

This ensures the input is normalized consistently at the boundary of your domain object.

## 📌 Example: `ToUpperString`

```csharp
public sealed partial class ToUpperString : IDomainValue<string>
{
    static PrimitiveValidationResult Validate(string value) =>
        value.All(char.IsUpper) ? PrimitiveValidationResult.Ok : "Value must be all uppercase.";

    // This method is automatically invoked before validation and construction.
    static string Transform(string value) => value.ToUpperInvariant();
}
```

### What happens:

- A user provides `"hello"` to `TryCreate("hello", out var result, out var error)`
- `Transform("hello")` runs and returns `"HELLO"`
- `"HELLO"` is then validated with `Validate(...)`
---


# Contributions 
Contributions to AltaSoft.DomainPrimitives are welcome! Whether you have suggestions or wish to contribute code, feel free to submit a pull request or open an issue.

# Contact
For support, questions, or additional information, please visit GitHub Issues.

# License
This project is licensed under [MIT](LICENSE.TXT). See the LICENSE file for details.


