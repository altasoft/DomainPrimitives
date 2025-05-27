using AltaSoft.DomainPrimitives;

namespace DomainPrimitivesDemo;

/// <summary>
/// Transfer Currency
/// </summary>
public sealed partial class Currency : IDomainValue<string>
{
    public static PrimitiveValidationResult Validate(string value)
    {
        if (value.Length != 3)
            return "Value must have length of 3";

        if (!IsUppercaseLetters(value))
            return "Currency code must consist of uppercase letters.";

        return PrimitiveValidationResult.Ok;
    }

    private static bool IsUppercaseLetters(string value)
    {
        foreach (var c in value)
        {
            if (!char.IsUpper(c))
            {
                return false;
            }
        }
        return true;
    }

}
