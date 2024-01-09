namespace AltaSoft.DomainPrimitives.Generator.Models;

/// <summary>
/// Represents numeric types used in the Domain Primitive code generation.
/// </summary>
internal enum NumericType
{
	/// <summary>
	/// 8-bit signed integer.
	/// </summary>
	Byte,

	/// <summary>
	/// 8-bit unsigned integer.
	/// </summary>
	SByte,

	/// <summary>
	/// 16-bit signed integer.
	/// </summary>
	Int16,

	/// <summary>
	/// 32-bit signed integer.
	/// </summary>
	Int32,

	/// <summary>
	/// 64-bit signed integer.
	/// </summary>
	Int64,

	/// <summary>
	/// 16-bit unsigned integer.
	/// </summary>
	UInt16,

	/// <summary>
	/// 32-bit unsigned integer.
	/// </summary>
	UInt32,

	/// <summary>
	/// 64-bit unsigned integer.
	/// </summary>
	UInt64,

	/// <summary>
	/// Decimal type with high precision.
	/// </summary>
	Decimal,

	/// <summary>
	/// Double-precision floating-point type.
	/// </summary>
	Double,

	/// <summary>
	/// Single-precision floating-point type.
	/// </summary>
	Single
}