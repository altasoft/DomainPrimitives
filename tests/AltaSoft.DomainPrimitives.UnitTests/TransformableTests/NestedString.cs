namespace AltaSoft.DomainPrimitives.UnitTests.TransformableTests;

[StringLength(2, 50)]
public sealed partial class NestedString : IDomainValue<ToUpperString>
{
    public static PrimitiveValidationResult Validate(ToUpperString value)
    {
        if (value.Contains("ERROR"))
            return "Error in value";
        return PrimitiveValidationResult.Ok;
    }
}
