using System;

namespace AltaSoft.DomainPrimitives;

/// <summary>
/// Specifies the serialization format for a class or struct.
/// </summary>
/// <remarks>
/// This attribute can be applied to classes or structs to indicate the format used for serialization
/// when converting the object to or from a serialized representation, such as JSON or XML.
/// </remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class SerializationFormatAttribute : Attribute
{
    /// <summary>
    /// Gets or sets the serialization format as a string.
    /// </summary>
    public string Format { get; set; }

    /// <summary>
    /// Initializes a new instance of the SerializationFormat class with the specified format.
    /// </summary>
    /// <param name="format">The serialization format as a string.</param>
    public SerializationFormatAttribute(string format)
    {
        Format = format;
    }
}
