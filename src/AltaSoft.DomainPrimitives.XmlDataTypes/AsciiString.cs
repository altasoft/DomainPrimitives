using System;

namespace AltaSoft.DomainPrimitives.XmlDataTypes;

/// <summary>
/// A domain primitive type representing an ASCII string.
/// </summary>
/// <remarks>
/// The AsciiString ensures that its value contains only ASCII characters.
/// </remarks>
[StringLength(10, 100, false)]
public partial class AsciiString : IDomainValue<string>
{
    /// <inheritdoc/>
    public static PrimitiveValidationResult Validate(string value)
    {
        var input = value.AsSpan();

        // ReSharper disable once ForCanBeConvertedToForeach
        for (var i = 0; i < input.Length; i++)
        {
            if (!char.IsAscii(input[i]))
                return "value contains non-ascii characters";
        }

        return PrimitiveValidationResult.Ok;
    }
}
