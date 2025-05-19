namespace AltaSoft.DomainPrimitives.UnitTests.TransformableTests;

public sealed partial class ToUpperString : IDomainValue<string>
{
    public static PrimitiveValidationResult Validate(string value) => value.All(char.IsUpper) ? PrimitiveValidationResult.Ok : "Invalid value";

    private static string Transform(string value) => value.ToUpperInvariant();
}
