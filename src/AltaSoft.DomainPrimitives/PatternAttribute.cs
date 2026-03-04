using System;
using System.Diagnostics.CodeAnalysis;

namespace AltaSoft.DomainPrimitives;

/// <summary>
/// Specifies a regex pattern for OpenAPI schema generation only.
/// By default, the pattern is not validated at runtime; validation occurs only when explicitly requested.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class PatternAttribute : Attribute
{
    /// <summary>
    /// Gets the regex pattern used in the generated OpenAPI schema.
    /// </summary>
    public string Pattern { get; }

    /// <summary>
    /// Gets a value indicating whether runtime validation should be performed.
    /// </summary>
    public bool Validate { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="PatternAttribute"/>.
    /// </summary>
    /// <param name="pattern">The regex pattern used for OpenAPI schema generation.</param>
    /// <param name="validate">
    /// A value indicating whether runtime validation should be performed. Defaults to <see langword="false"/>
    /// to avoid incurring runtime validation overhead unless explicitly requested.
    /// </param>

    public PatternAttribute([StringSyntax(StringSyntaxAttribute.Regex)] string pattern, bool validate = false)
    {
        Pattern = pattern;
        Validate = validate;
    }
}
