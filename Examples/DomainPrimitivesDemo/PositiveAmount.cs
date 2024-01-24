using AltaSoft.DomainPrimitives;

namespace DomainPrimitivesDemo;

/// <summary>
/// Positive amount
/// </summary>
/// <example> 10.5 </example>
public readonly partial struct PositiveAmount : IDomainValue<decimal>
{
    public static void Validate(decimal value)
    {
        if (value <= 0m)
            throw new InvalidDomainValueException("Positive amount number must be positive");
    }

    public static decimal Default => 0.01m;
}