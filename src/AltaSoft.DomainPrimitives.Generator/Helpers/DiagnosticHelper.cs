using Microsoft.CodeAnalysis;
using System;

namespace AltaSoft.DomainPrimitives.Generator.Helpers;

/// <summary>
/// A utility class for generating diagnostic messages related to domain primitives and code generation.
/// </summary>
internal static class DiagnosticHelper
{
    private const string Category = "AltaSoft.DomainPrimitives.Generator";

    ///// <summary>
    ///// Creates a diagnostic indicating that the AltaSoft.DomainPrimitives.Generator has started.
    ///// </summary>
    ///// <returns>
    ///// The created diagnostic.
    ///// </returns>
    //internal static Diagnostic GeneratorStarted()
    //{
    //    return Diagnostic.Create(new DiagnosticDescriptor(
    //        "AL0000",
    //        "AltaSoft.DomainPrimitives.Generator started",
    //        "AltaSoft.DomainPrimitives.Generator started",
    //        Category,
    //        DiagnosticSeverity.Info,
    //        isEnabledByDefault: true), null);
    //}

    /// <summary>
    /// Creates a diagnostic for general error
    /// </summary>
    /// <param name="location">The location where the diagnostic occurs.</param>
    /// <param name="ex"></param>
    /// <returns>A diagnostic indicating that the general error happened.</returns>
    internal static Diagnostic GeneralError(Location? location, Exception ex)
    {
        return Diagnostic.Create(
            new DiagnosticDescriptor(
                "AL1000",
                "An exception was thrown by the AltaSoft.DomainPrimitiveGenerator generator",
                "An exception was thrown by the AltaSoft.DomainPrimitiveGenerator generator: `{0}`{1}{2}",
                Category,
                DiagnosticSeverity.Error,
                isEnabledByDefault: true),
            location, ex, SourceCodeBuilder.s_newLine, ex.StackTrace);
    }

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
                Category,
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
                Category,
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
                $"{Constants.SerializationFormatAttribute} can only be used with  Date types",
                "Type {0} cannot have SerializationFormatAttribute as it's not a date type",
                Category,
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
                $"{Constants.SupportedOperationsAttribute} can only be used with Operational Numeric types",
                "Type {0} cannot have SupportedOperationsAttribute as it's not an operational numeric type",
                Category,
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
                "AL1002",
                "Class must be partial to generate Empty constructor",
                "Class must be partial to generate Empty constructor",
                Category,
                DiagnosticSeverity.Error,
                isEnabledByDefault: true), location);
    }

    /// <summary>
    /// Creates a diagnostic for a type that should be a reference type.
    /// </summary>
    /// <param name="className">The name of the class that violates the rule.</param>
    /// <param name="baseTypeName">The name of the expected base type.</param>
    /// <param name="location">The location where the diagnostic occurs.</param>
    /// <returns>A diagnostic indicating that the type should be a reference type.</returns>
    internal static Diagnostic TypeShouldBeReferenceType(string className, string baseTypeName, Location? location)
    {
        return Diagnostic.Create(
            new DiagnosticDescriptor(
                "AL1016",
                "Type should be a reference type",
                "Type `{0}` should be a reference type as it's wrapping a reference type of `{1}`",
                Category,
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true), location, className, baseTypeName);
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
                "Type `{0}` should be a value type as it's wrapping a value type of `{1}`",
                Category,
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true), location, className, baseTypeName);
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
                "Type `{0}` must not have a parameterized constructor to successfully generate members",
                Category,
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
                "Type `{0}` Should not have non obsolete empty constructors, either delete or add an obsolete attribute with Error=true",
                Category,
                DiagnosticSeverity.Error,
                isEnabledByDefault: true), location, className);
    }
}
