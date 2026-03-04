using System;
using System.Diagnostics.CodeAnalysis;

namespace AltaSoft.DomainPrimitives;

/// <summary>
/// Specifies a regex pattern that is always emitted to the OpenAPI schema and can optionally be used for runtime validation.
/// By default, the pattern is not validated at runtime; validation occurs only when explicitly requested via <see cref="Validate"/>.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class PatternAttribute : Attribute
{
    /// <summary>
    /// Gets the regex pattern used in the generated OpenAPI schema and, when enabled, for runtime validation.
    /// </summary>
    public string Pattern { get; }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Pattern"/> should also be enforced via runtime validation.
    /// </summary>
    public bool Validate { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="PatternAttribute"/>.
    /// </summary>
    /// <param name="pattern">
    /// The regex pattern that will always be included in the OpenAPI schema and may also be used for runtime validation.
    /// </param>
    /// <param name="validate">
    /// A value indicating whether runtime validation should be performed using <paramref name="pattern"/>. Defaults to <see langword="false"/>
    /// to avoid incurring runtime validation overhead unless explicitly requested.
    /// </param>

    public PatternAttribute([StringSyntax(StringSyntaxAttribute.Regex)] string pattern, bool validate = false)
    {
        Pattern = pattern;
        Validate = validate;
    }
}
