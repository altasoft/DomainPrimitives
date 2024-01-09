using AltaSoft.DomainPrimitives.Abstractions;

namespace AltaSoft.DomainPrimitives;

/// <summary>
/// A domain primitive type representing a positive integer.
/// </summary>
/// <remarks>
/// The PositiveInteger ensures that its value is a positive integer (greater than zero).
/// </remarks>
public readonly partial record struct PositiveInteger : IDomainValue<int>
{
	/// <inheritdoc/>
	public static void Validate(int value)
	{
		if (value <= 0)
			throw new InvalidDomainValueException("value is non-positive");
	}

	/// <inheritdoc/>
	public static int Default => 1;
}