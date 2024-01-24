using AltaSoft.DomainPrimitives;

namespace DomainPrimitivesDemo;

/// <summary>
/// CustomerId
/// </summary>
/// <example>132</example>
public readonly partial struct TransferId : IDomainValue<long>
{
    public static void Validate(long value)
    {
        if (value <= 0)
            throw new InvalidDomainValueException("Value must be positive");
    }

    public static long Default => default;
}