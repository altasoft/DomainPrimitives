using AltaSoft.DomainPrimitives.Abstractions;
using System;

namespace AltaSoft.DomainPrimitives;

/// <summary>
/// A domain primitive type representing an ASCII string.
/// </summary>
/// <remarks>
/// The AsciiString ensures that its value contains only ASCII characters.
/// </remarks>
public readonly partial record struct AsciiString : IDomainValue<string>
{
	/// <inheritdoc/>
	public static void Validate(string value)
	{
		var input = value.AsSpan();
		for (var i = 0; i < input.Length; i++)
		{
			if (!char.IsAscii(input[i]))
				throw new InvalidDomainValueException("value contains non-ascii characters");
		}
	}

	/// <inheritdoc/>
	public static string Default => string.Empty;
}