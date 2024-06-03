using System.Runtime.InteropServices;

namespace AltaSoft.DomainPrimitives.XmlDataTypes;

/// <summary>
/// A domain primitive type representing a positive integer.
/// </summary>
/// <remarks>
/// The PositiveInteger ensures that its value is a positive integer (greater than zero).
/// </remarks>
[StructLayout(LayoutKind.Auto)]
public readonly partial struct PositiveInteger : IDomainValue<int>
{
    /// <inheritdoc/>
    public static PrimitiveValidationResult Validate(int value)
    {
        if (value <= 0)
            return "value is non-positive";

        return PrimitiveValidationResult.Ok;
    }
}
