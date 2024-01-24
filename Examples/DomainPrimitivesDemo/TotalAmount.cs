using AltaSoft.DomainPrimitives;

namespace DomainPrimitivesDemo;

/// <summary>
/// Total amount
/// </summary>
/// <example> 10.5 </example>
public readonly partial struct TotalAmount : IDomainValue<decimal>
{
    public static void Validate(decimal value)
    {
        if (value <= 0m)
            throw new InvalidDomainValueException("Total amount number must be positive");
    }

    public static decimal Default => 0.01m;
}