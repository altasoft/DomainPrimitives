namespace AltaSoft.DomainPrimitives.UnitTests.TransformableTests;

public readonly partial struct AbsoluteInt : IDomainValue<int>
{
    public static PrimitiveValidationResult Validate(int value) => value < 0 ? "value is negative" : PrimitiveValidationResult.Ok;

    private static int Transform(int value) => Math.Abs(value);
}
