namespace AltaSoft.DomainPrimitives.UnitTests.TransformableTests;

/// <summary>
/// Pattern based string
/// </summary>
[Pattern("^[a-zA-H]{5,10}$", true)]
public sealed partial class PatternBasedString : IDomainValue<string>
{
    public static PrimitiveValidationResult Validate(string value)
    {
        return PrimitiveValidationResult.Ok;
    }
}
