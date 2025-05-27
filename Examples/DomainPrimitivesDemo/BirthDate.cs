using System;
using AltaSoft.DomainPrimitives;

namespace DomainPrimitivesDemo
{
    /// <summary>
    /// CustomerId
    /// </summary>
    public readonly partial struct BirthDate : IDomainValue<DateOnly>
    {
        public static PrimitiveValidationResult Validate(DateOnly value)
        {

            if (DateTime.Today.Year - value.Year < 18)
                return "Customer must be at least 18 years old";

            return PrimitiveValidationResult.Ok;
        }

    }
}
