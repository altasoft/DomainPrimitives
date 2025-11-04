namespace AltaSoft.DomainPrimitives.UnitTests.TransformableTests;

public readonly partial struct NestedInt : IDomainValue<AbsoluteInt>
{
    public static PrimitiveValidationResult Validate(AbsoluteInt value)
    {
        if ((int)value > 10)
            return "Must be less than 10";

        return PrimitiveValidationResult.Ok;
    }
}
