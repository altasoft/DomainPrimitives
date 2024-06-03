using System.Runtime.InteropServices;

namespace AltaSoft.DomainPrimitives.XmlDataTypes;

/// <summary>
/// A domain primitive type representing a negative integer.
/// </summary>
/// <remarks>
/// The NegativeInteger ensures that its value is a negative integer (less than zero).
/// </remarks>
[StructLayout(LayoutKind.Auto)]
public readonly partial struct NegativeInteger : IDomainValue<int>
{
    /// <inheritdoc/>
    public static PrimitiveValidationResult Validate(int value)
    {
        if (value >= 0)
            return "value is non-negative";

        return PrimitiveValidationResult.Ok;
    }
}
