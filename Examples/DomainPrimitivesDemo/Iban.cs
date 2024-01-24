using AltaSoft.DomainPrimitives;
using System.Text.RegularExpressions;

namespace DomainPrimitivesDemo;

/// <summary>
/// Iban
/// </summary>
/// <example>GB82WEST12345698765432</example>
public sealed partial class Iban : IDomainValue<string>
{
    public static void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidDomainValueException("IBAN cannot be null or empty.");
        }

        // Remove spaces from the IBAN (some formats include spaces for readability)
        var cleanedIban = value.Replace(" ", "");

        if (!CreateIbanRegex().IsMatch(cleanedIban))
        {
            throw new InvalidDomainValueException("Invalid IBAN format.");
        }
    }

    public static string Default => "";

    [GeneratedRegex(@"^[A-Z]{2}\d{2}[A-Za-z0-9]{4,}$")]
    private static partial Regex CreateIbanRegex();
}