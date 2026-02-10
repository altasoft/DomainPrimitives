namespace AltaSoft.DomainPrimitives.UnitTests.SerializationFormatTests;

[SerializationFormat("yyyyMMdd")]
public readonly partial struct CustomDateOnly : IDomainValue<DateOnly>
{
    public static PrimitiveValidationResult Validate(DateOnly value)
    {
        return PrimitiveValidationResult.Ok;
    }
}
