namespace AltaSoft.DomainPrimitives;

/// <summary>
/// A domain primitive representing a non-empty string.
/// </summary>
/// <remarks>
/// The NonEmptyString ensures that its value is a non-empty string.
/// </remarks>
public partial class NonEmptyString : IDomainValue<string>
{
	/// <inheritdoc/>
	public static void Validate(string value)
	{
		if (string.IsNullOrEmpty(value))
			throw new InvalidDomainValueException("value is empty string");
	}

	/// <summary>
	/// Gets the default value for NonEmptyString, which is "N/A".
	/// </summary>
	public static string Default => "N/A";
}