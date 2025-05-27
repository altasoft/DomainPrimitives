using AltaSoft.DomainPrimitives;

namespace DomainPrimitivesDemo;

/// <summary>
/// Total amount
/// </summary>
/// <example> 10.5 </example>
public readonly partial struct TotalAmount : IDomainValue<decimal>
{
    public static PrimitiveValidationResult Validate(decimal value)
    {
        if (value <= 0m)
            return "Total amount number must be positive";

        return PrimitiveValidationResult.Ok;
    }

}
