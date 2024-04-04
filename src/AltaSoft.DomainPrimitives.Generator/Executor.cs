using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using AltaSoft.DomainPrimitives.Generator.Extensions;
using AltaSoft.DomainPrimitives.Generator.Helpers;
using AltaSoft.DomainPrimitives.Generator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AltaSoft.DomainPrimitives.Generator;

/// <summary>
/// A static class responsible for executing the generation of code for domain primitive types.
/// </summary>
internal static class Executor
{
    /// <summary>
    /// Executes the generation of domain primitives based on the provided parameters.
    /// </summary>
    /// <param name="typesToGenerate">The list of domain primitives to generate.</param>
    /// <param name="assemblyName">The name of the assembly.</param>
    /// <param name="globalOptions">The global options for domain primitive generation.</param>
    /// <param name="context">The source production context.</param>
    internal static void Execute(
            in ImmutableArray<INamedTypeSymbol?> typesToGenerate,
            in string assemblyName,
            in DomainPrimitiveGlobalOptions globalOptions,
            in SourceProductionContext context)
    {
        if (typesToGenerate.IsDefaultOrEmpty)
            return;

        var swaggerTypes = new List<GeneratorData>(typesToGenerate.Length);
        var cachedOperationsAttributes = new Dictionary<INamedTypeSymbol, SupportedOperationsAttributeData>(SymbolEqualityComparer.Default);

        try
        {
            foreach (var typeSymbol in typesToGenerate)
            {
                if (typeSymbol is null) // Will never happen
                    continue;

                var generatorData = CreateGeneratorData(context, typeSymbol, globalOptions, cachedOperationsAttributes);
                if (generatorData is null)
                    continue;

                switch (generatorData.PrimitiveTypeSymbol.IsValueType)
                {
                    case true when !generatorData.TypeSymbol.IsValueType:
                        context.ReportDiagnostic(DiagnosticHelper.TypeShouldBeValueType(generatorData.ClassName, generatorData.PrimitiveTypeFriendlyName, typeSymbol.Locations.FirstOrDefault()));
                        break;

                    case false when generatorData.TypeSymbol.IsValueType:
                        context.ReportDiagnostic(DiagnosticHelper.TypeShouldBeReferenceType(generatorData.ClassName, generatorData.PrimitiveTypeFriendlyName, typeSymbol.Locations.FirstOrDefault()));
                        break;
                }

                if (!ProcessType(generatorData, globalOptions, context))
                    continue;

                if (globalOptions.GenerateJsonConverters)
                {
                    MethodGeneratorHelper.ProcessJsonConverter(generatorData, context);
                }

                if (globalOptions.GenerateTypeConverters)
                {
                    MethodGeneratorHelper.ProcessTypeConverter(generatorData, context);
                }

                if (globalOptions.GenerateSwaggerConverters)
                    swaggerTypes.Add(generatorData);
            }

            MethodGeneratorHelper.AddSwaggerOptions(assemblyName, swaggerTypes, context);
        }
        catch (Exception ex)
        {
            context.ReportDiagnostic(DiagnosticHelper.GeneralError(Location.None, ex));
        }
    }

    /// <summary>
    /// Creates generator data for a specified class symbol.
    /// </summary>
    /// <returns>The GeneratorData for the class or null if not applicable.</returns>
    private static GeneratorData? CreateGeneratorData(SourceProductionContext context, INamedTypeSymbol typeSymbol, DomainPrimitiveGlobalOptions globalOptions, Dictionary<INamedTypeSymbol, SupportedOperationsAttributeData> cachedOperationsAttributes)
    {
        var interfaceType = typeSymbol.AllInterfaces.First(x => x.IsDomainValue());

        if (interfaceType.TypeArguments[0] is not INamedTypeSymbol primitiveType)
        {
            context.ReportDiagnostic(DiagnosticHelper.InvalidBaseTypeSpecified(typeSymbol.Locations.FirstOrDefault()));
            return null;
        }

        var parentSymbols = new List<INamedTypeSymbol>(4);
        var (underlyingType, underlyingTypeSymbol) = primitiveType.GetUnderlyingPrimitiveType(parentSymbols);

        if (underlyingType == DomainPrimitiveUnderlyingType.Other)
        {
            context.ReportDiagnostic(DiagnosticHelper.InvalidBaseTypeSpecified(typeSymbol.Locations.FirstOrDefault()));
            return null;
        }

        var hasOverridenHashCode = typeSymbol.GetMembersOfType<IMethodSymbol>().Any(x => string.Equals(x.OverriddenMethod?.Name, "GetHashCode", StringComparison.Ordinal));

        var generateIsInitializedField = true;
        var defaultProperty = typeSymbol.GetMembersOfType<IPropertySymbol>().FirstOrDefault(x => string.Equals(x.Name, "Default", StringComparison.Ordinal));
        if (defaultProperty is not null)
        {
            generateIsInitializedField = !DefaultPropertyReturnsDefaultValue(defaultProperty, underlyingType);
        }

        var generatorData = new GeneratorData
        {
            FieldName = generateIsInitializedField ? "_valueOrDefault" : "_value",
            GenerateIsInitializedField = generateIsInitializedField,
            GenerateHashCode = !hasOverridenHashCode,
            UnderlyingType = underlyingType,
            TypeSymbol = typeSymbol,
            PrimitiveTypeSymbol = underlyingTypeSymbol,
            PrimitiveTypeFriendlyName = underlyingTypeSymbol.GetFriendlyName(),
            Namespace = typeSymbol.ContainingNamespace.ToDisplayString(),
            GenerateImplicitOperators = true,
            ParentSymbols = parentSymbols,
            GenerateConvertibles = underlyingType != DomainPrimitiveUnderlyingType.Guid
        };

        var attributes = typeSymbol.GetAttributes();
        var attributeData = attributes.FirstOrDefault(x => string.Equals(x.AttributeClass?.ToDisplayString(), Constants.SupportedOperationsAttributeFullName, StringComparison.Ordinal));
        var serializationAttribute = attributes.FirstOrDefault(x => string.Equals(x.AttributeClass?.ToDisplayString(), Constants.SerializationFormatAttributeFullName, StringComparison.Ordinal));

        var isNumeric = underlyingType.IsNumeric();
        var isDateOrTime = underlyingType.IsDateOrTime();

        if (!isNumeric && attributeData is not null)
        {
            context.ReportDiagnostic(DiagnosticHelper.TypeMustBeNumericType(attributeData.GetAttributeLocation(), typeSymbol.Name));
            return null;
        }

        if (!isDateOrTime && serializationAttribute is not null)
        {
            context.ReportDiagnostic(DiagnosticHelper.TypeMustBeDateType(serializationAttribute.GetAttributeLocation(), typeSymbol.Name));
            return null;
        }

        if (serializationAttribute is not null && serializationAttribute.ConstructorArguments.Length != 0)
        {
            var value = serializationAttribute.ConstructorArguments[0];
            generatorData.SerializationFormat = value.Value?.ToString();
        }

        if (isNumeric)
        {
            var supportedOperationsAttributeData = GetSupportedOperationsAttributeData(typeSymbol, underlyingType, parentSymbols, cachedOperationsAttributes);

            generatorData.GenerateAdditionOperators = supportedOperationsAttributeData.Addition && !typeSymbol.ImplementsInterface("System.Numerics.IAdditionOperators");

            generatorData.GenerateSubtractionOperators = supportedOperationsAttributeData.Subtraction && !typeSymbol.ImplementsInterface("System.Numerics.ISubtractionOperators");

            generatorData.GenerateDivisionOperators = supportedOperationsAttributeData.Division && !typeSymbol.ImplementsInterface("System.Numerics.IDivisionOperators");

            generatorData.GenerateMultiplyOperators = supportedOperationsAttributeData.Multiplication && !typeSymbol.ImplementsInterface("System.Numerics.IMultiplyOperators");

            generatorData.GenerateModulusOperator = supportedOperationsAttributeData.Modulus && !typeSymbol.ImplementsInterface("System.Numerics.IModulusOperator");
        }

        generatorData.GenerateParsable = !typeSymbol.ImplementsInterface("System.IParsable");

        generatorData.GenerateComparison = (isNumeric || (underlyingType == DomainPrimitiveUnderlyingType.Char || underlyingType.IsDateOrTime())) && !typeSymbol.ImplementsInterface("System.Numerics.IComparisonOperators");

        generatorData.GenerateSpanFormattable = (underlyingType == DomainPrimitiveUnderlyingType.Guid || underlyingType.IsDateOrTime()) && !typeSymbol.ImplementsInterface("System.ISpanFormattable");

        generatorData.GenerateUtf8SpanFormattable = primitiveType.ImplementsInterface("System.IUtf8SpanFormattable") && !typeSymbol.ImplementsInterface("System.IUtf8SpanFormattable");

        generatorData.GenerateXmlSerializableMethods = globalOptions.GenerateXmlSerialization;

        return generatorData;
    }

    private static bool DefaultPropertyReturnsDefaultValue(IPropertySymbol property, DomainPrimitiveUnderlyingType underlyingType)
    {
        var syntaxRefs = property.GetMethod?.DeclaringSyntaxReferences;
        if (syntaxRefs is null)
        {
            // If there are no syntax references, the property doesn't have a getter
            return false;
        }

        ExpressionSyntax? returnExpression = null;

        foreach (var syntaxRef in syntaxRefs)
        {
            var syntaxNode = syntaxRef.GetSyntax();

            // Handle expression-bodied properties
            if (syntaxNode is ArrowExpressionClauseSyntax arrowExpressionClauseSyntax)
            {
                returnExpression = arrowExpressionClauseSyntax.Expression;
                break;
            }

            // Handle expression-bodied properties
            if (syntaxNode is PropertyDeclarationSyntax { ExpressionBody: { } expressionBody })
            {
                returnExpression = expressionBody.Expression;
                break;
            }

            // Handle properties with getters that have a body
            if (syntaxNode is AccessorDeclarationSyntax { Body: not null } accessorDeclaration)
            {
                var returnExpressions = accessorDeclaration.Body.DescendantNodes()
                    .OfType<ReturnStatementSyntax>()
                    .Select(r => r.Expression)
                    .ToArray();

                if (returnExpressions.Length != 1)
                    return false;

                returnExpression = returnExpressions[0];
                break;
            }
        }

        // Check if the return expression is a default value for the type
        switch (returnExpression)
        {
            case null:
                return false;

            case DefaultExpressionSyntax:
                return true;

            // Simplified check for literal or default expressions
            case LiteralExpressionSyntax literal:
                if (literal.IsKind(SyntaxKind.DefaultLiteralExpression))
                    return true;

                // Determine the default value for the property's type
                var defaultValue = underlyingType.GetDefaultValue();
                if (defaultValue is null)
                    return literal.Token.Value is null;

                return defaultValue.Equals(literal.Token.Value);

            // For more complex expressions, additional analysis is required
            default:
                return false;
        }
    }

    /// <summary>
    /// Retrieves the SupportedOperationsAttributeData for a specified class, considering inheritance.
    /// </summary>
    /// <returns>The combined SupportedOperationsAttributeData for the class and its inherited types.</returns>
    private static SupportedOperationsAttributeData GetSupportedOperationsAttributeData(INamedTypeSymbol @class, DomainPrimitiveUnderlyingType underlyingType, List<INamedTypeSymbol> parentSymbols, Dictionary<INamedTypeSymbol, SupportedOperationsAttributeData> cachedOperationsAttributes)
    {
        return CreateCombinedAttribute(@class, underlyingType, parentSymbols.Count, cachedOperationsAttributes);

        static SupportedOperationsAttributeData CreateCombinedAttribute(INamedTypeSymbol @class, DomainPrimitiveUnderlyingType underlyingType, int parentCount, Dictionary<INamedTypeSymbol, SupportedOperationsAttributeData> cachedOperationsAttributes)
        {
            if (cachedOperationsAttributes.TryGetValue(@class, out var parentAttribute))
            {
                return parentAttribute;
            }

            var attributeData = @class.GetAttributes().FirstOrDefault(x => string.Equals(x.AttributeClass?.ToDisplayString(), Constants.SupportedOperationsAttributeFullName, StringComparison.Ordinal));

            var attribute = attributeData is null ? null : GetAttributeFromData(attributeData);

            if (parentCount == 0)
            {
                attribute ??= GetDefaultAttributeData(underlyingType);
                cachedOperationsAttributes[@class] = attribute;

                return attribute;
            }

            var parentType = @class.Interfaces.First(x => x.IsDomainValue());

            var attr = CombineAttribute(attribute, CreateCombinedAttribute((parentType.TypeArguments[0] as INamedTypeSymbol)!, underlyingType, parentCount - 1, cachedOperationsAttributes));

            cachedOperationsAttributes[@class] = attr;
            return attr;
        }

        static SupportedOperationsAttributeData CombineAttribute(SupportedOperationsAttributeData? attribute, SupportedOperationsAttributeData parentAttribute)
        {
            if (attribute is null)
                return parentAttribute;

            return new SupportedOperationsAttributeData
            {
                Addition = attribute.Addition && parentAttribute.Addition,
                Subtraction = attribute.Subtraction && parentAttribute.Subtraction,
                Multiplication = attribute.Multiplication && parentAttribute.Multiplication,
                Division = attribute.Division && parentAttribute.Division,
                Modulus = attribute.Modulus && parentAttribute.Modulus
            };
        }
    }

    /// <summary>
    /// Gets the default SupportedOperationsAttributeData based on the given NumericType.
    /// </summary>
    /// <param name="underlyingType">The NumericType for which to determine default attribute values.</param>
    /// <returns>The default SupportedOperationsAttributeData with attributes set based on the NumericType.</returns>
    private static SupportedOperationsAttributeData GetDefaultAttributeData(DomainPrimitiveUnderlyingType underlyingType)
    {
        var @default = DefaultAttributeValue(underlyingType);

        return new SupportedOperationsAttributeData
        {
            Addition = @default,
            Subtraction = @default,
            Multiplication = @default,
            Division = @default,
            Modulus = @default
        };

        static bool DefaultAttributeValue(DomainPrimitiveUnderlyingType underlyingType)
        {
            return underlyingType switch
            {
                DomainPrimitiveUnderlyingType.Byte => false,
                DomainPrimitiveUnderlyingType.SByte => false,
                DomainPrimitiveUnderlyingType.Int16 => false,
                DomainPrimitiveUnderlyingType.UInt16 => false,
                DomainPrimitiveUnderlyingType.Int32 => true,
                DomainPrimitiveUnderlyingType.UInt32 => true,
                DomainPrimitiveUnderlyingType.Int64 => true,
                DomainPrimitiveUnderlyingType.UInt64 => true,
                //DomainPrimitiveUnderlyingType.Int128 => true,
                //DomainPrimitiveUnderlyingType.UInt128 => true,
                DomainPrimitiveUnderlyingType.Decimal => true,
                DomainPrimitiveUnderlyingType.Double => true,
                DomainPrimitiveUnderlyingType.Single => true,
                _ => true
            };
        }
    }

    /// <summary>
    /// Creates a SupportedOperationsAttributeData from the provided AttributeData.
    /// </summary>
    /// <param name="attributeData">The AttributeData from which to create the SupportedOperationsAttributeData.</param>
    /// <returns>The SupportedOperationsAttributeData with attributes based on the provided AttributeData.</returns>
    private static SupportedOperationsAttributeData GetAttributeFromData(AttributeData attributeData)
    {
        return new SupportedOperationsAttributeData
        {
            Addition = CreateAttributeValue(attributeData, nameof(SupportedOperationsAttributeData.Addition)),
            Subtraction = CreateAttributeValue(attributeData, nameof(SupportedOperationsAttributeData.Subtraction)),
            Multiplication = CreateAttributeValue(attributeData, nameof(SupportedOperationsAttributeData.Multiplication)),
            Division = CreateAttributeValue(attributeData, nameof(SupportedOperationsAttributeData.Division)),
            Modulus = CreateAttributeValue(attributeData, nameof(SupportedOperationsAttributeData.Modulus))
        };

        static bool CreateAttributeValue(AttributeData? parentAttributeData, string property)
        {
            return parentAttributeData!.NamedArguments.FirstOrDefault(x => string.Equals(x.Key, property, StringComparison.Ordinal)).Value.Value is true;
        }
    }

    /// <summary>
    /// Processes the generation of code for a specific data type.
    /// </summary>
    /// <param name="data">The GeneratorData containing information about the data type.</param>
    /// <param name="options">The DomainPrimitiveGlobalOptions for code generation.</param>
    /// <param name="context">The SourceProductionContext for reporting diagnostics.</param>
    /// <returns>True if the code generation process was successful; otherwise, false.</returns>
    private static bool ProcessType(GeneratorData data, DomainPrimitiveGlobalOptions options, SourceProductionContext context)
    {
        var builder = new SourceCodeBuilder();
        var isSuccess = ProcessConstructor(data, builder, context);
        if (!isSuccess)
        {
            return false;
        }

        var validateMethod = data.TypeSymbol.GetMembersOfType<IMethodSymbol>().FirstOrDefault(x => string.Equals(x.Name, "Validate", StringComparison.Ordinal));
        if (validateMethod is not null)
            ExceptionHelper.VerifyException(validateMethod, context);

        Process(data, builder.ToString(), options, context);
        return true;
    }

    /// <summary>
    /// Processes the generator data and generates code for a specified class.
    /// </summary>
    /// <param name="data">The GeneratorData for the class.</param>
    /// <param name="ctorCode">The constructor code for the class.</param>
    /// <param name="options">The DomainPrimitiveGlobalOptions for the generator.</param>
    /// <param name="context">The SourceProductionContext for reporting diagnostics.</param>
    private static void Process(GeneratorData data, string ctorCode, DomainPrimitiveGlobalOptions options, SourceProductionContext context)
    {
        var modifiers = data.TypeSymbol.GetModifiers() ?? "public partial";

        if (!modifiers.Contains("partial"))
        {
            context.ReportDiagnostic(DiagnosticHelper.ClassMustBePartial(data.TypeSymbol.Locations.FirstOrDefault()));
        }

        var builder = new SourceCodeBuilder();
        var usings = new List<string>(3) { "System", "System.Numerics", "System.Diagnostics", "System.Runtime.CompilerServices" };

        if (data.ParentSymbols.Count > 0)
        {
            usings.Add(data.ParentSymbols[0].ContainingNamespace.ToDisplayString());
        }

        if (data.GenerateImplicitOperators)
        {
            usings.Add("System.Diagnostics.CodeAnalysis");
        }

        if (options.GenerateJsonConverters)
        {
            usings.Add("System.Text.Json.Serialization");
            usings.Add($"{data.Namespace}.Converters");
        }

        if (options.GenerateTypeConverters)
        {
            usings.Add("System.ComponentModel");
        }

        if (options.GenerateXmlSerialization)
        {
            usings.Add("System.Xml");
            usings.Add("System.Xml.Schema");
            usings.Add("System.Xml.Serialization");
            usings.Add("AltaSoft.DomainPrimitives");
        }

        var needsMathOperators = data.GenerateAdditionOperators || data.GenerateDivisionOperators ||
            data.GenerateMultiplyOperators || data.GenerateSubtractionOperators || data.GenerateModulusOperator;

        var isByteOrShort = data.ParentSymbols.Count == 0 && data.UnderlyingType.IsByteOrShort();

        if ((needsMathOperators && isByteOrShort) || data.UnderlyingType is DomainPrimitiveUnderlyingType.DateOnly or DomainPrimitiveUnderlyingType.TimeOnly)
        {
            usings.Add("AltaSoft.DomainPrimitives");
        }

        builder.AppendSourceHeader("AltaSoft DomainPrimitives Generator");

        builder.AppendUsings(usings);

        builder.AppendNamespace(data.Namespace);

        if (options.GenerateJsonConverters)
            builder.AppendLine($"[JsonConverter(typeof({data.ClassName + "JsonConverter"}))]");

        if (options.GenerateTypeConverters)
            builder.AppendLine($"[TypeConverter(typeof({data.ClassName + "TypeConverter"}))]");

        builder.Append("[UnderlyingPrimitiveType(typeof(").Append(data.PrimitiveTypeFriendlyName).AppendLine("))]");

        builder.AppendLine($"[DebuggerDisplay(\"{{{data.FieldName}}}\")]");

        if (!data.TypeSymbol.IsValueType)
            builder.AppendClass(false, modifiers, data.ClassName, CreateInheritedInterfaces(data, data.ClassName));
        else
            builder.AppendStruct(modifiers, data.ClassName, CreateInheritedInterfaces(data, data.ClassName));

        builder.AppendLine("/// <inheritdoc/>")
            .Append(" public Type GetUnderlyingPrimitiveType() => typeof(").Append(data.PrimitiveTypeFriendlyName).AppendLine(");");

        builder.AppendLine("/// <inheritdoc/>")
            .Append(" public object GetUnderlyingPrimitiveValue() => (").Append(data.PrimitiveTypeFriendlyName).AppendLine(")this;").NewLine();

        builder.AppendLines(ctorCode);

        if (needsMathOperators && isByteOrShort)
        {
            // Add int constructor
            builder.AppendComment("// Private constructor with 'int' value");
            builder.AppendLine($"private {data.ClassName}(int value) : this(value is >= {data.PrimitiveTypeFriendlyName}.MinValue and <= {data.PrimitiveTypeFriendlyName}.MaxValue ? ({data.PrimitiveTypeFriendlyName})value : throw new InvalidDomainValueException(\"The value has exceeded a {data.PrimitiveTypeFriendlyName} limit\"))")
                .OpenBracket()
                .CloseBracket()
                .NewLine();
        }

        MethodGeneratorHelper.GenerateEquatableOperators(data.ClassName, data.FieldName, data.TypeSymbol.IsValueType, builder);
        builder.NewLine();

        MethodGeneratorHelper.GenerateComparableCode(data.ClassName, data.FieldName, data.TypeSymbol.IsValueType, builder);
        builder.NewLine();

        if (data.GenerateImplicitOperators)
        {
            GenerateImplicitOperators(data, builder);
        }

        if (data.GenerateAdditionOperators)
        {
            MethodGeneratorHelper.GenerateAdditionCode(data.ClassName, data.FieldName, builder);
            builder.NewLine();
        }
        if (data.GenerateSubtractionOperators)
        {
            MethodGeneratorHelper.GenerateSubtractionCode(data.ClassName, data.FieldName, builder);
            builder.NewLine();
        }

        if (data.GenerateMultiplyOperators)
        {
            MethodGeneratorHelper.GenerateMultiplyCode(data.ClassName, data.FieldName, builder);
            builder.NewLine();
        }

        if (data.GenerateDivisionOperators)
        {
            MethodGeneratorHelper.GenerateDivisionCode(data.ClassName, data.FieldName, builder);
            builder.NewLine();
        }

        if (data.GenerateModulusOperator)
        {
            MethodGeneratorHelper.GenerateModulusCode(data.ClassName, data.FieldName, builder);
            builder.NewLine();
        }

        if (data.GenerateComparison)
        {
            MethodGeneratorHelper.GenerateComparisonCode(data.ClassName, data.FieldName, builder);
            builder.NewLine();
        }

        if (data.GenerateParsable)
        {
            MethodGeneratorHelper.GenerateParsable(data, builder);
            builder.NewLine();
        }

        if (data.GenerateSpanFormattable)
        {
            MethodGeneratorHelper.GenerateSpanFormattable(builder, data.FieldName);
            builder.NewLine();
        }

        if (data.GenerateUtf8SpanFormattable)
        {
            MethodGeneratorHelper.GenerateUtf8Formattable(builder, data.FieldName);
            builder.NewLine();
        }

        if (data.GenerateHashCode)
        {
            builder.AppendInheritDoc();
            builder.AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]");
            builder.AppendLine($"public override int GetHashCode() => {data.FieldName}.GetHashCode();");
            builder.NewLine();
        }

        if (data.GenerateConvertibles)
        {
            MethodGeneratorHelper.GenerateConvertibles(data, builder);
        }

        if (data.GenerateXmlSerializableMethods)
        {
            MethodGeneratorHelper.GenerateIXmlSerializableMethods(data, builder);
        }

        var baseType = data.ParentSymbols.Count == 0 ? data.PrimitiveTypeSymbol : data.ParentSymbols[0];
        var hasExplicitToStringMethod = data.TypeSymbol.GetMembersOfType<IMethodSymbol>().Any(x => string.Equals(x.Name, "ToString", StringComparison.Ordinal) && x is { IsStatic: true, Parameters.Length: 1 } &&
            x.Parameters[0].Type.Equals(baseType, SymbolEqualityComparer.Default));

        builder.AppendInheritDoc();
        builder.AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]");
        if (hasExplicitToStringMethod)
            builder.AppendLine($"public override string ToString() => ToString({data.FieldName});");
        else
            builder.AppendLine($"public override string ToString() => {data.FieldName}.ToString();");

        builder.CloseBracket();

        context.AddSource(data.ClassName + ".g", builder.ToString());

        return;

        static string CreateInheritedInterfaces(GeneratorData data, string className)
        {
            var sb = new StringBuilder(8096);

            sb.Append("IEquatable<").Append(className).Append('>');

            AppendInterface(sb, nameof(IComparable));
            AppendInterface(sb, "IComparable<").Append(className).Append('>');

            if (data.GenerateAdditionOperators)
            {
                AppendInterface(sb, "IAdditionOperators<").Append(className).Append(", ").Append(className).Append(", ").Append(className).Append('>');
            }

            if (data.GenerateSubtractionOperators)
            {
                AppendInterface(sb, "ISubtractionOperators<").Append(className).Append(", ").Append(className).Append(", ").Append(className).Append('>');
            }

            if (data.GenerateMultiplyOperators)
            {
                AppendInterface(sb, "IMultiplyOperators<").Append(className).Append(", ").Append(className).Append(", ").Append(className).Append('>');
            }

            if (data.GenerateDivisionOperators)
            {
                AppendInterface(sb, "IDivisionOperators<").Append(className).Append(", ").Append(className).Append(", ").Append(className).Append('>');
            }

            if (data.GenerateModulusOperator)
            {
                AppendInterface(sb, "IModulusOperators<").Append(className).Append(", ").Append(className).Append(", ").Append(className).Append('>');
            }

            if (data.GenerateComparison)
            {
                AppendInterface(sb, "IComparisonOperators<").Append(className).Append(", ").Append(className).Append(", ").Append("bool>");
            }

            if (data.GenerateSpanFormattable)
            {
                AppendInterface(sb, "ISpanFormattable");
            }

            if (data.GenerateParsable)
            {
                AppendInterface(sb, "IParsable<").Append(className).Append('>');
            }

            if (data.GenerateConvertibles)
            {
                AppendInterface(sb, nameof(IConvertible));
            }

            if (data.GenerateXmlSerializableMethods)
            {
                AppendInterface(sb, nameof(IXmlSerializable));
            }

            if (data.GenerateUtf8SpanFormattable)
            {
                sb.AppendLine().Append("#if NET8_0_OR_GREATER");
                AppendInterface(sb, "IUtf8SpanFormattable");
                sb.AppendLine().Append("#endif");
            }

            return sb.ToString();

            static StringBuilder AppendInterface(StringBuilder sb, string interfaceName)
                => sb.AppendLine().Append(SourceCodeBuilder.GetIndentation(2)).Append(", ").Append(interfaceName);
        }
    }

    /// <summary>
    /// Generates implicit operators for a specified class.
    /// </summary>
    /// <param name="data">The GeneratorData for the class.</param>
    /// <param name="builder">The SourceCodeBuilder for generating source code.</param>
    private static void GenerateImplicitOperators(GeneratorData data, SourceCodeBuilder builder)
    {
        var friendlyName = data.PrimitiveTypeFriendlyName;
        var type = data.PrimitiveTypeSymbol;
        var className = data.ClassName;

        // From Underlying to our type
        if (data.TypeSymbol.IsValueType)
        {
            builder.AppendSummary($"Implicit conversion from <see cref = \"{friendlyName}\"/> to <see cref = \"{className}\"/>")
                .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
                .Append($"public static implicit operator {className}({friendlyName} value)").AppendLine(" => new(value);")
            .NewLine();
        }

        builder.AppendSummary($"Implicit conversion from <see cref = \"{friendlyName}\"/> (nullable) to <see cref = \"{className}\"/> (nullable)")
            .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
            .AppendLine("[return: NotNullIfNotNull(nameof(value))]")
            .Append($"public static implicit operator {className}?({friendlyName}? value)")
            .AppendLine($" => value is null ? null : new(value{(type.IsValueType ? ".Value" : "")});")
            .NewLine();

        // From our type to underlying type
        if (data.TypeSymbol.IsValueType)
        {
            builder.AppendSummary($"Implicit conversion from <see cref = \"{className}\"/> to <see cref = \"{friendlyName}\"/>")
            .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
            .Append($"public static implicit operator {friendlyName}({className} value)").AppendLine($" => ({friendlyName})value.{data.FieldName};")
            .NewLine();
        }

        builder.AppendSummary($"Implicit conversion from <see cref = \"{className}\"/> (nullable) to <see cref = \"{friendlyName}\"/> (nullable)")
            .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
            .AppendLine("[return: NotNullIfNotNull(nameof(value))]")
            .Append($"public static implicit operator {friendlyName}?({className}? value)")
            .AppendLine($" => value is null ? null : ({friendlyName}?)value{(type.IsValueType ? ".Value" : "")}.{data.FieldName};")
            .NewLine();

        if (data.ParentSymbols.Count != 0)
        {
            var parentClassName = data.ParentSymbols[0].Name;

            if (data.TypeSymbol.IsValueType)
            {
                builder.AppendSummary($"Implicit conversion from <see cref = \"{parentClassName}\"/> to <see cref = \"{className}\"/>")
                .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
                .Append($"public static implicit operator {className}({parentClassName} value)")
                .AppendLine(" => new(value);")
                .NewLine();
            }

            builder.AppendSummary($"Implicit conversion from <see cref = \"{parentClassName}\"/> (nullable) to <see cref = \"{className}\"/> (nullable)")
                .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
                .AppendLine("[return: NotNullIfNotNull(nameof(value))]")
                .Append($"public static implicit operator {className}?({parentClassName}? value)")
                .AppendLine($" => value is null ? null : ({className}?)value{(type.IsValueType ? ".Value" : "")};")
                .NewLine();
        }

        if (data.UnderlyingType is DomainPrimitiveUnderlyingType.DateOnly or DomainPrimitiveUnderlyingType.TimeOnly)
        {
            builder.AppendSummary($"Implicit conversion from <see cref = \"{className}\"/> to <see cref = \"DateTime\"/>")
                .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
                .Append($"public static implicit operator DateTime({className} value)")
                .AppendLine($" => (({friendlyName})value.{data.FieldName}).ToDateTime();")
                .NewLine();

            builder.AppendSummary($"Implicit conversion from <see cref = \"DateTime\"/> to <see cref = \"{className}\"/>")
                .AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]")
                .Append($"public static implicit operator {className}(DateTime value)")
                .AppendLine($" => {data.UnderlyingType}.FromDateTime(value);")
                .NewLine();
        }
    }

    /// <summary>
    /// Processes the constructor for a specified class.
    /// </summary>
    /// <param name="data">The GeneratorData containing information about the data type.</param>
    /// <param name="builder">The SourceCodeBuilder for generating source code.</param>
    /// <param name="context">The SourceProductionContext for reporting diagnostics.</param>
    /// <returns>A boolean indicating whether the constructor processing was successful.</returns>
    private static bool ProcessConstructor(GeneratorData data, SourceCodeBuilder builder, SourceProductionContext context)
    {
        var type = data.TypeSymbol;
        if (type.HasDefaultConstructor(out _))
        {
            var emptyCtor = type.Constructors.First(x => x.IsPublic() && x.Parameters.Length == 0);
            context.ReportDiagnostic(DiagnosticHelper.ClassHasDefaultConstructor(type.Name, emptyCtor.Locations.FirstOrDefault()));
            return false;
        }

        var interfaceGenericType = (INamedTypeSymbol)type.Interfaces.First(x => x.IsDomainValue()).TypeArguments[0];
        var ctorWithParam = type.Constructors
            .FirstOrDefault(x => x.IsPublic() && x.Parameters.Length == 1 && (x.Parameters[0].Type as INamedTypeSymbol)!.Equals(interfaceGenericType, SymbolEqualityComparer.Default));

        if (ctorWithParam is not null)
        {
            context.ReportDiagnostic(DiagnosticHelper.ClassMustNotHaveConstructorWithParam(type.Name, type.Locations.FirstOrDefault()));
            return false;
        }

        var underlyingTypeName = interfaceGenericType.GetFriendlyName();

        if (data.GenerateIsInitializedField)
        {
            builder.AppendLine($"private {underlyingTypeName} _valueOrDefault => _isInitialized ? _value : Default;");
        }

        builder.AppendLine("[DebuggerBrowsable(DebuggerBrowsableState.Never)]");
        builder.AppendLine($"private readonly {underlyingTypeName} _value;");

        if (data.GenerateIsInitializedField)
        {
            builder.AppendLine("[DebuggerBrowsable(DebuggerBrowsableState.Never)]");
            builder.AppendLine("private readonly bool _isInitialized;");
        }
        builder.NewLine();

        builder.AppendSummary($"Initializes a new instance of the <see cref=\"{type.Name}\"/> class by validating the specified <see cref=\"{underlyingTypeName}\"/> value using <see cref=\"Validate\"/> static method.");
        builder.AppendParamDescription("value", "The value to be validated.");

        builder.AppendLine($"public {type.Name}({underlyingTypeName} value)")
            .OpenBracket()
            .AppendLine("Validate(value);")
            .AppendLine("_value = value;")
            .AppendLineIf(data.GenerateIsInitializedField, "_isInitialized = true;")
            .CloseBracket();

        var primitiveTypeIsValueType = data.PrimitiveTypeSymbol.IsValueType;
        if (!primitiveTypeIsValueType)
            builder.AppendLine("#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.");

        builder.NewLine()
            .AppendInheritDoc()
            .AppendLine("[Obsolete(\"Domain primitive cannot be created using empty Ctor\", true)]");

        builder.Append("public ").Append(type.Name).AppendLine("()")
            .OpenBracket()
            .AppendLine("_value = Default;")
            .AppendLineIf(data.GenerateIsInitializedField, "_isInitialized = true;")
            .CloseBracket();

        if (!primitiveTypeIsValueType)
            builder.AppendLine("#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.");

        return true;
    }
}
