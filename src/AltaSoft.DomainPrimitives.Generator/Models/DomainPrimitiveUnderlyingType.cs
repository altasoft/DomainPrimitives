namespace AltaSoft.DomainPrimitives.Generator.Models;

/// <summary>
/// Enumerates the possible types of Domain Primitive values.
/// </summary>
internal enum DomainPrimitiveUnderlyingType
{
    // Represents a string Domain Primitive type.
    String,

    // Represents a GUID Domain Primitive type.
    Guid,

    // Represents a boolean Domain Primitive type.
    Boolean,

    // Represents a signed byte Domain Primitive type.
    SByte,

    // Represents an unsigned byte Domain Primitive type.
    Byte,

    // Represents a signed 16-bit integer Domain Primitive type.
    Int16,

    // Represents an unsigned 16-bit integer Domain Primitive type.
    UInt16,

    // Represents a signed 32-bit integer Domain Primitive type.
    Int32,

    // Represents an unsigned 32-bit integer Domain Primitive type.
    UInt32,

    // Represents a signed 64-bit integer Domain Primitive type.
    Int64,

    // Represents an unsigned 64-bit integer Domain Primitive type.
    UInt64,

    // Represents a decimal Domain Primitive type.
    Decimal,

    // Represents a single-precision floating-point Domain Primitive type.
    Single,

    // Represents a double-precision floating-point Domain Primitive type.
    Double,

    // Represents a DateTime Domain Primitive type.
    DateTime,

    // Represents a DateOnly Domain Primitive type.
    DateOnly,

    // Represents a TimeOnly Domain Primitive type.
    TimeOnly,

    // Represents a TimeSpan Domain Primitive type.
    TimeSpan,

    // Represents a DateTimeOffset Domain Primitive type.
    DateTimeOffset,

    // Represents a character Domain Primitive type.
    Char,

    // Represents other Domain Primitive type.
    Other
}