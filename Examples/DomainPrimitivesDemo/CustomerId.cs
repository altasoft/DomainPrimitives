using AltaSoft.DomainPrimitives;
using System;

namespace DomainPrimitivesDemo;

/// <summary>
/// CustomerId
/// </summary>
/// <example>608eadda-6730-4031-9333-8a21e40210ed</example>
public readonly partial struct CustomerId : IDomainValue<Guid>
{
    public static void Validate(Guid value)
    {
    }

    public static Guid Default => default;
}