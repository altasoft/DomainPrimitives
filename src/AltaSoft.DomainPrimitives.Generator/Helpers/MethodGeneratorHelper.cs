using System.Collections.Generic;
using System.Linq;
using System.Text;
using AltaSoft.DomainPrimitives.Generator.Extensions;
using AltaSoft.DomainPrimitives.Generator.Models;
using Microsoft.CodeAnalysis;

namespace AltaSoft.DomainPrimitives.Generator.Helpers;

/// <summary>
/// A helper class providing methods for generating code related to Swagger, TypeConverter, JsonConverter, and other operations.
/// </summary>
internal static class MethodGeneratorHelper
{
    /// <summary>
    /// Represents the length of a new line character sequence.
    /// </summary>
    internal static readonly int s_newLineLength = GetNewLineLength();

    /// <summary>
    /// Retrieves the length of a new line character sequence.
    /// </summary>
    /// <returns>The length of a new line character sequence.</returns>
    private static int GetNewLineLength() => new StringBuilder().AppendLine().Length;

    /// <summary>
    /// Adds Swagger mappings for specific custom types to ensure proper OpenAPI documentation generation.
    /// </summary>
    /// <param name="assemblyName">The AssemblyName of the project.</param>
    /// <param name="types">A list of custom types to add Swagger mappings for.</param>
    /// <param name="context">The source production context.</param>
    internal static void AddSwaggerOptions(string assemblyName, List<GeneratorData> types, SourceProductionContext context)
    {
        if (types.Count == 0)
            return;

        var sb = new SourceCodeBuilder();
        sb.AddSourceHeader();

        var usings = types.ConvertAll(x => x.Namespace);
        usings.Add("Microsoft.Extensions.DependencyInjection");
        usings.Add("Swashbuckle.AspNetCore.SwaggerGen");
        usings.Add("Microsoft.OpenApi.Models");
        usings.Add("Microsoft.OpenApi.Any");
        sb.AppendUsings(usings);

        var ns = string.Join(".", assemblyName.Split('.').Select(s => char.IsDigit(s[0]) ? '_' + s : s));

        sb.AppendNamespace(ns + ".Converters.Extensions");

        sb.AppendSummary($"Helper class providing methods to configure Swagger mappings for DomainPrimitive types of {assemblyName}");

        sb.AppendClass("public static", "SwaggerTypeHelper");

        sb.AppendSummary("Adds Swagger mappings for specific custom types to ensure proper OpenAPI documentation generation.",
            "The SwaggerGenOptions instance to which mappings are added.", "options");

        sb.AppendLine("/// <remarks>");
        sb.AppendLine("/// The method adds Swagger mappings for the following types:");

        foreach (var data in types)
        {
            sb.Append("/// <see cref=\"").Append(data.ClassName).AppendLine("\" />");
        }
        sb.AppendLine("/// </remarks>");

        sb.AppendLine("public static void AddSwaggerMappings(this SwaggerGenOptions options)")
            .OpenBracket();

        foreach (var data in types)
        {
            var (typeName, format) = data.PrimitiveTypeSymbol.GetSwaggerTypeAndFormat();

            // Get the XML documentation comment for the namedTypeSymbol
            var xmlDocumentation = data.TypeSymbol.GetDocumentationCommentXml();

            AddMapping(false);
            if (data.TypeSymbol.IsValueType)
                AddMapping(true);

            continue;

            void AddMapping(bool isNullable)
            {
                sb.Append("options.MapType<").Append(data.ClassName);
                if (isNullable)
                    sb.Append("?");
                sb.Append(">(() => new OpenApiSchema")
                    .OpenBracket()

                    .Append("Type = ").Append(Quote(typeName)).AppendLine(",");
                if (!string.IsNullOrEmpty(format))
                    sb.Append("Format = ").Append(Quote(data.SerializationFormat ?? format)).AppendLine(",");
                if (isNullable)
                    sb.AppendLine("Nullable = true,");

                var title = isNullable ? $"Nullable<{data.ClassName}>" : data.ClassName;
                sb.Append("Title = ").Append(Quote(title)).AppendLine(",");

                var defaultValue = CreateDefaultValue(data.UnderlyingType, data.ClassName, data.SerializationFormat);

                if (defaultValue is not null)
                    sb.Append("Default = ").Append(defaultValue).AppendLine(",");

                if (!string.IsNullOrEmpty(xmlDocumentation))
                {
                    var xmlDoc = new System.Xml.XmlDocument();
                    xmlDoc.LoadXml(xmlDocumentation);

                    // Select the <summary> node
                    var summaryNode = xmlDoc.SelectSingleNode("member/summary");

                    if (summaryNode is not null)
                    {
                        sb.Append("Description = @").Append(Quote(summaryNode.InnerText.Trim())).AppendLine(",");
                    }

                    var example = xmlDoc.SelectSingleNode("member/example");
                    if (example is not null)
                    {
                        var exampleValue = example.InnerText.Trim().Replace("\"", "\\\"");
                        sb.Append("Example = new OpenApiString(").Append("\"" + exampleValue + "\"").AppendLine("),");
                    }
                }

                sb.Length -= s_newLineLength + 1;
                sb.NewLine();
                sb.AppendLine("});");
            }
        }

        sb.CloseBracket();
        sb.CloseBracket();

        context.AddSource("SwaggerTypeHelper.g.cs", sb.ToString());

        return;

        static string Quote(string? value) => '\"' + value?.Replace("\"", "\"\"") + '\"';

        static string? CreateDefaultValue(DomainPrimitiveUnderlyingType underlyingType, string className, string? serializationFormat)
        {
            return (underlyingType) switch
            {
                DomainPrimitiveUnderlyingType.String => $"new OpenApiString({className}.Default)",
                DomainPrimitiveUnderlyingType.Guid => $"new OpenApiString({className}.Default.ToString())",
                DomainPrimitiveUnderlyingType.Boolean => $"new OpenApiBoolean({className}.Default)",
                DomainPrimitiveUnderlyingType.SByte => $"new OpenApiByte((byte){className}.Default)",
                DomainPrimitiveUnderlyingType.Byte => $"new OpenApiByte({className}.Default)",
                DomainPrimitiveUnderlyingType.Int16 => $"new OpenApiInteger({className}.Default)",
                DomainPrimitiveUnderlyingType.UInt16 => $"new OpenApiInteger((short){className}.Default)",
                DomainPrimitiveUnderlyingType.Int32 => $"new OpenApiInteger({className}.Default)",
                DomainPrimitiveUnderlyingType.UInt32 => $"new OpenApiInteger((int){className}.Default)",
                DomainPrimitiveUnderlyingType.Int64 => $"new OpenApiLong({className}.Default)",
                DomainPrimitiveUnderlyingType.UInt64 => $"new OpenApiLong((long){className}.Default)",
                DomainPrimitiveUnderlyingType.Decimal => $"new OpenApiDouble(decimal.ToDouble({className}.Default))",
                DomainPrimitiveUnderlyingType.Single => $"new OpenApiFloat({className}.Default)",
                DomainPrimitiveUnderlyingType.Double => $"new OpenApiDouble({className}.Default)",
                DomainPrimitiveUnderlyingType.DateTime when serializationFormat is null => $"new OpenApiDateTime({className}.Default)",
                DomainPrimitiveUnderlyingType.DateTime => $"new OpenApiString({className}.Default.ToString(\"{serializationFormat}\", null))",
                DomainPrimitiveUnderlyingType.DateOnly => $"new OpenApiString({className}.Default.ToString(\"{serializationFormat ?? "YYYY-MM-DD"}\", null))",
                DomainPrimitiveUnderlyingType.TimeOnly => $"new OpenApiString({className}.Default.ToString(\"{serializationFormat ?? "hh:mm:ss"}\", null))",
                DomainPrimitiveUnderlyingType.TimeSpan => $"new OpenApiString({className}.Default.ToString(\"{serializationFormat ?? "hh:mm:ss"}\", null))",
                DomainPrimitiveUnderlyingType.DateTimeOffset when serializationFormat is null => $"new OpenApiDateTime(((DateTimeOffset){className}.Default).DateTime)",
                DomainPrimitiveUnderlyingType.DateTimeOffset => $"new OpenApiString({className}.Default.ToString(\"{serializationFormat}\", null))",
                DomainPrimitiveUnderlyingType.Char => $"new OpenApiString({className}.Default.ToString())",
                _ => null
            };
        }
    }

    /// <summary>
    /// Generates code for a TypeConverter for the specified type.
    /// </summary>
    /// <param name="data">The generator data containing type information.</param>
    /// <param name="context">The source production context.</param>
    internal static void ProcessTypeConverter(GeneratorData data, SourceProductionContext context)
    {
        var friendlyName = data.UnderlyingType.ToString();
        var sb = new SourceCodeBuilder();

        sb.AddSourceHeader();

        sb.AppendUsings(new[] {
            data.Namespace,
            "System",
            "System.ComponentModel",
            "System.Globalization",
            "AltaSoft.DomainPrimitives"
        });

        sb.AppendNamespace(data.Namespace + ".Converters");
        sb.AppendSummary($"TypeConverter for <see cref = \"{data.ClassName}\"/>");
        sb.AppendClass("public sealed", data.ClassName + "TypeConverter", $" {friendlyName}Converter");
        sb.AppendInheritDoc()
            .AppendLine("public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)")
            .OpenBracket();

        if (data.SerializationFormat is not null)
        {
            sb.AppendLine("if (value is string s)")
                .OpenBracket()
                .AppendLine("try")
                .OpenBracket()
                .Append("return ").Append(data.ClassName).AppendLine(".Parse(s, culture);")
                .CloseBracket()
                .AppendLine("catch (InvalidDomainValueException ex)")
                .OpenBracket()
                .Append("throw new FormatException(\"Cannot parse ").AppendLine("\", ex);")
                .CloseBracket()
                .CloseBracket()
                .NewLine()
                .AppendLine("return base.ConvertFrom(context, culture, value);");
        }
        else
        {
            sb.AppendLine("var result = base.ConvertFrom(context, culture, value);")
            .AppendLine("if (result is null)")
            .AppendLine("\treturn null;")
            .AppendLine("try")
            .OpenBracket()
            .AppendLine($"return new {data.ClassName}(({data.PrimitiveTypeFriendlyName})result);")
            .CloseBracket()
            .AppendLine("catch (InvalidDomainValueException ex)")
            .OpenBracket()
            .Append("throw new FormatException(\"Cannot parse ").Append(data.ClassName).AppendLine("\", ex);")
            .CloseBracket();
        }
        sb.CloseBracket().CloseBracket();

        context.AddSource($"{data.ClassName}TypeConverter.g.cs", sb.ToString());
    }

    /// <summary>
    /// Generates code for a JsonConverter for the specified type.
    /// </summary>
    /// <param name="data">The generator data containing type information.</param>
    /// <param name="context">The source production context.</param>
    internal static void ProcessJsonConverter(GeneratorData data, SourceProductionContext context)
    {
        var sb = new SourceCodeBuilder();

        sb.AddSourceHeader();

        var usingStatements =
            new List<string>
                {
                    data.Namespace,
                    "System",
                    "System.Text.Json",
                    "System.Text.Json.Serialization",
                    "System.Globalization",
                    "System.Text.Json.Serialization.Metadata",
                    "AltaSoft.DomainPrimitives",
                };

        var converterName = data.UnderlyingType.ToString();
        var primitiveTypeIsValueType = data.PrimitiveTypeSymbol.IsValueType;
        sb.AppendUsings(usingStatements);

        sb.AppendNamespace(data.Namespace + ".Converters");
        sb.AppendSummary($"JsonConverter for <see cref = \"{data.ClassName}\"/>");
        sb.AppendClass("public sealed", data.ClassName + "JsonConverter", $" JsonConverter<{data.ClassName}>");

        sb.AppendInheritDoc()
            .Append("public override ").Append(data.ClassName).AppendLine(" Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)")
            .OpenBracket();
        if (data.SerializationFormat is null)
        {
            sb.AppendLine("try")
                .OpenBracket()
                .AppendLine($"return JsonInternalConverters.{converterName}Converter.Read(ref reader, typeToConvert, options){(primitiveTypeIsValueType ? "" : "!")};")
                .CloseBracket();
        }
        else
        {
            sb.AppendLine("if (reader.TokenType != JsonTokenType.String)")
                .Append("\tthrow new JsonException(\"Expected a string value to deserialize ").Append(data.ClassName).AppendLine("\");")
                .NewLine()
                .Append("var str = reader.GetString() ?? throw new JsonException(\"Expected a non-null string value to deserialize ").Append(data.ClassName).AppendLine("\");")
                .AppendLine("try")
                .OpenBracket()
                .Append("return ").Append(data.ClassName).AppendLine(".Parse(str, CultureInfo.InvariantCulture);")
                .CloseBracket();
        }

        sb.AppendLine("catch (InvalidDomainValueException ex)")
            .OpenBracket()
            .AppendLine("throw new JsonException(ex.Message);")
            .CloseBracket().CloseBracket()
        .NewLine();

        sb.AppendInheritDoc().AppendLine($"public override void Write(Utf8JsonWriter writer, {data.ClassName} value, JsonSerializerOptions options)")
            .OpenBracket()
            .AppendLineIf(data.SerializationFormat is null, $"JsonInternalConverters.{converterName}Converter.Write(writer, ({data.PrimitiveTypeFriendlyName})value, options);")
            .AppendLineIf(data.SerializationFormat is not null, $"writer.WriteStringValue(value.ToString(\"{data.SerializationFormat}\", CultureInfo.InvariantCulture));")
            .CloseBracket()
            .NewLine();

        sb.AppendInheritDoc()
            .Append("public override ").Append(data.ClassName).AppendLine(" ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)")
            .OpenBracket();

        if (data.SerializationFormat is null)
        {
            sb.AppendLine("try")
                .OpenBracket()
                .AppendLine($"return JsonInternalConverters.{converterName}Converter.ReadAsPropertyName(ref reader, typeToConvert, options){(primitiveTypeIsValueType ? "" : "!")};")
                .CloseBracket();
        }
        else
        {
            sb.AppendLine("if (reader.TokenType != JsonTokenType.String)")
                .Append("\tthrow new JsonException(\"Expected a string value to deserialize ").Append(data.ClassName).AppendLine("\");")
                .NewLine()
                .Append("var str = reader.GetString() ?? throw new JsonException(\"Expected a non-null string value to deserialize ").Append(data.ClassName).AppendLine("\");")
                .AppendLine("try")
                .OpenBracket()
                .Append("return ").Append(data.ClassName).AppendLine(".Parse(str, CultureInfo.InvariantCulture);")
                .CloseBracket();
        }

        sb.AppendLine("catch (InvalidDomainValueException ex)")
            .OpenBracket()
            .AppendLine("throw new JsonException(ex.Message);")
            .CloseBracket()
            .CloseBracket()
            .NewLine();

        sb.AppendInheritDoc()
            .Append("public override void WriteAsPropertyName(Utf8JsonWriter writer, ").Append(data.ClassName).AppendLine(" value, JsonSerializerOptions options)")
            .OpenBracket()
                .AppendLineIf(data.SerializationFormat is null, $"JsonInternalConverters.{converterName}Converter.WriteAsPropertyName(writer, ({data.PrimitiveTypeFriendlyName})value, options);")
            .AppendLineIf(data.SerializationFormat is not null, $"writer.WritePropertyName(value.ToString(\"{data.SerializationFormat}\", CultureInfo.InvariantCulture));")
            .CloseBracket();

        sb.CloseBracket();

        context.AddSource($"{data.ClassName}JsonConverter.g.cs", sb.ToString());
    }

    /// <summary>
    /// Generates IConvertible interface methods for the specified type.
    /// </summary>
    /// <param name="data">The generator data containing type information.</param>
    /// <param name="sb">The source code builder.</param>
    internal static void GenerateConvertibles(GeneratorData data, SourceCodeBuilder sb)
    {
        var fieldName = $"({data.UnderlyingType}){data.FieldName}";

        if (data.UnderlyingType is DomainPrimitiveUnderlyingType.DateOnly or DomainPrimitiveUnderlyingType.TimeOnly)
            fieldName = '(' + fieldName + ").ToDateTime()";

        sb.AppendInheritDoc();
        sb.AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]");
        sb.Append("TypeCode IConvertible.GetTypeCode()")
            .AppendLine($" => ((IConvertible){fieldName}).GetTypeCode();")
            .NewLine();

        sb.AppendInheritDoc();
        sb.Append("bool IConvertible.ToBoolean(IFormatProvider? provider)")
            .AppendLine($" => ((IConvertible){fieldName}).ToBoolean(provider);")
            .NewLine();

        sb.AppendInheritDoc();
        sb.Append("byte IConvertible.ToByte(IFormatProvider? provider)")
            .AppendLine($" => ((IConvertible){fieldName}).ToByte(provider);")
            .NewLine();

        sb.AppendInheritDoc();
        sb.Append("char IConvertible.ToChar(IFormatProvider? provider)")
            .AppendLine($" => ((IConvertible){fieldName}).ToChar(provider);")
            .NewLine();

        sb.AppendInheritDoc();
        sb.Append("DateTime IConvertible.ToDateTime(IFormatProvider? provider)")
            .AppendLine($" => ((IConvertible){fieldName}).ToDateTime(provider);")
            .NewLine();

        sb.AppendInheritDoc();
        sb.Append("decimal IConvertible.ToDecimal(IFormatProvider? provider)")
            .AppendLine($" => ((IConvertible){fieldName}).ToDecimal(provider);")
            .NewLine();

        sb.AppendInheritDoc();
        sb.Append("double IConvertible.ToDouble(IFormatProvider? provider)")
            .AppendLine($" => ((IConvertible){fieldName}).ToDouble(provider);")
            .NewLine();

        sb.AppendInheritDoc();
        sb.Append("short IConvertible.ToInt16(IFormatProvider? provider)")
            .AppendLine($" => ((IConvertible){fieldName}).ToInt16(provider);")
            .NewLine();

        sb.AppendInheritDoc();
        sb.Append("int IConvertible.ToInt32(IFormatProvider? provider)")
            .AppendLine($" => ((IConvertible){fieldName}).ToInt32(provider);")
            .NewLine();

        sb.AppendInheritDoc();
        sb.Append("long IConvertible.ToInt64(IFormatProvider? provider)")
            .AppendLine($" => ((IConvertible){fieldName}).ToInt64(provider);")
            .NewLine();

        sb.AppendInheritDoc();
        sb.Append("sbyte IConvertible.ToSByte(IFormatProvider? provider)")
            .AppendLine($" => ((IConvertible){fieldName}).ToSByte(provider);")
            .NewLine();

        sb.AppendInheritDoc();
        sb.Append("float IConvertible.ToSingle(IFormatProvider? provider)")
            .AppendLine($" => ((IConvertible){fieldName}).ToSingle(provider);")
            .NewLine();

        sb.AppendInheritDoc();
        sb.Append("string IConvertible.ToString(IFormatProvider? provider)")
            .AppendLine($" => ((IConvertible){fieldName}).ToString(provider);")
            .NewLine();

        sb.AppendInheritDoc();
        sb.Append("object IConvertible.ToType(Type conversionType, IFormatProvider? provider)")
            .AppendLine($" => ((IConvertible){fieldName}).ToType(conversionType, provider);")
            .NewLine();

        sb.AppendInheritDoc();
        sb.Append("ushort IConvertible.ToUInt16(IFormatProvider? provider)")
            .AppendLine($" => ((IConvertible){fieldName}).ToUInt16(provider);")
            .NewLine();

        sb.AppendInheritDoc();
        sb.Append("uint IConvertible.ToUInt32(IFormatProvider? provider)")
            .AppendLine($" => ((IConvertible){fieldName}).ToUInt32(provider);")
            .NewLine();

        sb.AppendInheritDoc();
        sb.Append("ulong IConvertible.ToUInt64(IFormatProvider? provider)")
            .AppendLine($" => ((IConvertible){fieldName}).ToUInt64(provider);")
            .NewLine();
    }

    /// <summary>
    /// Generates an addition operator for the specified type.
    /// </summary>
    /// <param name="className">The name of the class.</param>
    /// <param name="fieldName">The name of the field to perform addition on.</param>
    /// <param name="sb">The source code builder.</param>
    internal static void GenerateAdditionCode(string className, string fieldName, SourceCodeBuilder sb)
    {
        sb.AppendInheritDoc()
            .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
            .Append($"public static {className} operator +({className} left, {className} right)")
            .AppendLine($" => new(left.{fieldName} + right.{fieldName});");
    }

    /// <summary>
    /// Generates code for implementing the IComparable interface for the specified type.
    /// </summary>
    /// <param name="className">The name of the class.</param>
    /// <param name="fieldName">The name of the field to compare.</param>
    /// <param name="isValueType">A flag indicating if the type is a value type.</param>
    /// <param name="sb">The source code builder.</param>
    internal static void GenerateComparableCode(string className, string fieldName, bool isValueType, SourceCodeBuilder sb)
    {
        sb.AppendInheritDoc()
            .AppendLine("public int CompareTo(object? value)")
            .OpenBracket()
            .AppendLine("if (value is null)")
            .AppendLine("\treturn 1;").NewLine()
            .AppendLine($"if (value is {className} c)")
            .AppendLine("\treturn CompareTo(c);").NewLine()
            .AppendLine($"throw new ArgumentException(\"Object is not a {className}\", nameof(value));")
            .CloseBracket();

        var nullable = isValueType ? "" : "?";
        sb.NewLine().AppendInheritDoc().AppendLine($"public int CompareTo({className}{nullable} other) => {fieldName}.CompareTo(other{nullable}.{fieldName});");
    }

    /// <summary>
    /// Generates comparison operators (&lt;, &lt;=, &gt;, &gt;=) for the specified type.
    /// </summary>
    /// <param name="className">The name of the class.</param>
    /// <param name="fieldName">The name of the field to compare.</param>
    /// <param name="sb">The source code builder.</param>
    internal static void GenerateComparisonCode(string className, string fieldName, SourceCodeBuilder sb)
    {
        sb.AppendInheritDoc()
            .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
            .Append($"public static bool operator <({className} left, {className} right)")
            .AppendLine($" => left.{fieldName} < right.{fieldName};")
            .NewLine();

        sb.AppendInheritDoc()
            .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
            .Append($"public static bool operator <=({className} left, {className} right)")
            .AppendLine($" => left.{fieldName} <= right.{fieldName};")
            .NewLine();

        sb.AppendInheritDoc()
            .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
            .Append($"public static bool operator >({className} left, {className} right)")
            .AppendLine($" => left.{fieldName} > right.{fieldName};")
            .NewLine();

        sb.AppendInheritDoc()
            .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
            .Append($"public static bool operator >=({className} left, {className} right)")
            .AppendLine($" => left.{fieldName} >= right.{fieldName};")
            .NewLine();
    }

    /// <summary>
    /// Generates a division operator for the specified type.
    /// </summary>
    /// <param name="className">The name of the class.</param>
    /// <param name="fieldName">The name of the field to perform division on.</param>
    /// <param name="sb">The source code builder.</param>
    public static void GenerateDivisionCode(string className, string fieldName, SourceCodeBuilder sb)
    {
        sb.AppendInheritDoc()
            .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
            .Append($"public static {className} operator /({className} left, {className} right)")
            .AppendLine($" => new(left.{fieldName} / right.{fieldName});");
    }

    /// <summary>
    /// Generates a multiplication operator for the specified type.
    /// </summary>
    /// <param name="className">The name of the class.</param>
    /// <param name="fieldName">The name of the field to perform multiplication on.</param>
    /// <param name="sb">The source code builder.</param>
    public static void GenerateMultiplyCode(string className, string fieldName, SourceCodeBuilder sb)
    {
        sb.AppendInheritDoc()
            .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
            .Append($"public static {className} operator *({className} left, {className} right)")
            .AppendLine($" => new(left.{fieldName} * right.{fieldName});");
    }

    /// <summary>
    /// Generates a subtraction operator for the specified type.
    /// </summary>
    /// <param name="className">The name of the class.</param>
    /// <param name="fieldName">The name of the field to perform subtraction on.</param>
    /// <param name="sb">The source code builder.</param>
    public static void GenerateSubtractionCode(string className, string fieldName, SourceCodeBuilder sb)
    {
        sb.AppendInheritDoc()
            .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
            .Append($"public static {className} operator -({className} left, {className} right)")
            .AppendLine($" => new(left.{fieldName} - right.{fieldName});");
    }

    /// <summary>
    /// Generates a modulus operator for the specified type.
    /// </summary>
    /// <param name="className">The name of the class.</param>
    /// <param name="fieldName">The name of the field to perform modulus on.</param>
    /// <param name="sb">The source code builder.</param>
    public static void GenerateModulusCode(string className, string fieldName, SourceCodeBuilder sb)
    {
        sb.AppendInheritDoc()
            .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
            .Append($"public static {className} operator %({className} left, {className} right)")
            .AppendLine($" => new(left.{fieldName} % right.{fieldName});");
    }

    /// <summary>
    /// Generates methods required for implementing ISpanFormattable for the specified type.
    /// </summary>
    /// <param name="sb">The source code builder.</param>
    /// <param name="fieldName">The name of the field.</param>
    public static void GenerateSpanFormattable(SourceCodeBuilder sb, string fieldName)
    {
        sb.AppendInheritDoc()
            .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
            .AppendLine($"public string ToString(string? format, IFormatProvider? formatProvider) => {fieldName}.ToString(format, formatProvider);")
            .NewLine();

        sb.AppendInheritDoc()
            .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
            .AppendLine("public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)")
            .OpenBracket()
            .Append("return ((ISpanFormattable)").Append(fieldName).AppendLine(").TryFormat(destination, out charsWritten, format, provider);")
            .CloseBracket()
            .NewLine();
    }

    /// <summary>
    /// Generates a method for formatting to UTF-8 if the condition NET8_0_OR_GREATER is met.
    /// </summary>
    /// <param name="sb">The SourceCodeBuilder to append the generated code.</param>
    /// <param name="fieldName">The name of the field.</param>
    internal static void GenerateUtf8Formattable(SourceCodeBuilder sb, string fieldName)
    {
        sb.AppendPreProcessorDirective("if NET8_0_OR_GREATER")
            .AppendInheritDoc("IUtf8SpanFormattable.TryFormat")
            .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
            .AppendLine("public bool TryFormat(Span<byte> utf8Destination, out int bytesWritten, ReadOnlySpan<char> format, IFormatProvider? provider)")
            .OpenBracket()
            .Append("return ((IUtf8SpanFormattable)").Append(fieldName).AppendLine(").TryFormat(utf8Destination, out bytesWritten, format, provider);")
            .CloseBracket()
            .AppendPreProcessorDirective("endif");
    }

    /// <summary>
    /// Generates Parse and TryParse methods for the specified type.
    /// </summary>
    /// <param name="data">The <see cref="GeneratorData"/> object containing information about the data type.</param>
    /// <param name="sb">The <see cref="SourceCodeBuilder"/> used to build the source code.</param>
    /// <remarks>
    /// This method generates parsing methods based on the provided data type and serialization format.
    /// </remarks>
    public static void GenerateParsable(GeneratorData data, SourceCodeBuilder sb)
    {
        var dataClassName = data.ClassName;
        var underlyingType = data.ParentSymbols.Count == 0
            ? data.PrimitiveTypeFriendlyName
            : data.ParentSymbols[0].Name;
        var format = data.SerializationFormat;

        sb.AppendInheritDoc()
            .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
            .Append($"public static {dataClassName} Parse(string s, IFormatProvider? provider) => ");

        var isString = data.ParentSymbols.Count == 0 && data.UnderlyingType is DomainPrimitiveUnderlyingType.String;
        var isChar = data.ParentSymbols.Count == 0 && data.UnderlyingType is DomainPrimitiveUnderlyingType.Char;
        var isBool = data.ParentSymbols.Count == 0 && data.UnderlyingType is DomainPrimitiveUnderlyingType.Boolean;

        if (isString)
        {
            sb.AppendLine("s;");
        }
        else
        if (isChar)
        {
            sb.AppendLine("char.Parse(s);");
        }
        else
        if (isBool)
        {
            sb.AppendLine("bool.Parse(s);");
        }
        else
        {
            sb.Append($"{underlyingType}.")
                .AppendLineIfElse(format is null, "Parse(s, provider);", $"ParseExact(s, \"{format}\", provider);");
        }

        sb.NewLine();

        sb.AppendInheritDoc()
            .AppendLine($"public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out {dataClassName} result)")
            .OpenBracket();

        if (isString)
        {
            sb.AppendLine("if (s is null)");
        }
        else
        if (isChar)
        {
            sb.AppendLine("if (!char.TryParse(s, out var value))");
        }
        else
        if (isBool)
        {
            sb.AppendLine("if (!bool.TryParse(s, out var value))");
        }
        else
        {
            sb.AppendIf(format is null, $"if (!{underlyingType}.TryParse(s, provider, out var value))")
                .AppendIf(format is not null, $"if (!{underlyingType}.TryParseExact(s, \"{format}\", out var value))");
        }

        sb.OpenBracket()
        .AppendLine("result = default;")
        .AppendLine("return false;")
        .CloseBracket()
        .NewLine()
        .AppendLine("try")
        .OpenBracket()
        .AppendLine($"result = new {dataClassName}({(isString ? "s" : "value")});")
        .AppendLine("return true;")
        .CloseBracket()
        .AppendLine("catch (Exception)")
        .OpenBracket()
        .AppendLine("result = default;")
        .AppendLine("return false;")
        .CloseBracket()
        .CloseBracket()
        .NewLine();
    }

    /// <summary>
    /// Generates equality and inequality operators for the specified type.
    /// </summary>
    /// <param name="className">The name of the class.</param>
    /// <param name="fieldName">The name of the field to compare.</param>
    /// <param name="isValueType">A flag indicating if the type is a value type.</param>
    /// <param name="sb">The source code builder.</param>
    public static void GenerateEquatableOperators(string className, string fieldName, bool isValueType, SourceCodeBuilder sb)
    {
        sb.AppendInheritDoc()
            .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
            .AppendLine($"public override bool Equals(object? obj) => obj is {className} other && Equals(other);");

        var nullable = isValueType ? "" : "?";

        sb.AppendInheritDoc()
            .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
            .AppendLine($"public bool Equals({className}{nullable} other) => {fieldName} == other{nullable}.{fieldName};");

        sb.AppendInheritDoc()
            .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
            .AppendLine($"public static bool operator ==({className} left, {className} right) => left.Equals(right);");

        sb.AppendInheritDoc()
            .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
            .AppendLine($"public static bool operator !=({className} left, {className} right) => !(left == right);");
    }

    /// <summary>
    /// Generates the necessary methods for implementing the IXmlSerializable interface.
    /// </summary>
    /// <param name="data">The generator data.</param>
    /// <param name="sb">The source code builder.</param>
    public static void GenerateIXmlSerializableMethods(GeneratorData data, SourceCodeBuilder sb)
    {
        sb.AppendInheritDoc();
        sb.AppendLine("public XmlSchema? GetSchema() => null;")
            .NewLine();

        //var typeName = CapitalizeFirstLetter(data.PrimitiveTypeFriendlyName);

        var method = data.PrimitiveTypeFriendlyName switch
        {
            "string" => "ReadElementContentAsString",
            "bool" => "ReadElementContentAsBoolean",
            _ => $"ReadElementContentAs<{data.PrimitiveTypeFriendlyName}>"
        };

        sb.AppendInheritDoc();
        sb.AppendLine("public void ReadXml(XmlReader reader)")
            .OpenBracket()
            .Append("System.Runtime.CompilerServices.Unsafe.AsRef(in _value) = reader.").Append(method).AppendLine("();")
            .AppendLineIf(data.GenerateIsInitializedField, "System.Runtime.CompilerServices.Unsafe.AsRef(in _isInitialized) = true;")
            .CloseBracket()
            .NewLine();

        sb.AppendInheritDoc();

        if (data.PrimitiveTypeFriendlyName == "string")
            sb.AppendLine($"public void WriteXml(XmlWriter writer) => writer.WriteString({data.FieldName});");
        else
        if (data.SerializationFormat is null)
            sb.AppendLine($"public void WriteXml(XmlWriter writer) => writer.WriteValue((({data.PrimitiveTypeFriendlyName}){data.FieldName}).ToXmlString());");
        else
            sb.AppendLine($"public void WriteXml(XmlWriter writer) => writer.WriteString({data.FieldName}.ToString(\"{data.SerializationFormat}\"));");
        sb.NewLine();
    }
}
