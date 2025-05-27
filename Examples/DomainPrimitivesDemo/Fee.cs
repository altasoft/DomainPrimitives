using AltaSoft.DomainPrimitives;

namespace DomainPrimitivesDemo;

/// <summary>
/// Transfer Fees
/// </summary>
/// <example>1.15</example>
public readonly partial struct Fee : IDomainValue<decimal>
{
    public static PrimitiveValidationResult Validate(decimal value)
    {
        if (value < 0m)
            return "Positive amount number must be positive";

        return PrimitiveValidationResult.Ok;
    }
}
