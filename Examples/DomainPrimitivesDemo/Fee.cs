using AltaSoft.DomainPrimitives;

namespace DomainPrimitivesDemo;

/// <summary>
/// Transfer Fees
/// </summary>
/// <example>1.15</example>
public readonly partial struct Fee : IDomainValue<decimal>
{
    public static void Validate(decimal value)
    {
        if (value < 0m)
            throw new InvalidDomainValueException("Positive amount number must be positive");
    }

    public static decimal Default => 0m;
}