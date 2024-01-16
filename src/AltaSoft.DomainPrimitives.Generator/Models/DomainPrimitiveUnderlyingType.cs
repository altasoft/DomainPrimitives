using Microsoft.CodeAnalysis;

namespace AltaSoft.DomainPrimitives.Generator.Models;

/// <summary>
/// Enumerates the possible types of Domain Primitive values.
/// </summary>
internal enum DomainPrimitiveUnderlyingType
{
	/// <summary>
	/// Represents a string Domain Primitive type.
	/// </summary>
	String,

	/// <summary>
	/// Represents a GUID Domain Primitive type.
	/// </summary>
	Guid,

	/// <summary>
	/// Represents a boolean Domain Primitive type.
	/// </summary>
	Boolean,

	SByte,
	Byte,
	Int16,
	UInt16,
	Int32,
	UInt32,
	Int64,
	UInt64,
	Decimal,
	Single,
	Double,

	/// <summary>
	/// Represents a DateTime Domain Primitive type.
	/// </summary>
	DateTime,

	DateOnly,

	TimeOnly,

	TimeSpan,

	DateTimeOffset,

	/// <summary>
	/// Represents a character Domain Primitive type.
	/// </summary>
	Char,

	/// <summary>
	/// Represents other Domain Primitive type.
	/// </summary>
	Other
}