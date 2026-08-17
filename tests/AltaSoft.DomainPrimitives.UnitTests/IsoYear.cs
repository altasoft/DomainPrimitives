using AltaSoft.DomainPrimitives.XmlDataTypes;

namespace AltaSoft.DomainPrimitives.UnitTests;

/// <summary>
/// <para>Year represented by YYYY (ISO 8601).</para>
/// </summary>
public partial struct IsoYear : IDomainValue<GYear> // iso20022:Year
{
    /// <inheritdoc/>
    public static PrimitiveValidationResult Validate(GYear value)
    {
        return PrimitiveValidationResult.Ok;
    }
}
