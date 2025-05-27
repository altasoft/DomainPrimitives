using AltaSoft.DomainPrimitives;

namespace DomainPrimitivesDemo;

/// <summary>
/// CustomerId
/// </summary>
/// <example>132</example>
public readonly partial struct TransferId : IDomainValue<long>
{
    public static PrimitiveValidationResult Validate(long value)
    {
        if (value <= 0)
            return "Value must be positive";

        return PrimitiveValidationResult.Ok;
    }

}
