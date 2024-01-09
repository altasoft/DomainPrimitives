using AltaSoft.DomainPrimitives.Abstractions;
using Microsoft.CodeAnalysis;

namespace AltaSoft.DomainPrimitives.Generator.Helpers;

/// <summary>
/// A utility class for generating diagnostic messages related to domain primitives and code generation.
/// </summary>
internal static class DiagnosticHelper
{
	/// <summary>
	/// Creates a diagnostic for an invalid exception type being thrown.
	/// </summary>
	/// <param name="location">The location where the diagnostic occurs.</param>
	/// <returns>A diagnostic indicating that an InvalidDomainValueException should be thrown.</returns>
	internal static Diagnostic InvalidExceptionTypeThrown(Location? location)
	{
		return Diagnostic.Create(
			new DiagnosticDescriptor(
				"AL1021",
				"InvalidDomainValueException should be thrown in order to be converted to bad request and work Correctly",
				"InvalidDomainValueException should be thrown in order to be converted to bad request and work Correctly",
				"General",
				DiagnosticSeverity.Warning,
				isEnabledByDefault: true), location);
	}

	/// <summary>
	/// Creates a diagnostic for specifying an invalid base type.
	/// </summary>
	/// <param name="location">The location where the diagnostic occurs.</param>
	/// <returns>A diagnostic indicating that the primitive type must be numeric, date, or string.</returns>
	internal static Diagnostic InvalidBaseTypeSpecified(Location? location)
	{
		return Diagnostic.Create(
			new DiagnosticDescriptor(
				"AL1001",
				"Primitive type must be a Numeric type, Date type or string type to use DomainPrimitivesGenerator",
				"Primitive type must be a Numeric type, Date type or string type to use DomainPrimitivesGenerator",
				"General",
				DiagnosticSeverity.Error,
				isEnabledByDefault: true), location);
	}

	/// <summary>
	/// Creates a diagnostic for a type that must be a date type.
	/// </summary>
	/// <param name="location">The location where the diagnostic occurs.</param>
	/// <param name="className">The name of the class that violates the rule.</param>
	/// <returns>A diagnostic indicating that a SerializationFormatAttribute can only be used with date types.</returns>
	internal static Diagnostic TypeMustBeDateType(Location? location, string className)
	{
		return Diagnostic.Create(
			new DiagnosticDescriptor(
				"AL1012",
				$"{nameof(SerializationFormatAttribute)} can only be used with  Date types",
				"Type {0} cannot have SerializationFormatAttribute as it's not a date type",
				"General",
				DiagnosticSeverity.Error,
				isEnabledByDefault: true), location, className);
	}

	/// <summary>
	/// Creates a diagnostic for a type that must be a numeric type.
	/// </summary>
	/// <param name="location">The location where the diagnostic occurs.</param>
	/// <param name="className">The name of the class that violates the rule.</param>
	/// <returns>A diagnostic indicating that a SupportedOperationsAttribute can only be used with operational numeric types.</returns>
	internal static Diagnostic TypeMustBeNumericType(Location? location, string className)
	{
		return Diagnostic.Create(
			new DiagnosticDescriptor(
				"AL1012",
				$"{nameof(SupportedOperationsAttribute)} can only be used with Operational Numeric types",
				"Type {0} cannot have SupportedOperationsAttribute as it's not an operational numeric type",
				"General",
				DiagnosticSeverity.Error,
				isEnabledByDefault: true), location, className);
	}

	/// <summary>
	/// Creates a diagnostic for a class that must be partial to generate an empty constructor.
	/// </summary>
	/// <param name="location">The location where the diagnostic occurs.</param>
	/// <returns>A diagnostic indicating that a class must be partial to generate an empty constructor.</returns>
	internal static Diagnostic ClassMustBePartial(Location? location)
	{
		return Diagnostic.Create(
			new DiagnosticDescriptor(
				"AL1000",
				"Class must be partial to generate Empty constructor",
				"Class must be partial to generate Empty constructor",
				"General",
				DiagnosticSeverity.Error,
				isEnabledByDefault: true), location);
	}

	/// <summary>
	/// Creates a diagnostic for a type that should be a value type.
	/// </summary>
	/// <param name="className">The name of the class that violates the rule.</param>
	/// <param name="baseTypeName">The name of the expected base type.</param>
	/// <param name="location">The location where the diagnostic occurs.</param>
	/// <returns>A diagnostic indicating that the type should be a value type.</returns>
	internal static Diagnostic TypeShouldBeValueType(string className, string baseTypeName, Location? location)
	{
		return Diagnostic.Create(
			new DiagnosticDescriptor(
				"AL1015",
				"Type should be a value type",
				"Type '{0}' should be a value type as it's wrapping a value type of `{1}`",
				"General",
				DiagnosticSeverity.Error,
				isEnabledByDefault: true), location, className, baseTypeName);
	}

	/// <summary>
	/// Creates a diagnostic for a class constructor parameter that must match the type specified in IDomainValue.
	/// </summary>
	/// <param name="className">The name of the class that violates the rule.</param>
	/// <param name="parameterType">The expected parameter type.</param>
	/// <param name="location">The location where the diagnostic occurs.</param>
	/// <returns>A diagnostic indicating that the class must have a single parameter constructor with the specified type.</returns>
	internal static Diagnostic ClassConstructorParamMustBeCorrectType(string className, string parameterType, Location? location)
	{
		return Diagnostic.Create(
			new DiagnosticDescriptor(
				"AL1010",
				"Domain Primitives must have a single parameter constructor with the same type as specified in IDomainValue<>",
				"Type '{0}' must have a single parameter constructor with type `{1}` as specified in IDomainValue<>",
				"General",
				DiagnosticSeverity.Error,
				isEnabledByDefault: true), location, className, parameterType);
	}

	/// <summary>
	/// Creates a diagnostic for a class that must not have a parameterized constructor to generate members.
	/// </summary>
	/// <param name="className">The name of the class that violates the rule.</param>
	/// <param name="location">The location where the diagnostic occurs.</param>
	/// <returns>A diagnostic indicating that the class must not have a parameterized constructor.</returns>
	internal static Diagnostic ClassMustNotHaveConstructorWithParam(string className, Location? location)
	{
		return Diagnostic.Create(
			new DiagnosticDescriptor(
				"AL1011",
				"Domain Primitives must not have a parameterized constructor to successfully generate members",
				"Type '{0}' must not have a parameterized constructor to successfully generate members",
				"General",
				DiagnosticSeverity.Error,
				isEnabledByDefault: true), location, className);
	}

	/// <summary>
	/// Creates a diagnostic for a class that has a non-obsolete empty constructor.
	/// </summary>
	/// <param name="className">The name of the class that violates the rule.</param>
	/// <param name="location">The location where the diagnostic occurs.</param>
	/// <returns>A diagnostic indicating that the class should not have non-obsolete empty constructors.</returns>
	internal static Diagnostic ClassHasDefaultConstructor(string className, Location? location)
	{
		return Diagnostic.Create(
			new DiagnosticDescriptor(
				"AL1003",
				"Domain Primitives Should not have non obsolete empty constructors, either delete or add an obsolete attribute with Error=true",
				"Type '{0}' Should not have non obsolete empty constructors, either delete or add an obsolete attribute with Error=true",
				"General",
				DiagnosticSeverity.Error,
				isEnabledByDefault: true), location, className);
	}

	/// <summary>
	/// Creates a diagnostic for a class that should have only one field with the same signature as the base type.
	/// </summary>
	/// <param name="className">The name of the class that violates the rule.</param>
	/// <param name="location">The location where the diagnostic occurs.</param>
	/// <returns>A diagnostic indicating that the class should have only one field with the same signature as the base type.</returns>
	internal static Diagnostic MultipleOrNoFieldsAvailable(string className, Location? location)
	{
		return Diagnostic.Create(
			new DiagnosticDescriptor(
				"AL1004",
				"Domain Primitives Should only have 1 field with the same signature as base type",
				"Domain Primitives Should only have 1 field with the same signature as base type",
				"General",
				DiagnosticSeverity.Error,
				isEnabledByDefault: true), location, className);
	}
}