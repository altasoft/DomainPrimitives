namespace AltaSoft.DomainPrimitives.UnitTests.SerializationFormatTests
{
    [SerializationFormat(@"hh\:mm")]
    public readonly partial struct CustomTimeSpan : IDomainValue<TimeSpan>
    {
        public static PrimitiveValidationResult Validate(TimeSpan value)
        {
            return PrimitiveValidationResult.Ok;
        }
    }
}
