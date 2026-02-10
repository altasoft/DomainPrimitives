namespace AltaSoft.DomainPrimitives.UnitTests.SerializationFormatTests;

[SerializationFormat("yyyyMMdd_HHmmss")]
public readonly partial struct CustomDateTime : IDomainValue<DateTime>
{
    public static PrimitiveValidationResult Validate(DateTime value)
    {
        return PrimitiveValidationResult.Ok;
    }
}
