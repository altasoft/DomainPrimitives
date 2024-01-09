namespace AltaSoft.DomainPrimitives.Generator.Models;

/// <summary>
/// Enumerates the possible types of Domain Primitive values.
/// </summary>
internal enum DomainPrimitiveType
{
	/// <summary>
	/// Represents a numeric Domain Primitive type.
	/// </summary>
	Numeric,

	/// <summary>
	/// Represents a string Domain Primitive type.
	/// </summary>
	String,

	/// <summary>
	/// Represents a DateTime Domain Primitive type.
	/// </summary>
	DateTime,

	/// <summary>
	/// Represents a boolean Domain Primitive type.
	/// </summary>
	Boolean,

	/// <summary>
	/// Represents a character Domain Primitive type.
	/// </summary>
	Char,

	/// <summary>
	/// Represents a GUID Domain Primitive type.
	/// </summary>
	Guid,

	/// <summary>
	/// Represents an other Domain Primitive type.
	/// </summary>
	Other
}