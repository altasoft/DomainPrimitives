namespace AltaSoft.DomainPrimitives.UnitTests.TransformableTests;

public sealed partial class NestedString : IDomainValue<ToUpperString>
{
    public static PrimitiveValidationResult Validate(ToUpperString value)
    {
        if (value.Contains("ERROR"))
            return "Error in value";
        return PrimitiveValidationResult.Ok;
    }
}
