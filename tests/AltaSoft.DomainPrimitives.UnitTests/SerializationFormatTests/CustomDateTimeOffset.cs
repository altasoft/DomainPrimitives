namespace AltaSoft.DomainPrimitives.UnitTests.SerializationFormatTests
{
    [SerializationFormat("yyyyMMdd")]
    public readonly partial struct CustomDateTimeOffset : IDomainValue<DateTimeOffset>
    {
        public static PrimitiveValidationResult Validate(DateTimeOffset value)
        {
            return PrimitiveValidationResult.Ok;
        }
    }
}
