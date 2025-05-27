using AltaSoft.DomainPrimitives;

namespace DomainPrimitivesDemo;

/// <summary>
/// Positive amount
/// </summary>
/// <example> 10.5 </example>
public readonly partial struct PositiveAmount : IDomainValue<decimal>
{
    public static PrimitiveValidationResult Validate(decimal value)
    {
        if (value <= 0m)
            return "Positive amount number must be positive";

        return PrimitiveValidationResult.Ok;
    }

}
