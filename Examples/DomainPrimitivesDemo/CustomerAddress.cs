using AltaSoft.DomainPrimitives;

namespace DomainPrimitivesDemo;

/// <summary>
/// Customer Address
/// </summary>
/// <example> Customer address N35 apt 14</example>
public sealed partial class CustomerAddress : IDomainValue<string>
{
    public static PrimitiveValidationResult Validate(string value)
    {
        if (value.Length < 10)
            return "Address must be at least 10 characters";

        return PrimitiveValidationResult.Ok;
    }

}
