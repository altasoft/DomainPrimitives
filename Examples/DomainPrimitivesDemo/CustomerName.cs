using AltaSoft.DomainPrimitives;

namespace DomainPrimitivesDemo;

/// <summary>
/// Customer Name
/// </summary>
/// <example>Test Name</example>
public sealed partial class CustomerName : IDomainValue<string>
{
    public static PrimitiveValidationResult Validate(string value)
    {
        if (value.Length <= 3)
            return "Name must have at least 3 characters";

        foreach (var t in value)
        {
            if (!char.IsLetter(t) && !char.IsWhiteSpace(t))
                return "Name must contain only ascii letters";
        }

        return PrimitiveValidationResult.Ok;
    }

}
