using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AltaSoft.DomainPrimitives.Generator.Extensions;
using AltaSoft.DomainPrimitives.Generator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace AltaSoft.DomainPrimitives.Generator.Helpers;

/// <summary>
/// A helper class providing methods for generating code related to OpenApi mapping, TypeConverter, JsonConverter, and other operations.
/// </summary>
internal static class MethodGeneratorHelper
{
    /// <summary>
    /// Adds OpenApiSchema mappings for specific custom types to ensure proper OpenAPI documentation generation.
    /// </summary>
    /// <param name="assemblyName">The AssemblyName of the project.</param>
    /// <param name="types">A list of custom types to add OpenApiSchema mappings for.</param>
    /// <param name="context">The source production context.</param>
    internal static void AddOpenApiSchemas(string assemblyName, List<GeneratorData> types, SourceProductionContext context)
    {
        if (types.Count == 0)
            return;

        var builder = new SourceCodeBuilder();
        builder.AppendSourceHeader("AltaSoft DomainPrimitives Generator");

        builder.AppendLine("#if NET10_0_OR_GREATER", false)
            .AppendLine("using Microsoft.OpenApi;", false)
            .AppendLine("#else", false)
            .AppendLine("using Microsoft.OpenApi.Models;", false)
            .AppendLine("using Microsoft.OpenApi.Any;", false)
            .AppendLine("#endif");

        var usings = types.ConvertAll(x => x.Namespace);
        usings.Add("System");
        usings.Add("System.Collections.Frozen");
        usings.Add("System.Collections.Generic");
        usings.Add("System.Text.Json.Nodes");
        usings.Add("AltaSoft.DomainPrimitives");
        builder.AppendUsings(usings);

        builder.AppendLine("[assembly: AltaSoft.DomainPrimitives.DomainPrimitiveAssemblyAttribute]");

        var ns = string.Join(".", assemblyName.Split('.').Select(s => char.IsDigit(s[0]) ? '_' + s : s));

        builder.AppendNamespace(ns + ".Converters.Helpers");

        builder.AppendSummary($"Helper class providing methods to configure OpenApiSchema mappings for DomainPrimitive types of {assemblyName}");

        builder.AppendClass(false, "public static", "OpenApiHelper");

        builder.AppendSummary("Mapping of DomainPrimitive types to OpenApiSchema definitions.");

        builder.AppendLine("/// <remarks>");
        builder.AppendLine("/// The Dictionary contains mappings for the following types:");

        foreach (var data in types)
        {
            builder.AppendLine("/// <para>");
            builder.Append("/// <see cref=\"").Append(data.ClassName).AppendLine("\" />");
            builder.AppendLine("/// </para>");
        }
        builder.AppendLine("/// </remarks>");

        builder.NewLine().AppendLine("#if NET10_0_OR_GREATER", false);
        builder.AppendLine("public static FrozenDictionary<Type, OpenApiSchema> Schemas = new Dictionary<Type, OpenApiSchema>()")
            .OpenBracket();

        foreach (var data in types)
        {
            var (typeName, format) = data.PrimitiveTypeSymbol.GetOpenApiTypeAndFormat();

            // Get the XML documentation comment for the namedTypeSymbol
            var xmlDocumentation = data.TypeSymbol.GetDocumentationCommentXml(cancellationToken: context.CancellationToken);

            builder.OpenBracket();
            AddMapping();
            builder.CloseBracketWithComma();

            continue;

            void AddMapping()
            {
                builder.Append("typeof(").Append(data.ClassName).AppendLine("),");
                builder.Append("new OpenApiSchema")
                    .OpenBracket()

                    .Append("Type = ").Append(typeName).AppendLine(",");

                if (!string.IsNullOrEmpty(format))
                    builder.Append("Format = ").Append(QuoteAndEscape(data.SerializationFormat ?? format)).AppendLine(",");

                var title = data.ClassName;
                builder.Append("Title = ").Append(Quote(title)).AppendLine(",");

                if (!string.IsNullOrEmpty(xmlDocumentation))
                {
                    var xmlDoc = new System.Xml.XmlDocument();
                    xmlDoc.LoadXml(xmlDocumentation);

                    // Select the <summary> node
                    var summaryNode = xmlDoc.SelectSingleNode("member/summary");

                    if (summaryNode is not null)
                    {
                        builder.Append("Description = @").Append(Quote(summaryNode.InnerText.Trim())).AppendLine(",");
                    }

                    var example = xmlDoc.SelectSingleNode("member/example");
                    if (example is not null)
                    {
                        var exampleValue = example.InnerText.Trim().Replace("\"", "\\\"");
                        if (!data.UnderlyingType.IsNumeric())
                            exampleValue = Quote(exampleValue);

                        builder.Append("Example = JsonValue.Create(").Append(exampleValue).AppendLine("),");
                    }
                }

                builder.Length -= SourceCodeBuilder.s_newLineLength + 1;
                builder.NewLine();
                builder.AppendLine("}");
            }
        }

        builder.Rollback(builder.GetNewLineLength() + 1).NewLine();
        builder.CloseBracketWithString(".ToFrozenDictionary();");

        builder.NewLine();
        builder.AppendLine("#else", false);

        builder.AppendLine("public static FrozenDictionary<Type, OpenApiSchema> Schemas = new Dictionary<Type, OpenApiSchema>()")
            .OpenBracket();

        ProcessOldVersionOpenApi(types, builder, context.CancellationToken);

        builder.Rollback(builder.GetNewLineLength() + 1).NewLine();
        builder.CloseBracketWithString(".ToFrozenDictionary();");
        builder.AppendLine("#endif", false);

        builder.CloseBracket();
        context.AddSource("OpenApiHelper.g.cs", builder.ToString());

        return;

        static void ProcessOldVersionOpenApi(List<GeneratorData> types, SourceCodeBuilder builder, CancellationToken cancellationToken)
        {
            foreach (var data in types)
            {
                var (typeName, format) = data.PrimitiveTypeSymbol.GetOldOpenApiTypeAndFormat();

                // Get the XML documentation comment for the namedTypeSymbol
                var xmlDocumentation = data.TypeSymbol.GetDocumentationCommentXml(cancellationToken: cancellationToken);

                AddOldMapping(false);

                if (data.TypeSymbol.IsValueType)
                {
                    builder.NewLine();
                    AddOldMapping(true);
                }

                continue;

                void AddOldMapping(bool isNullable)
                {
                    builder.OpenBracket();

                    builder.Append("typeof(").Append(data.ClassName).AppendIf(isNullable, "?").AppendLine("),");

                    builder.Append("new OpenApiSchema")
                        .OpenBracket()
                        .Append("Type = ").Append(Quote(typeName)).AppendLine(",");

                    if (!string.IsNullOrEmpty(format))
                        builder.Append("Format = ").Append(Quote(data.SerializationFormat ?? format)).AppendLine(",");

                    if (isNullable)
                        builder.AppendLine("Nullable = true,");

                    var title = isNullable ? $"Nullable<{data.ClassName}>" : data.ClassName;

                    builder.Append("Title = ").Append(Quote(title)).AppendLine(",");

                    if (!string.IsNullOrEmpty(xmlDocumentation))
                    {
                        var xmlDoc = new System.Xml.XmlDocument();
                        xmlDoc.LoadXml(xmlDocumentation);

                        // Select the <summary> node
                        var summaryNode = xmlDoc.SelectSingleNode("member/summary");

                        if (summaryNode is not null)
                        {
                            builder.Append("Description = @").Append(Quote(summaryNode.InnerText.Trim())).AppendLine(",");
                        }

                        var example = xmlDoc.SelectSingleNode("member/example");
                        if (example is not null)
                        {
                            var exampleValue = example.InnerText.Trim().Replace("\"", "\\\"");
                            builder.Append("Example = new OpenApiString(").Append("\"" + exampleValue + "\"").AppendLine("),");
                        }
                    }

                    builder.Length -= SourceCodeBuilder.s_newLineLength + 1;
                    builder.NewLine();
                    builder.AppendLine("}");
                    builder.CloseBracketWithComma();
                }
            }
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
        var builder = new SourceCodeBuilder();

        builder.AppendSourceHeader("AltaSoft DomainPrimitives Generator");

        builder.AppendUsings([
            data.Namespace,
            "System",
            "System.ComponentModel",
            "System.Globalization",
            "AltaSoft.DomainPrimitives"
        ]);

        builder.AppendNamespace(data.Namespace + ".Converters");
        builder.AppendSummary($"TypeConverter for <see cref = \"{data.ClassName}\"/>");
        builder.AppendClass(false, "internal sealed", data.ClassName + "TypeConverter", $"{friendlyName}Converter");
        builder.AppendInheritDoc()
            .AppendLine("public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)")
            .OpenBracket();

        if (data.SerializationFormat is not null)
        {
            builder.AppendLine("if (value is string s)")
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
            builder.AppendLine("var result = base.ConvertFrom(context, culture, value);")
            .AppendLine("if (result is null)")
            .AppendIndentation().AppendLine("return null;")
            .AppendLine("try")
            .OpenBracket()
            .AppendLine($"return new {data.ClassName}(({data.PrimitiveTypeFriendlyName})result);")
            .CloseBracket()
            .AppendLine("catch (InvalidDomainValueException ex)")
            .OpenBracket()
            .Append("throw new FormatException(\"Cannot parse ").Append(data.ClassName).AppendLine("\", ex);")
            .CloseBracket();
        }
        builder.CloseBracket().CloseBracket();

        context.AddSource($"{data.ClassName}TypeConverter.g.cs", builder.ToString());
    }

    /// <summary>
    /// Generates the value converters extension for the specified assembly name, types, and source production context.
    /// </summary>
    /// <param name="addAssemblyAttribute">if assembly attribute should be added</param>
    /// <param name="assemblyName">The name of the assembly.</param>
    /// <param name="types">The list of named type symbols.</param>
    /// <param name="context">The source production context.</param>
    internal static void GenerateValueConvertersExtension(bool addAssemblyAttribute, string assemblyName, List<INamedTypeSymbol> types, SourceProductionContext context)
    {
        if (types.Count == 0)
            return;

        var builder = new SourceCodeBuilder();
        builder.AppendSourceHeader("AltaSoft DomainPrimitives Generator");

        var usings = types.ConvertAll(x => x.ContainingNamespace.ToDisplayString());
        usings.Add("Microsoft.EntityFrameworkCore");
        usings.AddRange(types.ConvertAll(x => x.ContainingNamespace.ToDisplayString() + ".EntityFrameworkCore.Converters"));

        builder.AppendUsings(usings);

        if (addAssemblyAttribute)
            builder.AppendLine("[assembly: AltaSoft.DomainPrimitives.DomainPrimitiveAssemblyAttribute]");

        var ns = string.Join(".", assemblyName.Split('.').Select(s => char.IsDigit(s[0]) ? '_' + s : s));
        builder.AppendNamespace(ns + ".Converters.Extensions");

        builder.AppendSummary($"Helper class providing methods to configure EntityFrameworkCore ValueConverters for DomainPrimitive types of {assemblyName}");
        builder.AppendClass(false, "public static", "ModelConfigurationBuilderExt");

        builder.AppendSummary("Adds EntityFrameworkCore ValueConverters for specific custom types to ensure proper mapping to EFCore ORM.");
        builder.AppendParamDescription("configurationBuilder", "The ModelConfigurationBuilder instance to which converters are added.");
        builder.AppendLine("public static ModelConfigurationBuilder AddDomainPrimitivePropertyConversions(this ModelConfigurationBuilder configurationBuilder)")
            .OpenBracket();

        foreach (var type in types)
        {
            builder.Append("configurationBuilder.Properties<").Append(type.Name).Append(">().HaveConversion<").Append(type.Name).AppendLine("ValueConverter>();");
        }

        builder.AppendLine("return configurationBuilder;");
        builder.CloseBracket();

        builder.CloseBracket();
        context.AddSource("ModelConfigurationBuilderExt.g.cs", builder.ToString());
    }

    /// <summary>
    /// Processes the Entity Framework value converter for the specified generator data and source production context.
    /// </summary>
    /// <param name="data">The generator data.</param>
    /// <param name="context">The source production context.</param>
    internal static void ProcessEntityFrameworkValueConverter(GeneratorData data, SourceProductionContext context)
    {
        var builder = new SourceCodeBuilder();

        builder.AppendSourceHeader("AltaSoft DomainPrimitives Generator");

        var usingStatements =
            new List<string>(8)
                {
                    data.Namespace,
                    data.PrimitiveTypeSymbol.ContainingNamespace.ToDisplayString(),
                    "Microsoft.EntityFrameworkCore",
                    "Microsoft.EntityFrameworkCore.Storage.ValueConversion",
                    "AltaSoft.DomainPrimitives",
                };

        var converterName = data.ClassName + "ValueConverter";

        builder.AppendUsings(usingStatements);

        builder.AppendNamespace(data.Namespace + ".EntityFrameworkCore.Converters");
        builder.AppendSummary($"ValueConverter for <see cref = \"{data.ClassName}\"/>");
        builder.AppendClass(false, "public sealed", converterName, $"ValueConverter<{data.ClassName}, {data.PrimitiveTypeFriendlyName}>");

        builder.AppendSummary($"Constructor to create {converterName}")
            .AppendLine($"public {converterName}() : base(v=> v, v=> v)")
            .OpenBracket()
            .CloseBracket();

        builder.CloseBracket();

        context.AddSource($"{converterName}.g.cs", builder.ToString());
    }

    /// <summary>
    /// Generates code for a JsonConverter for the specified type.
    /// </summary>
    /// <param name="data">The generator data containing type information.</param>
    /// <param name="context">The source production context.</param>
    internal static void ProcessJsonConverter(GeneratorData data, SourceProductionContext context)
    {
        var builder = new SourceCodeBuilder();

        builder.AppendSourceHeader("AltaSoft DomainPrimitives Generator");

        var usingStatements =
            new List<string>(8)
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
        builder.AppendUsings(usingStatements);

        builder.AppendNamespace(data.Namespace + ".Converters");
        builder.AppendSummary($"JsonConverter for <see cref = \"{data.ClassName}\"/>");
        builder.AppendClass(false, "internal sealed", data.ClassName + "JsonConverter", $"JsonConverter<{data.ClassName}>");

        builder.AppendInheritDoc()
            .Append("public override ").Append(data.ClassName).AppendLine(" Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)")
            .OpenBracket();

        if (data.SerializationFormat is null)
        {
            var rawValueStr = $"JsonInternalConverters.{converterName}Converter.Read(ref reader, typeToConvert, options){(primitiveTypeIsValueType ? "" : "!")}";

            builder.AppendLine("try")
                .OpenBracket()
                .AppendLineIf(data.GenerateImplicitOperators, $"return {rawValueStr};")
                .AppendLineIf(!data.GenerateImplicitOperators, $"return new ({rawValueStr});")
                .CloseBracket();
        }
        else
        {
            builder.AppendLine("if (reader.TokenType != JsonTokenType.String)")
                .AppendIndentation().Append("throw new JsonException(\"Expected a string value to deserialize ").Append(data.ClassName).AppendLine("\");")
                .NewLine()
                .Append("var str = reader.GetString() ?? throw new JsonException(\"Expected a non-null string value to deserialize ").Append(data.ClassName).AppendLine("\");")
                .AppendLine("try")
                .OpenBracket()
                .Append("return ").Append(data.ClassName).AppendLine(".Parse(str, CultureInfo.InvariantCulture);")
                .CloseBracket();
        }

        builder.AppendLine("catch (InvalidDomainValueException ex)")
            .OpenBracket()
            .AppendLine("throw new JsonException(ex.Message);")
            .CloseBracket().CloseBracket()
        .NewLine();

        builder.AppendInheritDoc().AppendLine($"public override void Write(Utf8JsonWriter writer, {data.ClassName} value, JsonSerializerOptions options)")
            .OpenBracket()
            .AppendLineIf(data.SerializationFormat is null, $"JsonInternalConverters.{converterName}Converter.Write(writer, ({data.PrimitiveTypeFriendlyName})value, options);")
            .AppendLineIf(data.SerializationFormat is not null, $"writer.WriteStringValue(value.ToString({QuoteAndEscape(data.SerializationFormat)}, CultureInfo.InvariantCulture));")
            .CloseBracket()
            .NewLine();

        builder.AppendInheritDoc()
            .Append("public override ").Append(data.ClassName).AppendLine(" ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)")
            .OpenBracket();

        if (data.SerializationFormat is null)
        {

            var rawValueStr = $"JsonInternalConverters.{converterName}Converter.ReadAsPropertyName(ref reader, typeToConvert, options){(primitiveTypeIsValueType ? "" : "!")}";

            builder.AppendLine("try")
                .OpenBracket()
                .AppendLineIf(data.GenerateImplicitOperators, $"return {rawValueStr};")
                .AppendLineIf(!data.GenerateImplicitOperators, $"return new({rawValueStr});")
                .CloseBracket();
        }
        else
        {
            builder.AppendLine("if (reader.TokenType != JsonTokenType.String)")
                .AppendIndentation().Append("throw new JsonException(\"Expected a string value to deserialize ").Append(data.ClassName).AppendLine("\");")
                .NewLine()
                .Append("var str = reader.GetString() ?? throw new JsonException(\"Expected a non-null string value to deserialize ").Append(data.ClassName).AppendLine("\");")
                .AppendLine("try")
                .OpenBracket()
                .Append("return ").Append(data.ClassName).AppendLine(".Parse(str, CultureInfo.InvariantCulture);")
                .CloseBracket();
        }

        builder.AppendLine("catch (InvalidDomainValueException ex)")
            .OpenBracket()
            .AppendLine("throw new JsonException(ex.Message);")
            .CloseBracket()
            .CloseBracket()
            .NewLine();

        builder.AppendInheritDoc()
            .Append("public override void WriteAsPropertyName(Utf8JsonWriter writer, ").Append(data.ClassName).AppendLine(" value, JsonSerializerOptions options)")
            .OpenBracket()
                .AppendLineIf(data.SerializationFormat is null, $"JsonInternalConverters.{converterName}Converter.WriteAsPropertyName(writer, ({data.PrimitiveTypeFriendlyName})value, options);")
            .AppendLineIf(data.SerializationFormat is not null, $"writer.WritePropertyName(value.ToString({QuoteAndEscape(data.SerializationFormat)}, CultureInfo.InvariantCulture));")
            .CloseBracket();

        builder.CloseBracket();

        context.AddSource($"{data.ClassName}JsonConverter.g.cs", builder.ToString());
    }

    /// <summary>
    /// TryCreate,TryCreate with error message methods for the specified type, and ValidateOrThrow.
    /// </summary>
    /// <param name="data">The generator data containing type information.</param>
    /// <param name="builder">The source code builder.</param>
    internal static void GenerateMandatoryMethods(GeneratorData data, SourceCodeBuilder builder)
    {
        builder.AppendSummary("Tries to create an instance of AsciiString from the specified value.")
            .AppendParamDescription("value", $"The value to create {data.ClassName} from")
            .AppendParamDescription("result", $"When this method returns, contains the created {data.ClassName} if the conversion succeeded, or null if the conversion failed.")
            .AppendReturnsDescription("true if the conversion succeeded; otherwise, false.");

        var primitiveType = data.ParentSymbols.Count != 0 ? data.ParentSymbols[0].GetFriendlyName() : data.PrimitiveTypeFriendlyName;

        builder.Append("public static bool TryCreate(").Append(primitiveType).Append(" value, [NotNullWhen(true)] out ").Append(data.ClassName).AppendLine("? result)")
            .OpenBracket()
            .Append("return TryCreate(value, out result, out _);")
            .CloseBracket().NewLine();

        builder.AppendSummary("Tries to create an instance of AsciiString from the specified value.")
            .AppendParamDescription("value", $"The value to create {data.ClassName} from")
            .AppendParamDescription("result", $"When this method returns, contains the created {data.ClassName} if the conversion succeeded, or null if the conversion failed.")
            .AppendParamDescription("errorMessage", "When this method returns, contains the error message if the conversion failed; otherwise, null.")
            .AppendReturnsDescription("true if the conversion succeeded; otherwise, false.");

        builder.Append("public static bool TryCreate(").Append(primitiveType).Append(" value, [NotNullWhen(true)]  out ").Append(data.ClassName)
            .AppendLine("? result, [NotNullWhen(false)]  out string? errorMessage)")
            .OpenBracket();

        if (data.UseTransformMethod)
        {
            builder.AppendLine("value = Transform(value);");
        }

        AddStringLengthValidation(data, builder);

        builder.AppendLine("var validationResult = Validate(value);")
               .AppendLine("if (!validationResult.IsValid)")
               .OpenBracket()
               .AppendLine("result = null;")
               .AppendLine("errorMessage = validationResult.ErrorMessage;")
               .AppendLine("return false;")
               .CloseBracket()
               .NewLine()

               .AppendLine("result = new (value, false);")
               .AppendLine("errorMessage = null;")
               .AppendLine("return true;")
               .CloseBracket()
               .NewLine();

        builder.AppendSummary("Validates the specified value and throws an exception if it is not valid.")
            .AppendParamDescription("value", "The value to validate")
            .AppendExceptionDescription("InvalidDomainValueException", "Thrown when the value is not valid.");

        builder.AppendLine($"public void ValidateOrThrow({primitiveType} value)")
            .OpenBracket()
            .AppendLine("var result = Validate(value);")
            .AppendLine("if (!result.IsValid)")
            .AppendLine($"\tthrow new InvalidDomainValueException(result.ErrorMessage, typeof({data.ClassName}), value);")
            .CloseBracket()
            .NewLine();

        return;

        static void AddStringLengthValidation(GeneratorData data, SourceCodeBuilder sb)
        {
            if (data.StringLengthAttributeValidation is null)
                return;

            var (minValue, maxValue) = data.StringLengthAttributeValidation.Value;
            var hasMinValue = minValue >= 0;
            var hasMaxValue = maxValue != int.MaxValue;

            if (!hasMinValue && !hasMaxValue)
                return;

            sb.Append("if (value.Length is ")
                .AppendIf(hasMinValue, $"< {minValue}")
                .AppendIf(hasMinValue && hasMaxValue, " or ")
                .AppendIf(hasMaxValue, $"> {maxValue}").AppendLine(")")
                .OpenBracket()
                .AppendLine("result = null;")
                .AppendLine($"errorMessage = \"String length is out of range {minValue}..{maxValue}\";")
                .AppendLine("return false;")
                .CloseBracket()
                .NewLine();
        }
    }

    /// <summary>
    /// Generates IConvertible interface methods for the specified type.
    /// </summary>
    /// <param name="data">The generator data containing type information.</param>
    /// <param name="builder">The source code builder.</param>
    internal static void GenerateConvertibles(GeneratorData data, SourceCodeBuilder builder)
    {
        var fieldName = $"({data.UnderlyingType}){data.FieldName}";

        if (data.UnderlyingType is DomainPrimitiveUnderlyingType.DateOnly or DomainPrimitiveUnderlyingType.TimeOnly)
            fieldName = '(' + fieldName + ").ToDateTime()";

        builder.AppendInheritDoc();
        builder.AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]");
        builder.Append("TypeCode IConvertible.GetTypeCode()")
            .AppendLine($" => ((IConvertible){fieldName}).GetTypeCode();")
            .NewLine();

        builder.AppendInheritDoc();
        builder.Append("bool IConvertible.ToBoolean(IFormatProvider? provider)")
            .AppendLine($" => ((IConvertible){fieldName}).ToBoolean(provider);")
            .NewLine();

        builder.AppendInheritDoc();
        builder.Append("byte IConvertible.ToByte(IFormatProvider? provider)")
            .AppendLine($" => ((IConvertible){fieldName}).ToByte(provider);")
            .NewLine();

        builder.AppendInheritDoc();
        builder.Append("char IConvertible.ToChar(IFormatProvider? provider)")
            .AppendLine($" => ((IConvertible){fieldName}).ToChar(provider);")
            .NewLine();

        builder.AppendInheritDoc();
        builder.Append("DateTime IConvertible.ToDateTime(IFormatProvider? provider)")
            .AppendLine($" => ((IConvertible){fieldName}).ToDateTime(provider);")
            .NewLine();

        builder.AppendInheritDoc();
        builder.Append("decimal IConvertible.ToDecimal(IFormatProvider? provider)")
            .AppendLine($" => ((IConvertible){fieldName}).ToDecimal(provider);")
            .NewLine();

        builder.AppendInheritDoc();
        builder.Append("double IConvertible.ToDouble(IFormatProvider? provider)")
            .AppendLine($" => ((IConvertible){fieldName}).ToDouble(provider);")
            .NewLine();

        builder.AppendInheritDoc();
        builder.Append("short IConvertible.ToInt16(IFormatProvider? provider)")
            .AppendLine($" => ((IConvertible){fieldName}).ToInt16(provider);")
            .NewLine();

        builder.AppendInheritDoc();
        builder.Append("int IConvertible.ToInt32(IFormatProvider? provider)")
            .AppendLine($" => ((IConvertible){fieldName}).ToInt32(provider);")
            .NewLine();

        builder.AppendInheritDoc();
        builder.Append("long IConvertible.ToInt64(IFormatProvider? provider)")
            .AppendLine($" => ((IConvertible){fieldName}).ToInt64(provider);")
            .NewLine();

        builder.AppendInheritDoc();
        builder.Append("sbyte IConvertible.ToSByte(IFormatProvider? provider)")
            .AppendLine($" => ((IConvertible){fieldName}).ToSByte(provider);")
            .NewLine();

        builder.AppendInheritDoc();
        builder.Append("float IConvertible.ToSingle(IFormatProvider? provider)")
            .AppendLine($" => ((IConvertible){fieldName}).ToSingle(provider);")
            .NewLine();

        builder.AppendInheritDoc();
        builder.Append("string IConvertible.ToString(IFormatProvider? provider)")
            .AppendLine($" => ((IConvertible){fieldName}).ToString(provider);")
            .NewLine();

        builder.AppendInheritDoc();
        builder.Append("object IConvertible.ToType(Type conversionType, IFormatProvider? provider)")
            .AppendLine($" => ((IConvertible){fieldName}).ToType(conversionType, provider);")
            .NewLine();

        builder.AppendInheritDoc();
        builder.Append("ushort IConvertible.ToUInt16(IFormatProvider? provider)")
            .AppendLine($" => ((IConvertible){fieldName}).ToUInt16(provider);")
            .NewLine();

        builder.AppendInheritDoc();
        builder.Append("uint IConvertible.ToUInt32(IFormatProvider? provider)")
            .AppendLine($" => ((IConvertible){fieldName}).ToUInt32(provider);")
            .NewLine();

        builder.AppendInheritDoc();
        builder.Append("ulong IConvertible.ToUInt64(IFormatProvider? provider)")
            .AppendLine($" => ((IConvertible){fieldName}).ToUInt64(provider);")
            .NewLine();
    }

    /// <summary>
    /// Generates an addition operator for the specified type.
    /// </summary>
    /// <param name="className">The name of the class.</param>
    /// <param name="fieldName">The name of the field to perform addition on.</param>
    /// <param name="builder">The source code builder.</param>
    internal static void GenerateAdditionCode(string className, string fieldName, SourceCodeBuilder builder)
    {
        builder.AppendInheritDoc()
            .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
            .Append($"public static {className} operator +({className} left, {className} right)")
            .AppendLine($" => new(left.{fieldName} + right.{fieldName});");
    }

    /// <summary>
    /// Generates code for implementing the IComparable interface for the specified type.
    /// </summary>
    /// <param name="className">The name of the class.</param>
    /// <param name="isValueType">A flag indicating if the type is a value type.</param>
    /// <param name="builder">The source code builder.</param>
    internal static void GenerateComparableCode(string className, bool isValueType, SourceCodeBuilder builder)
    {
        builder.AppendInheritDoc()
            .AppendLine("public int CompareTo(object? obj)")
            .OpenBracket()
            .AppendLine("if (obj is null)")
            .AppendIndentation().AppendLine("return 1;").NewLine()
            .AppendLine($"if (obj is {className} c)")
            .AppendIndentation().AppendLine("return CompareTo(c);").NewLine()
            .AppendLine($"throw new ArgumentException(\"Object is not a {className}\", nameof(obj));")
            .CloseBracket();

        var nullable = isValueType ? "" : "?";

        builder.NewLine().AppendInheritDoc()
            .AppendLine($"public int CompareTo({className}{nullable} other)")
            .OpenBracket()
            .Append("if (").AppendIf(!isValueType, "other is null || ").AppendLine("!other._isInitialized)")
            .AppendIndentation().AppendLine("return 1;")
            .AppendLine("if (!_isInitialized)")
            .AppendIndentation().AppendLine("return -1;")
            .AppendLine("return _value.CompareTo(other._value);")
            .CloseBracket();
    }

    /// <summary>
    /// Generates comparison operators (&lt;, &lt;=, &gt;, &gt;=) for the specified type.
    /// </summary>
    /// <param name="className">The name of the class.</param>
    /// <param name="fieldName">The name of the field to compare.</param>
    /// <param name="builder">The source code builder.</param>
    internal static void GenerateComparisonCode(string className, string fieldName, SourceCodeBuilder builder)
    {
        builder.AppendInheritDoc()
            .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
            .Append($"public static bool operator <({className} left, {className} right)")
            .AppendLine($" => left.{fieldName} < right.{fieldName};")
            .NewLine();

        builder.AppendInheritDoc()
            .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
            .Append($"public static bool operator <=({className} left, {className} right)")
            .AppendLine($" => left.{fieldName} <= right.{fieldName};")
            .NewLine();

        builder.AppendInheritDoc()
            .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
            .Append($"public static bool operator >({className} left, {className} right)")
            .AppendLine($" => left.{fieldName} > right.{fieldName};")
            .NewLine();

        builder.AppendInheritDoc()
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
    /// <param name="builder">The source code builder.</param>
    public static void GenerateDivisionCode(string className, string fieldName, SourceCodeBuilder builder)
    {
        builder.AppendInheritDoc()
            .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
            .Append($"public static {className} operator /({className} left, {className} right)")
            .AppendLine($" => new(left.{fieldName} / right.{fieldName});");
    }

    /// <summary>
    /// Generates a multiplication operator for the specified type.
    /// </summary>
    /// <param name="className">The name of the class.</param>
    /// <param name="fieldName">The name of the field to perform multiplication on.</param>
    /// <param name="builder">The source code builder.</param>
    public static void GenerateMultiplyCode(string className, string fieldName, SourceCodeBuilder builder)
    {
        builder.AppendInheritDoc()
            .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
            .Append($"public static {className} operator *({className} left, {className} right)")
            .AppendLine($" => new(left.{fieldName} * right.{fieldName});");
    }

    /// <summary>
    /// Generates a subtraction operator for the specified type.
    /// </summary>
    /// <param name="className">The name of the class.</param>
    /// <param name="fieldName">The name of the field to perform subtraction on.</param>
    /// <param name="builder">The source code builder.</param>
    public static void GenerateSubtractionCode(string className, string fieldName, SourceCodeBuilder builder)
    {
        builder.AppendInheritDoc()
            .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
            .Append($"public static {className} operator -({className} left, {className} right)")
            .AppendLine($" => new(left.{fieldName} - right.{fieldName});");
    }

    /// <summary>
    /// Generates a modulus operator for the specified type.
    /// </summary>
    /// <param name="className">The name of the class.</param>
    /// <param name="fieldName">The name of the field to perform modulus on.</param>
    /// <param name="builder">The source code builder.</param>
    public static void GenerateModulusCode(string className, string fieldName, SourceCodeBuilder builder)
    {
        builder.AppendInheritDoc()
            .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
            .Append($"public static {className} operator %({className} left, {className} right)")
            .AppendLine($" => new(left.{fieldName} % right.{fieldName});");
    }

    /// <summary>
    /// Generates methods required for implementing ISpanFormattable for the specified type.
    /// </summary>
    /// <param name="builder">The source code builder.</param>
    /// <param name="fieldName">The name of the field.</param>
    public static void GenerateSpanFormattable(SourceCodeBuilder builder, string fieldName)
    {
        builder.AppendInheritDoc()
            .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
            .AppendLine($"public string ToString(string? format, IFormatProvider? formatProvider) => {fieldName}.ToString(format, formatProvider);")
            .NewLine();

        builder.AppendInheritDoc()
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
    /// <param name="builder">The SourceCodeBuilder to append the generated code.</param>
    /// <param name="fieldName">The name of the field.</param>
    internal static void GenerateUtf8Formattable(SourceCodeBuilder builder, string fieldName)
    {
        builder.AppendPreProcessorDirective("if NET8_0_OR_GREATER")
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
    /// <param name="builder">The <see cref="SourceCodeBuilder"/> used to build the source code.</param>
    /// <remarks>
    /// This method generates parsing methods based on the provided data type and serialization format.
    /// </remarks>
    public static void GenerateParsable(GeneratorData data, SourceCodeBuilder builder)
    {
        var dataClassName = data.ClassName;
        var underlyingType = data.ParentSymbols.Count == 0
            ? data.PrimitiveTypeFriendlyName
            : data.ParentSymbols[0].Name;
        var format = data.SerializationFormat;

        builder.AppendInheritDoc()
            .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
            .Append($"public static {dataClassName} Parse(string s, IFormatProvider? provider) => ");

        var isString = data.ParentSymbols.Count == 0 && data.UnderlyingType is DomainPrimitiveUnderlyingType.String;
        var isChar = data.ParentSymbols.Count == 0 && data.UnderlyingType is DomainPrimitiveUnderlyingType.Char;
        var isBool = data.ParentSymbols.Count == 0 && data.UnderlyingType is DomainPrimitiveUnderlyingType.Boolean;

        if (!data.GenerateImplicitOperators)
            builder.Append("new (");

        if (isString)
        {
            builder.Append("s");
        }
        else
        if (isChar)
        {
            builder.Append("char.Parse(s)");
        }
        else
        if (isBool)
        {
            builder.Append("bool.Parse(s)");
        }
        else
        {
            builder.Append($"{underlyingType}.")
                .AppendIfElse(format is null, "Parse(s, provider)", $"ParseExact(s, {QuoteAndEscape(format!)}, provider)");
        }

        builder.AppendLine(!data.GenerateImplicitOperators ? ");" : ";");

        builder.NewLine();

        builder.AppendInheritDoc()
            .AppendLine($"public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out {dataClassName} result)")
            .OpenBracket();

        if (isString)
        {
            builder.AppendLine("if (s is null)");
        }
        else
        if (isChar)
        {
            builder.AppendLine("if (!char.TryParse(s, out var value))");
        }
        else
        if (isBool)
        {
            builder.AppendLine("if (!bool.TryParse(s, out var value))");
        }
        else
        {
            var style = "";
            if (format is not null)
            {
                style = data.UnderlyingType == DomainPrimitiveUnderlyingType.TimeSpan ? "TimeSpanStyles.None" : "DateTimeStyles.None";
            }

            builder.AppendIf(format is null, $"if (!{underlyingType}.TryParse(s, provider, out var value))")
                .AppendIf(format is not null, $"if (!{underlyingType}.TryParseExact(s, {QuoteAndEscape(format)}, provider, {style}, out var value))");
        }

        builder.OpenBracket()
        .AppendLine("result = default;")
        .AppendLine("return false;")
        .CloseBracket()
        .NewLine();

        if (!data.TypeSymbol.IsValueType)
        {
            builder.AppendLine($"return {dataClassName}.TryCreate(s, out result);");
        }
        else
        {
            builder.AppendLine("if (TryCreate(value, out var created))")
                .OpenBracket()
                .AppendLine("result = created.Value;")
                .AppendLine("return true;")
                .CloseBracket()
                .NewLine()
                .AppendLine("result = default;")
                .AppendLine("return false;");
        }

        builder.CloseBracket();
    }

    /// <summary>
    /// Generates string methods
    /// </summary>
    /// <param name="builder">The source code builder.</param>
    public static void GenerateStringMethods(SourceCodeBuilder builder)
    {
        builder
            .AppendLine("/// <summary>")
            .AppendLine("/// Gets the character at the specified index.")
            .AppendLine("/// </summary>")
            .AppendLine("public char this[int i]")
            .OpenBracket()
            .AppendLine("get => _value[i];")
            .CloseBracket()
            .NewLine();

        builder
            .AppendLine("/// <summary>")
            .AppendLine("/// Gets the character at the specified index.")
            .AppendLine("/// </summary>")
            .AppendLine("public char this[Index index]")
            .OpenBracket()
            .AppendLine("get => _value[index];")
            .CloseBracket()
            .NewLine();

        builder
            .AppendLine("/// <summary>")
            .AppendLine("/// Gets the substring by specified range.")
            .AppendLine("/// </summary>")
            .AppendLine("public string this[Range range]")
            .OpenBracket()
            .AppendLine("get => _value[range];")
            .CloseBracket()
            .NewLine();

        builder
            .AppendLine("/// <summary>")
            .AppendLine("/// Gets the number of characters.")
            .AppendLine("/// </summary>")
            .AppendLine("/// <returns>The number of characters in underlying string value.</returns>")
            .AppendLine("public int Length => _value.Length;")
            .NewLine();

        builder
            .AppendLine("/// <summary>")
            .AppendLine("/// Returns a substring of this string.")
            .AppendLine("/// </summary>")
            .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
            .AppendLine("public string Substring(int startIndex, int length) => _value.Substring(startIndex, length);")
            .NewLine();

        builder
            .AppendLine("/// <summary>")
            .AppendLine("/// Returns a substring of this string.")
            .AppendLine("/// </summary>")
            .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
            .AppendLine("public string Substring(int startIndex) => _value.Substring(startIndex);")
            .NewLine();

        builder
            .AppendLine("/// <summary>")
            .AppendLine("/// Checks if the specified value is contained within the current instance.")
            .AppendLine("/// </summary>")
            .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
            .AppendLine("public bool Contains(string value) => _value.Contains(value);")
            .NewLine();

        builder
            .AppendLine("/// <summary>")
            .AppendLine("/// Determines whether a specified string is a prefix of the current instance.")
            .AppendLine("/// </summary>")
            .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
            .AppendLine("public bool StartsWith(string value) => _value.StartsWith(value);")
            .NewLine();

        builder
            .AppendLine("/// <summary>")
            .AppendLine("/// Determines whether a specified string is a suffix of the current instance.")
            .AppendLine("/// </summary>")
            .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
            .AppendLine("public bool EndsWith(string value) => _value.EndsWith(value);")
            .NewLine();

        builder
            .AppendLine("/// <summary>")
            .AppendLine("/// Returns the entire string as an array of characters.")
            .AppendLine("/// </summary>")
            .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
            .AppendLine("public char[] ToCharArray() => _value.ToCharArray();");
    }

    /// <summary>
    /// Generates equality and inequality operators for the specified type.
    /// </summary>
    /// <param name="className">The name of the class.</param>
    /// <param name="isValueType">A flag indicating if the type is a value type.</param>
    /// <param name="builder">The source code builder.</param>
    public static void GenerateEquatableOperators(string className, bool isValueType, SourceCodeBuilder builder)
    {
        builder.AppendInheritDoc()
            .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
            .AppendLine($"public override bool Equals(object? obj) => obj is {className} other && Equals(other);");

        var nullable = isValueType ? "" : "?";

        builder.AppendInheritDoc()
            .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
            .AppendLine($"public bool Equals({className}{nullable} other)")
            .OpenBracket()
            .Append("if (").AppendIf(!isValueType, "other is null || ").AppendLine("!_isInitialized || !other._isInitialized)")
            .AppendIndentation().AppendLine("return false;")
            .AppendLine("return _value.Equals(other._value);")
            .CloseBracket();

        builder.AppendInheritDoc()
            .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
            .Append($"public static bool operator ==({className}{nullable} left, {className}{nullable} right)");

        if (isValueType)
        {
            builder.AppendLine(" => left.Equals(right);");
        }
        else
        {
            builder.NewLine();
            builder.OpenBracket()
                .AppendLine("if (ReferenceEquals(left, right))")
                .AppendIndentation().AppendLine("return true;")
                .AppendLine("if (left is null || right is null)")
                .AppendIndentation().AppendLine("return false;")
                .AppendLine("return left.Equals(right);")
                .CloseBracket();
        }

        builder.AppendInheritDoc()
                .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
                .AppendLine($"public static bool operator !=({className}{nullable} left, {className}{nullable} right) => !(left == right);");
    }

    /// <summary>
    /// Generates the necessary methods for implementing the IXmlSerializable interface.
    /// </summary>
    /// <param name="data">The generator data.</param>
    /// <param name="builder">The source code builder.</param>
    public static void GenerateIXmlSerializableMethods(GeneratorData data, SourceCodeBuilder builder)
    {
        builder.AppendInheritDoc();
        builder.AppendLine("public XmlSchema? GetSchema() => null;")
            .NewLine();

        string method;

        if (data.UnderlyingType.IsDateOrTime() && data.SerializationFormat is not null)
        {
            method = "ReadElementContentAs" + data.PrimitiveTypeFriendlyName;
        }
        else
        {
            method = data.PrimitiveTypeFriendlyName switch
            {
                "string" => "ReadElementContentAsString",
                "bool" => "ReadElementContentAsBoolean",
                "DateOnly" => "ReadElementContentAsDateOnly",
                _ => $"ReadElementContentAs<{data.PrimitiveTypeFriendlyName}>"
            };
        }

        builder.AppendInheritDoc();
        builder.AppendLine("public void ReadXml(XmlReader reader)")
            .OpenBracket()
            .Append("var value = reader.").Append(method)
            .AppendLineIfElse(data.SerializationFormat is not null, $"({QuoteAndEscape(data.SerializationFormat)});", "();")
            .AppendLine("ValidateOrThrow(value);")
            .AppendLine("System.Runtime.CompilerServices.Unsafe.AsRef(in _value) = value;")
            .AppendLine("System.Runtime.CompilerServices.Unsafe.AsRef(in _isInitialized) = true;")
            .CloseBracket()
            .NewLine();

        builder.AppendInheritDoc();

        if (string.Equals(data.PrimitiveTypeFriendlyName, "string", System.StringComparison.Ordinal))
            builder.AppendLine($"public void WriteXml(XmlWriter writer) => writer.WriteString({data.FieldName});");
        else
        if (data.SerializationFormat is null)
            builder.AppendLine($"public void WriteXml(XmlWriter writer) => writer.WriteValue((({data.PrimitiveTypeFriendlyName}){data.FieldName}).ToXmlString());");
        else
            builder.AppendLine($"public void WriteXml(XmlWriter writer) => writer.WriteString({data.FieldName}.ToString({QuoteAndEscape(data.SerializationFormat)}));");
        builder.NewLine();
    }

    private static string Quote(string? value) => '\"' + value?.Replace("\"", "\"\"") + '\"';
    private static string QuoteAndEscape(string? value) => value is null ? "\"\"" : SymbolDisplay.FormatLiteral(value, quote: true);
}
