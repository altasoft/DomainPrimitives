using System.Text.RegularExpressions;
using AltaSoft.DomainPrimitives;

namespace DomainPrimitivesDemo;

/// <summary>
/// Iban
/// </summary>
/// <example>GB82WEST12345698765432</example>
public sealed partial class Iban : IDomainValue<string>
{
    public static PrimitiveValidationResult Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return "IBAN cannot be null or empty.";
        }

        // Remove spaces from the IBAN (some formats include spaces for readability)
        var cleanedIban = value.Replace(" ", "");

        if (!CreateIbanRegex().IsMatch(cleanedIban))
        {
            return "Invalid IBAN format.";
        }

        return PrimitiveValidationResult.Ok;
    }

    [GeneratedRegex(@"^[A-Z]{2}\d{2}[A-Za-z0-9]{4,}$")]
    private static partial Regex CreateIbanRegex();
}
