namespace AltaSoft.DomainPrimitives.UnitTests.SerializationFormatTests;

[SerializationFormat("HHmmss")]
public readonly partial struct CustomTimeOnly : IDomainValue<TimeOnly>
{
    public static PrimitiveValidationResult Validate(TimeOnly value)
    {
        return PrimitiveValidationResult.Ok;
    }
}
