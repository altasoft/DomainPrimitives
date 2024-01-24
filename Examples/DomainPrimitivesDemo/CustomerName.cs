using AltaSoft.DomainPrimitives;

namespace DomainPrimitivesDemo;

/// <summary>
/// Customer Name
/// </summary>
/// <example>Test Name</example>
public sealed partial class CustomerName : IDomainValue<string>
{
    public static void Validate(string value)
    {
        if (value.Length <= 3)
            throw new InvalidDomainValueException("Name must have at least 3 characters");

        foreach (var t in value)
        {
            if (!char.IsLetter(t) && !char.IsWhiteSpace(t))
                throw new InvalidDomainValueException("Name must contain only ascii letters");
        }
    }

    public static string Default => "N/A";
}