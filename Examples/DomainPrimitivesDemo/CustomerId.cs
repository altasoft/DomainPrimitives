using System;
using AltaSoft.DomainPrimitives;

namespace DomainPrimitivesDemo;

/// <summary>
/// CustomerId
/// </summary>
/// <example>608eadda-6730-4031-9333-8a21e40210ed</example>
public readonly partial struct CustomerId : IDomainValue<Guid>
{
    public static PrimitiveValidationResult Validate(Guid value)
    {
        return PrimitiveValidationResult.Ok;
    }

}
