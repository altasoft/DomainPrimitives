using System;

namespace AltaSoft.DomainPrimitives;

/// <summary>
/// Specifies that the attributed class or struct has an underlying primitive type.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class UnderlyingPrimitiveTypeAttribute : Attribute
{
    /// <summary>
    /// Gets the underlying primitive type.
    /// </summary>
    public Type UnderlyingPrimitiveType { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="UnderlyingPrimitiveTypeAttribute"/> class
    /// with the specified underlying primitive type.
    /// </summary>
    /// <param name="underlyingPrimitiveType">The underlying primitive type.</param>
    public UnderlyingPrimitiveTypeAttribute(Type underlyingPrimitiveType)
    {
        UnderlyingPrimitiveType = underlyingPrimitiveType;
    }
}
