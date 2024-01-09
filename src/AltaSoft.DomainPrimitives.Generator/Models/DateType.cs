namespace AltaSoft.DomainPrimitives.Generator.Models;

/// <summary>
/// Specifies the types of date and time values that can be represented.
/// </summary>
internal enum DateType
{
	/// <summary>
	/// Represents a date-only value.
	/// </summary>
	DateOnly,

	/// <summary>
	/// Represents a date and time value.
	/// </summary>
	DateTime,

	/// <summary>
	/// Represents a time-only value.
	/// </summary>
	TimeOnly,

	/// <summary>
	/// Represents a date and time value with an offset.
	/// </summary>
	DateTimeOffset,

	/// <summary>
	/// Represents a duration of time.
	/// </summary>
	TimeSpan
}