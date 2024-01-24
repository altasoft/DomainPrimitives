using AltaSoft.DomainPrimitives;

namespace DomainPrimitivesDemo;

/// <summary>
/// Transfer Currency
/// </summary>
public sealed partial class Currency : IDomainValue<string>
{
    public static void Validate(string value)
    {
        if (value.Length != 3)
            throw new InvalidDomainValueException("Value must have length of 3");

        if (!IsUppercaseLetters(value))
            throw new InvalidDomainValueException("Currency code must consist of uppercase letters.");
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

    public static string Default => "USD";
}
