using AltaSoft.DomainPrimitives.Abstractions;
using AltaSoft.DomainPrimitives.Generator.Extensions;
using AltaSoft.DomainPrimitives.Generator.Helpers;
using AltaSoft.DomainPrimitives.Generator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Microsoft.CodeAnalysis.CSharp;

namespace AltaSoft.DomainPrimitives.Generator;

/// <summary>
/// A static class responsible for executing the generation of code for domain primitive types.
/// </summary>
internal static class Executor
{
	private static INamedTypeSymbol? s_nullableTypeSymbol;
	private static readonly Dictionary<INamedTypeSymbol, SupportedOperationsAttribute> CachedOperationsAttributes = new(SymbolEqualityComparer.Default);

	/// <summary>
	/// Executes the AltaSoft.DomainPrimitiveGenerator generator.
	/// </summary>
	/// <param name="compilation">The compilation being built.</param>
	/// <param name="types">The collection of TypeDeclarationSyntax representing types to process.</param>
	/// <param name="analyzerOptions">The AnalyzerConfigOptionsProvider to access analyzer options.</param>
	/// <param name="context">The SourceProductionContext for reporting diagnostics.</param>
	internal static void Execute(Compilation compilation, ImmutableArray<TypeDeclarationSyntax> types,
		AnalyzerConfigOptionsProvider analyzerOptions, SourceProductionContext context)
	{
		s_nullableTypeSymbol = compilation.GetSpecialType(SpecialType.System_Nullable_T);

		CompilationExt.InitializeTypes();

		if (types.IsDefaultOrEmpty)
			return;

		var projectNs = compilation.AssemblyName ?? throw new Exception("Assembly name must be provided");

		var globalOptions = GetGlobalOptions(analyzerOptions);

		var swaggerTypes = new List<(string, string, bool, string?, INamedTypeSymbol primitiveType)>(types.Length);
		try
		{
			foreach (var type in types)
			{
				var classSemantic = compilation.GetSemanticModel(type.SyntaxTree);

				if (ModelExtensions.GetDeclaredSymbol(classSemantic, type) is not INamedTypeSymbol @class)
					continue;

				var generatorData = CreateGeneratorData(@class, context, globalOptions);
				if (generatorData is null)
					continue;

				if (generatorData.PrimitiveTypeSymbol.IsValueType && !generatorData.Type.IsValueType)
				{
					context.ReportDiagnostic(DiagnosticHelper.TypeShouldBeValueType(generatorData.ClassName, generatorData.PrimitiveTypeFriendlyName, @class.Locations.FirstOrDefault()));
				}

				var isSuccess = ProcessType(generatorData, globalOptions, context);

				if (!isSuccess)
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
					swaggerTypes.Add((generatorData.Namespace, generatorData.ClassName, generatorData.Type.IsValueType, generatorData.SerializationFormat, generatorData.PrimitiveTypeSymbol));
			}

			MethodGeneratorHelper.AddSwaggerOptions(projectNs, swaggerTypes, context);
		}
		catch (Exception ex)
		{
			context.ReportDiagnostic(DiagnosticHelper.GeneralError(Location.None, ex));
		}
		finally
		{
			CachedOperationsAttributes.Clear();
			CompilationExt.ClearTypes();
		}
	}

	/// <summary>
	/// Gets the global options for the AltaSoft.DomainPrimitiveGenerator generator.
	/// </summary>
	/// <param name="analyzerOptions">The AnalyzerConfigOptionsProvider to access analyzer options.</param>
	/// <returns>The DomainPrimitiveGlobalOptions for the generator.</returns>
	private static DomainPrimitiveGlobalOptions GetGlobalOptions(AnalyzerConfigOptionsProvider analyzerOptions)
	{
		var result = new DomainPrimitiveGlobalOptions();

		if (analyzerOptions.GlobalOptions.TryGetValue("build_property.DomainPrimitiveGenerator_GenerateJsonConverters", out var value)
			&& bool.TryParse(value, out var generateJsonConverters))
		{
			result.GenerateJsonConverters = generateJsonConverters;
		}

		if (analyzerOptions.GlobalOptions.TryGetValue("build_property.DomainPrimitiveGenerator_GenerateTypeConverters", out value)
			&& bool.TryParse(value, out var generateTypeConverter))
		{
			result.GenerateTypeConverters = generateTypeConverter;
		}

		if (analyzerOptions.GlobalOptions.TryGetValue("build_property.DomainPrimitiveGenerator_GenerateSwaggerConverters", out value)
			&& bool.TryParse(value, out var generateSwaggerConverters))
		{
			result.GenerateSwaggerConverters = generateSwaggerConverters;
		}

		if (analyzerOptions.GlobalOptions.TryGetValue("build_property.DomainPrimitiveGenerator_GenerateXmlSerialization", out value)
			&& bool.TryParse(value, out var generateXmlSerialization))
		{
			result.GenerateXmlSerialization = generateXmlSerialization;
		}

		return result;
	}

	/// <summary>
	/// Creates generator data for a specified class symbol.
	/// </summary>
	/// <param name="type">The INamedTypeSymbol representing the class.</param>
	/// <param name="context">The SourceProductionContext for reporting diagnostics.</param>
	/// <param name="globalOptions">The global options for generating source.</param>
	/// <returns>The GeneratorData for the class or null if not applicable.</returns>
	private static GeneratorData? CreateGeneratorData(INamedTypeSymbol type, SourceProductionContext context, DomainPrimitiveGlobalOptions globalOptions)
	{
		var interfaceType = type.AllInterfaces.First(x => x.IsDomainValue());

		if (interfaceType.TypeArguments[0] is not INamedTypeSymbol primitiveType)
		{
			context.ReportDiagnostic(DiagnosticHelper.InvalidBaseTypeSpecified(type.Locations.FirstOrDefault()));
			return null;
		}

		var parentSymbols = new List<INamedTypeSymbol>();
		var (category, typeSymbol) = primitiveType.GetUnderlyingPrimitiveCategory(parentSymbols);

		if (category == PrimitiveCategory.Other)
		{
			context.ReportDiagnostic(DiagnosticHelper.InvalidBaseTypeSpecified(type.Locations.FirstOrDefault()));
			return null;
		}

		NumericType? numericType = null;
		DateType? dateType = null;

		switch (category)
		{
			case PrimitiveCategory.Numeric:
				numericType = typeSymbol.GetNumericTypeFromNamedTypeSymbol();
				break;

			case PrimitiveCategory.DateTime:
				dateType = typeSymbol.GetDateTypeFromNamedTypeSymbol();
				break;
		}

		var hasOverridenHashCode = type.GetMembersOfType<IMethodSymbol>().Any(x => x.OverriddenMethod?.Name == "GetHashCode");

		var generateIsInitializedField = true;
		var defaultProperty = type.GetMembersOfType<IPropertySymbol>().FirstOrDefault(x => x.Name == "Default");
		if (defaultProperty is not null)
		{
			generateIsInitializedField = !DefaultPropertyReturnsDefaultValue(defaultProperty, category, numericType, dateType);
		}

		var generatorData = new GeneratorData
		{
			FieldName = generateIsInitializedField ? "_valueOrDefault" : "_value",
			GenerateIsInitializedField = generateIsInitializedField,
			GenerateHashCode = !hasOverridenHashCode,
			Category = category,
			Type = type,
			PrimitiveTypeSymbol = typeSymbol,
			PrimitiveTypeFriendlyName = typeSymbol.GetFriendlyName(s_nullableTypeSymbol!),
			Namespace = type.ContainingNamespace.ToDisplayString(),
			NumericType = numericType,
			DateType = dateType,
			GenerateImplicitOperators = true,
			ParentSymbols = parentSymbols,
			GenerateConvertibles = category != PrimitiveCategory.Guid
		};

		var attributes = type.GetAttributes();
		var attributeData = attributes.FirstOrDefault(x => x.AttributeClass?.ToDisplayString() == typeof(SupportedOperationsAttribute).FullName);
		var serializationAttribute = attributes.FirstOrDefault(x => x.AttributeClass?.ToDisplayString() == typeof(SerializationFormatAttribute).FullName);

		if (numericType is null && attributeData is not null)
		{
			context.ReportDiagnostic(DiagnosticHelper.TypeMustBeNumericType(attributeData.GetAttributeLocation(), type.Name));
			return null;
		}

		if (serializationAttribute is not null && dateType is null)
		{
			context.ReportDiagnostic(DiagnosticHelper.TypeMustBeDateType(serializationAttribute.GetAttributeLocation(), type.Name));
			return null;
		}

		if (serializationAttribute is not null && serializationAttribute.ConstructorArguments.Length != 0)
		{
			var value = serializationAttribute.ConstructorArguments[0];
			generatorData.SerializationFormat = value.Value?.ToString();
		}

		var supportedOperationsAttribute = numericType is null ? null : GetSupportedOperationsAttributes(type, numericType.Value, parentSymbols);

		generatorData.GenerateAdditionOperators = category == PrimitiveCategory.Numeric && supportedOperationsAttribute!.Addition && !type.ImplementsInterface("System.Numerics.IAdditionOperators");

		generatorData.GenerateSubtractionOperators = category == PrimitiveCategory.Numeric && supportedOperationsAttribute!.Subtraction && !type.ImplementsInterface("System.Numerics.ISubtractionOperators");

		generatorData.GenerateDivisionOperators = category == PrimitiveCategory.Numeric && supportedOperationsAttribute!.Division && !type.ImplementsInterface("System.Numerics.IDivisionOperators");

		generatorData.GenerateMultiplyOperators = category == PrimitiveCategory.Numeric && supportedOperationsAttribute!.Multiplication && !type.ImplementsInterface("System.Numerics.IMultiplyOperators");

		generatorData.GenerateModulusOperator = category == PrimitiveCategory.Numeric && supportedOperationsAttribute!.Modulus && !type.ImplementsInterface("System.Numerics.IModulusOperator");

		generatorData.GenerateComparable = category is not PrimitiveCategory.Boolean && !type.ImplementsInterface("System.IComparable");

		generatorData.GenerateParsable = !type.ImplementsInterface("System.IParsable");

		generatorData.GenerateComparison = (category is PrimitiveCategory.Numeric or PrimitiveCategory.DateTime or PrimitiveCategory.Char) && !type.ImplementsInterface("System.Numerics.IComparisonOperators");

		generatorData.GenerateSpanFormattable = (category is PrimitiveCategory.DateTime or PrimitiveCategory.Guid) && !type.ImplementsInterface("System.ISpanFormattable");

		generatorData.GenerateUtf8SpanFormattable = primitiveType.ImplementsInterface("System.IUtf8SpanFormattable") && !type.ImplementsInterface("System.IUtf8SpanFormattable");

		generatorData.GenerateEquatableOperators = !type.ImplementsInterface("System.IEquatable");

		generatorData.GenerateXmlSerializableMethods = globalOptions.GenerateXmlSerialization;

		return generatorData;
	}

	private static bool DefaultPropertyReturnsDefaultValue(IPropertySymbol property, PrimitiveCategory category, NumericType? numericType, DateType? dateType)
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
				var defaultValue = GetDefaultValue();
				if (defaultValue is null)
					return literal.Token.Value is null;

				return defaultValue.Equals(literal.Token.Value);

			// For more complex expressions, additional analysis is required
			default:
				return false;
		}

		object? GetDefaultValue()
		{
			return category switch
			{
				PrimitiveCategory.String => default(string?),
				PrimitiveCategory.Boolean => false,
				PrimitiveCategory.Char => default(char),
				PrimitiveCategory.Guid => default(Guid),

				PrimitiveCategory.Numeric =>
					numericType switch
					{
						NumericType.Byte => default(byte),
						NumericType.SByte => default(sbyte),
						NumericType.Int16 => default(short),
						NumericType.Int32 => default(int),
						NumericType.Int64 => default(long),
						NumericType.UInt16 => default(ushort),
						NumericType.UInt32 => default(uint),
						NumericType.UInt64 => default(ulong),
						NumericType.Decimal => default(decimal),
						NumericType.Double => default(double),
						NumericType.Single => default(float),
						_ => new DummyValueObject()
					},

				PrimitiveCategory.DateTime =>
					dateType switch
					{
						DateType.DateOnly => new DateTime(1, 1, 1),
						DateType.DateTime => default(DateTime),
						DateType.TimeOnly => new DateTime(1, 1, 1, 0, 0, 0),
						DateType.DateTimeOffset => default(DateTimeOffset),
						DateType.TimeSpan => default(TimeSpan),
						_ => new DummyValueObject()
					},

				_ => new DummyValueObject()
			};
		}
	}

	private readonly struct DummyValueObject;

	/// <summary>
	/// Retrieves the SupportedOperationsAttribute for a specified class, considering inheritance.
	/// </summary>
	/// <param name="class">The INamedTypeSymbol representing the class.</param>
	/// <param name="numericType">The NumericType associated with the class.</param>
	/// <param name="parentSymbols">The list of parent symbols for the class.</param>
	/// <returns>The combined SupportedOperationsAttribute for the class and its inherited types.</returns>
	private static SupportedOperationsAttribute GetSupportedOperationsAttributes(INamedTypeSymbol @class, NumericType numericType, List<INamedTypeSymbol> parentSymbols)
	{
		return CreateCombinedAttribute(@class, numericType, parentSymbols.Count);

		static SupportedOperationsAttribute CreateCombinedAttribute(INamedTypeSymbol @class, NumericType numericType, int parentCount)
		{
			if (CachedOperationsAttributes.TryGetValue(@class, out var parentAttribute))
			{
				return parentAttribute;
			}

			var attributeData = @class.GetAttributes()
				.FirstOrDefault(x => x.AttributeClass?.ToDisplayString() == typeof(SupportedOperationsAttribute).FullName);

			var attribute = attributeData is null ? null : GetAttributeFromData(attributeData);

			if (parentCount == 0)
			{
				attribute ??= GetDefaultAttributeData(numericType);
				CachedOperationsAttributes[@class] = attribute;

				return attribute;
			}

			var parentType = @class.Interfaces.First(x => x.IsDomainValue());

			var attr = CombineAttribute(attribute, CreateCombinedAttribute((parentType.TypeArguments[0] as INamedTypeSymbol)!, numericType, parentCount - 1));

			CachedOperationsAttributes[@class] = attr;
			return attr;
		}
		static SupportedOperationsAttribute CombineAttribute(SupportedOperationsAttribute? attribute, SupportedOperationsAttribute parentAttribute)
		{
			if (attribute is null)
				return parentAttribute;

			return new SupportedOperationsAttribute
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
	/// Gets the default SupportedOperationsAttribute based on the given NumericType.
	/// </summary>
	/// <param name="numericType">The NumericType for which to determine default attribute values.</param>
	/// <returns>The default SupportedOperationsAttribute with attributes set based on the NumericType.</returns>
	private static SupportedOperationsAttribute GetDefaultAttributeData(NumericType? numericType)
	{
		var @default = DefaultAttributeValue(numericType!.Value);

		return new SupportedOperationsAttribute
		{
			Addition = @default,
			Subtraction = @default,
			Multiplication = @default,
			Division = @default,
			Modulus = @default
		};

		static bool DefaultAttributeValue(NumericType type) =>
			type switch
			{
				NumericType.Byte => false,
				NumericType.SByte => false,
				NumericType.Int16 => false,
				NumericType.UInt16 => false,
				NumericType.Int32 => true,
				NumericType.UInt32 => true,
				NumericType.Int64 => true,
				NumericType.UInt64 => true,
				//NumericType.Int128 => true,
				//NumericType.UInt128 => true,
				NumericType.Decimal => true,
				NumericType.Double => true,
				NumericType.Single => true,
				_ => true
			};
	}

	/// <summary>
	/// Creates a SupportedOperationsAttribute from the provided AttributeData.
	/// </summary>
	/// <param name="attributeData">The AttributeData from which to create the SupportedOperationsAttribute.</param>
	/// <returns>The SupportedOperationsAttribute with attributes based on the provided AttributeData.</returns>
	private static SupportedOperationsAttribute GetAttributeFromData(AttributeData attributeData)
	{
		return new SupportedOperationsAttribute
		{
			Addition = CreateAttributeValue(attributeData, nameof(SupportedOperationsAttribute.Addition)),
			Subtraction = CreateAttributeValue(attributeData, nameof(SupportedOperationsAttribute.Subtraction)),
			Multiplication = CreateAttributeValue(attributeData, nameof(SupportedOperationsAttribute.Multiplication)),
			Division = CreateAttributeValue(attributeData, nameof(SupportedOperationsAttribute.Division)),
			Modulus = CreateAttributeValue(attributeData, nameof(SupportedOperationsAttribute.Modulus))
		};

		static bool CreateAttributeValue(AttributeData? parentAttributeData, string property)
		{
			return parentAttributeData!.NamedArguments.FirstOrDefault(x => x.Key == property).Value.Value is true;
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
		var sb = new SourceCodeBuilder();
		var isSuccess = ProcessConstructor(data, sb, context);
		if (!isSuccess)
		{
			return false;
		}

		var validateMethod = data.Type.GetMembersOfType<IMethodSymbol>().FirstOrDefault(x => x.Name == "Validate");
		if (validateMethod is not null)
			ExceptionHelper.VerifyException(validateMethod, context);

		Process(data, sb.ToString(), options, context);
		return true;
	}

	/// <summary>
	/// Processes the generator data and generates code for a specified class.
	/// </summary>
	/// <param name="data">The GeneratorData for the class.</param>
	/// <param name="ctorCode">The constructor code for the class.</param>
	/// <param name="options">The DomainPrimitiveGlobalOptions for the generator.</param>
	/// <param name="context">The SourceProductionContext for reporting diagnostics.</param>
	private static void Process(GeneratorData data, string? ctorCode, DomainPrimitiveGlobalOptions options, SourceProductionContext context)
	{
		var modifiers = data.Type.GetModifiers() ?? "public partial";

		if (!modifiers.Contains("partial"))
		{
			context.ReportDiagnostic(DiagnosticHelper.ClassMustBePartial(data.Type.Locations.FirstOrDefault()));
		}

		var sb = new SourceCodeBuilder();
		var usings = new List<string>(3) { "System", "System.Numerics", "System.Diagnostics" };

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
			usings.Add("AltaSoft.DomainPrimitives.Abstractions");
		}

		var needsMathOperators = data.GenerateAdditionOperators || data.GenerateDivisionOperators ||
										 data.GenerateMultiplyOperators || data.GenerateSubtractionOperators ||
										 data.GenerateModulusOperator;

		var isByteOrShort = data.ParentSymbols.Count == 0 &&
			data.NumericType is NumericType.Byte or NumericType.SByte or NumericType.Int16 or NumericType.UInt16;

		if ((needsMathOperators && isByteOrShort) || data.DateType is DateType.DateOnly or DateType.TimeOnly)
		{
			usings.Add("AltaSoft.DomainPrimitives.Abstractions");
		}

		sb.AddSourceHeader();
		sb.AppendUsings(usings);

		sb.AppendNamespace(data.Namespace);

		if (options.GenerateJsonConverters)
			sb.AppendLine($"[JsonConverter(typeof({data.ClassName + "JsonConverter"}))]");

		if (options.GenerateTypeConverters)
			sb.AppendLine($"[TypeConverter(typeof({data.ClassName + "TypeConverter"}))]");

		sb.AppendLine($"[DebuggerDisplay(\"{{{data.FieldName}}}\")]");

		if (!data.Type.IsValueType)
			sb.AppendClass(modifiers, data.ClassName, CreateInheritedInterfaces(data, data.ClassName));
		else
			sb.AppendStruct(modifiers, data.ClassName, CreateInheritedInterfaces(data, data.ClassName));

		if (ctorCode is not null)
		{
			sb.AppendLines(ctorCode);
		}

		if (data is
			{
				GenerateComparable: false, GenerateAdditionOperators: false, GenerateDivisionOperators: false,
				GenerateMultiplyOperators: false, GenerateSubtractionOperators: false, GenerateModulusOperator: false, GenerateComparison: false,
				GenerateImplicitOperators: false, GenerateParsable: false, GenerateEquatableOperators: false, GenerateHashCode: false,
				GenerateSpanFormattable: false, GenerateConvertibles: false, GenerateUtf8SpanFormattable: false, GenerateXmlSerializableMethods: false
			})

		{
			return;
		}

		if (needsMathOperators && isByteOrShort)
		{
			sb.AppendLine($"private {data.ClassName}(int value)")
				.OpenBracket()
				.AppendLine($"if (value is < {data.PrimitiveTypeFriendlyName}.MinValue or > {data.PrimitiveTypeFriendlyName}.MaxValue)")
				.AppendLine($"\tthrow new InvalidDomainValueException(\"The value has exceeded a {data.PrimitiveTypeFriendlyName} limit\");")
				.NewLine()
				.AppendLine($"var checkedValue = ({data.PrimitiveTypeFriendlyName})value;")
				.AppendLine("Validate(checkedValue);")
				.AppendLine("_value = checkedValue;")
				.CloseBracket()
				.NewLine();
		}

		if (data.GenerateImplicitOperators)
		{
			GenerateImplicitOperators(data, sb);
		}

		if (data.GenerateAdditionOperators)
		{
			MethodGeneratorHelper.GenerateAdditionCode(data.ClassName, data.FieldName, sb);
			sb.NewLine();
		}
		if (data.GenerateSubtractionOperators)
		{
			MethodGeneratorHelper.GenerateSubtractionCode(data.ClassName, data.FieldName, sb);
			sb.NewLine();
		}

		if (data.GenerateMultiplyOperators)
		{
			MethodGeneratorHelper.GenerateMultiplyCode(data.ClassName, data.FieldName, sb);
			sb.NewLine();
		}

		if (data.GenerateDivisionOperators)
		{
			MethodGeneratorHelper.GenerateDivisionCode(data.ClassName, data.FieldName, sb);
			sb.NewLine();
		}

		if (data.GenerateModulusOperator)
		{
			MethodGeneratorHelper.GenerateModulusCode(data.ClassName, data.FieldName, sb);
			sb.NewLine();
		}

		if (data.GenerateComparable)
		{
			MethodGeneratorHelper.GenerateComparableCode(data.ClassName, data.FieldName, data.Type.IsValueType, sb);
			sb.NewLine();
		}

		if (data.GenerateComparison)
		{
			MethodGeneratorHelper.GenerateComparisonCode(data.ClassName, data.FieldName, sb);
			sb.NewLine();
		}

		if (data.GenerateParsable)
		{
			MethodGeneratorHelper.GenerateParsable(data, sb);
			sb.NewLine();
		}

		if (data.GenerateSpanFormattable)
		{
			MethodGeneratorHelper.GenerateSpanFormattable(sb, data.FieldName);
			sb.NewLine();
		}

		if (data.GenerateUtf8SpanFormattable)
		{
			MethodGeneratorHelper.GenerateUtf8Formattable(sb, data.FieldName);
			sb.NewLine();
		}

		if (data.GenerateEquatableOperators)
		{
			MethodGeneratorHelper.GenerateEquatableOperators(data.ClassName, data.FieldName, data.Type.IsValueType, sb);
			sb.NewLine();
		}

		if (data.GenerateHashCode)
		{
			sb.AppendInheritDoc();
			sb.AppendLine($"public override int GetHashCode() => {data.FieldName}.GetHashCode();");
		}

		if (data.GenerateConvertibles)
		{
			MethodGeneratorHelper.GenerateConvertibles(data, sb);
		}

		if (data.GenerateXmlSerializableMethods)
		{
			MethodGeneratorHelper.GenerateIXmlSerializableMethods(data, sb);
		}

		var baseType = data.ParentSymbols.Count == 0 ? data.PrimitiveTypeSymbol : data.ParentSymbols[0];
		var hasExplicitToStringMethod = data.Type.GetMembersOfType<IMethodSymbol>().Any(x =>
			x.Name == "ToString" && x is { IsStatic: true, Parameters.Length: 1 } &&
			x.Parameters[0].Type.Equals(baseType, SymbolEqualityComparer.Default));

		sb.AppendInheritDoc();
		if (hasExplicitToStringMethod)
			sb.AppendLine($"public override string ToString() => ToString({data.FieldName});").NewLine();
		else
			sb.AppendLine($"public override string ToString() => {data.FieldName}.ToString();").NewLine();

		sb.CloseBracket();

		context.AddSource(data.ClassName + ".g", sb.ToString());

		return;

		static string CreateInheritedInterfaces(GeneratorData data, string className)
		{
			var sb = new StringBuilder();

			if (data.GenerateAdditionOperators)
			{
				AppendInterface(sb, "IAdditionOperators<").Append(className).Append(", ").Append(className).Append(", ").Append(className).Append(">");
			}

			if (data.GenerateSubtractionOperators)
			{
				AppendInterface(sb, "ISubtractionOperators<").Append(className).Append(", ").Append(className).Append(", ").Append(className).Append(">");
			}

			if (data.GenerateMultiplyOperators)
			{
				AppendInterface(sb, "IMultiplyOperators<").Append(className).Append(", ").Append(className).Append(", ").Append(className).Append(">");
			}

			if (data.GenerateDivisionOperators)
			{
				AppendInterface(sb, "IDivisionOperators<").Append(className).Append(", ").Append(className).Append(", ").Append(className).Append(">");
			}

			if (data.GenerateModulusOperator)
			{
				AppendInterface(sb, "IModulusOperators<").Append(className).Append(", ").Append(className).Append(", ").Append(className).Append(">");
			}

			if (data.GenerateComparison)
			{
				AppendInterface(sb, "IComparisonOperators<").Append(className).Append(", ").Append(className).Append(", ").Append("bool>");
			}

			if (data.GenerateSpanFormattable)
			{
				AppendInterface(sb, "ISpanFormattable");
			}

			if (data.GenerateUtf8SpanFormattable)
			{
				if (sb.Length != 0)
					sb.AppendLine(",");
				else
					sb.AppendLine();

				sb.AppendLine("#if NET8_0_OR_GREATER")
					.AppendLine("IUtf8SpanFormattable")
					.AppendLine("#endif");
			}

			if (data.GenerateComparable)
			{
				AppendInterface(sb, nameof(IComparable));
				AppendInterface(sb, "IComparable<").Append(className).Append('>');
			}

			if (data.GenerateParsable)
			{
				AppendInterface(sb, "IParsable<").Append(className).Append('>');
			}

			if (data.GenerateEquatableOperators)
			{
				AppendInterface(sb, "IEquatable<").Append(className).Append('>');
			}

			if (data.GenerateConvertibles)
			{
				AppendInterface(sb, nameof(IConvertible));
			}

			if (data.GenerateXmlSerializableMethods)
			{
				AppendInterface(sb, nameof(IXmlSerializable));
			}

			return sb.ToString();

			static StringBuilder AppendInterface(StringBuilder sb, string interfaceName)
			{
				if (sb.Length != 0)
					sb.AppendLine(",");
				else
					sb.AppendLine();

				return sb.Append("\t\t").Append(interfaceName);
			}
		}
	}

	/// <summary>
	/// Generates implicit operators for a specified class.
	/// </summary>
	/// <param name="data">The GeneratorData for the class.</param>
	/// <param name="sb">The SourceCodeBuilder for generating source code.</param>
	private static void GenerateImplicitOperators(GeneratorData data, SourceCodeBuilder sb)
	{
		var friendlyName = data.PrimitiveTypeFriendlyName;

		if (data.Type.IsValueType)
		{
			sb.AppendSummary($"<summary>Implicit conversion from <see cref = \"{friendlyName}\"/> to <see cref = \"{data.ClassName}\"/></summary>")
				.Append($"public static implicit operator {data.ClassName}({friendlyName} value)")
			.AppendLine(" => new(value);")
			.NewLine();
		}

		var type = data.PrimitiveTypeSymbol;

		sb.AppendSummary($"<summary>Implicit conversion from <see cref = \"{friendlyName}?\"/> to <see cref = \"{data.ClassName}?\"/></summary>")
			.AppendLine("[return: NotNullIfNotNull(nameof(value))]")
			.Append($"public static implicit operator {data.ClassName}?({friendlyName}? value)")
			.AppendLine($" => value is null ? null : new(value{(type.IsValueType ? ".Value" : "")});")
			.NewLine();

		if (data.ParentSymbols.Count != 0)
		{
			sb.AppendSummary($"<summary>Implicit conversion from <see cref = \"{data.ParentSymbols[0].Name}\"/> to <see cref = \"{data.ClassName}\"/></summary>")
			.Append($"public static implicit operator {data.ClassName}({data.ParentSymbols[0].Name} value)")
				.AppendLine(" => new(value);")
				.NewLine();
		}

		sb.AppendSummary($"<summary>Implicit conversion from <see cref = \"{data.ClassName}\"/> to <see cref = \"{friendlyName}\"/></summary>")
		.Append($"public static implicit operator {friendlyName}({data.ClassName} value)")
			.AppendLine($" => ({friendlyName})value.{data.FieldName};")
			.NewLine();

		if (data.DateType is DateType.DateOnly or DateType.TimeOnly)
		{
			sb.AppendSummary($"<summary>Implicit conversion from <see cref = \"{data.ClassName}\"/> to <see cref = \"DateTime\"/></summary>")
				.Append($"public static implicit operator DateTime({data.ClassName} value)")
				.AppendLine($" => (({friendlyName})value.{data.FieldName}).ToDateTime();")
				.NewLine();
		}
	}

	/// <summary>
	/// Processes the constructor for a specified class.
	/// </summary>
	/// <param name="data">The GeneratorData containing information about the data type.</param>
	/// <param name="sb">The SourceCodeBuilder for generating source code.</param>
	/// <param name="context">The SourceProductionContext for reporting diagnostics.</param>
	/// <returns>A boolean indicating whether the constructor processing was successful.</returns>
	private static bool ProcessConstructor(GeneratorData data, SourceCodeBuilder sb, SourceProductionContext context)
	{
		var type = data.Type;
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

		var underlyingTypeName = interfaceGenericType.GetFriendlyName(s_nullableTypeSymbol!);

		if (data.GenerateIsInitializedField)
		{
			sb.AppendLine($"private {underlyingTypeName} _valueOrDefault => _isInitialized ? _value : Default;");
		}

		sb.AppendLine("[DebuggerBrowsable(DebuggerBrowsableState.Never)]");
		sb.AppendLine($"private readonly {underlyingTypeName} _value;");

		if (data.GenerateIsInitializedField)
		{
			sb.AppendLine("[DebuggerBrowsable(DebuggerBrowsableState.Never)]");
			sb.AppendLine("private readonly bool _isInitialized;");
		}
		sb.NewLine();

		sb.AppendSummary($"Initializes a new instance of the <see cref=\"{type.Name}\"/> class by validating the specified <see cref=\"{underlyingTypeName}\"/> value using <see cref=\"Validate\"/> static method.",
			"The value to be validated.", "value");

		sb.AppendLine($"public {type.Name}({underlyingTypeName} value)")
			.OpenBracket()
			.AppendLine("Validate(value);")
			.AppendLine("_value = value;")
			.AppendLineIf(data.GenerateIsInitializedField, "_isInitialized = true;")
			.CloseBracket();

		if (!type.IsValueType)
			return true;

		sb.NewLine().AppendLine("[Obsolete(\"Domain primitive cannot be created using empty Ctor\", true)]");

		var primitiveTypeIsValueType = data.PrimitiveTypeSymbol.IsValueType;
		if (!primitiveTypeIsValueType)
			sb.AppendLine("#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.");

		sb.Append("public ").Append(type.Name).AppendLine("() : this(Default)");

		if (!primitiveTypeIsValueType)
			sb.AppendLine("#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.");

		sb.OpenBracket().CloseBracket();

		return true;
	}
}