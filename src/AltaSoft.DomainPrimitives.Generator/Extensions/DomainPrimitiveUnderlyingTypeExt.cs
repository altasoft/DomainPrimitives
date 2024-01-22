using AltaSoft.DomainPrimitives.Generator.Models;
using Microsoft.CodeAnalysis;
using System;

namespace AltaSoft.DomainPrimitives.Generator.Extensions;

internal static class DomainPrimitiveUnderlyingTypeExt
{
    public static DomainPrimitiveUnderlyingType GetDomainPrimitiveUnderlyingType(this INamedTypeSymbol type)
    {
        switch (type.SpecialType)
        {
            case SpecialType.System_String:
                return DomainPrimitiveUnderlyingType.String;

            case SpecialType.System_Boolean:
                return DomainPrimitiveUnderlyingType.Boolean;

            case SpecialType.System_Char:
                return DomainPrimitiveUnderlyingType.Char;

            case SpecialType.System_SByte:
                return DomainPrimitiveUnderlyingType.SByte;

            case SpecialType.System_Byte:
                return DomainPrimitiveUnderlyingType.Byte;

            case SpecialType.System_Int16:
                return DomainPrimitiveUnderlyingType.Int16;

            case SpecialType.System_UInt16:
                return DomainPrimitiveUnderlyingType.UInt16;

            case SpecialType.System_Int32:
                return DomainPrimitiveUnderlyingType.Int32;

            case SpecialType.System_UInt32:
                return DomainPrimitiveUnderlyingType.UInt32;

            case SpecialType.System_Int64:
                return DomainPrimitiveUnderlyingType.Int64;

            case SpecialType.System_UInt64:
                return DomainPrimitiveUnderlyingType.UInt64;

            case SpecialType.System_Decimal:
                return DomainPrimitiveUnderlyingType.Decimal;

            case SpecialType.System_Single:
                return DomainPrimitiveUnderlyingType.Single;

            case SpecialType.System_Double:
                return DomainPrimitiveUnderlyingType.Double;

            case SpecialType.System_DateTime:
                return DomainPrimitiveUnderlyingType.DateTime;
        }

        if (type.ToDisplayString() == "System.Guid")
            return DomainPrimitiveUnderlyingType.Guid;

        if (type.ToDisplayString() == "System.DateOnly")
            return DomainPrimitiveUnderlyingType.DateOnly;

        if (type.ToDisplayString() == "System.TimeOnly")
            return DomainPrimitiveUnderlyingType.TimeOnly;

        if (type.ToDisplayString() == "System.TimeSpan")
            return DomainPrimitiveUnderlyingType.TimeSpan;

        if (type.ToDisplayString() == "System.DateTimeOffset")
            return DomainPrimitiveUnderlyingType.DateTimeOffset;

        return DomainPrimitiveUnderlyingType.Other;
    }

    public static bool IsNumeric(this DomainPrimitiveUnderlyingType underlyingType)
    {
        return underlyingType switch
        {
            DomainPrimitiveUnderlyingType.Byte => true,
            DomainPrimitiveUnderlyingType.SByte => true,
            DomainPrimitiveUnderlyingType.Int16 => true,
            DomainPrimitiveUnderlyingType.Int32 => true,
            DomainPrimitiveUnderlyingType.Int64 => true,
            DomainPrimitiveUnderlyingType.UInt16 => true,
            DomainPrimitiveUnderlyingType.UInt32 => true,
            DomainPrimitiveUnderlyingType.UInt64 => true,
            DomainPrimitiveUnderlyingType.Decimal => true,
            DomainPrimitiveUnderlyingType.Double => true,
            DomainPrimitiveUnderlyingType.Single => true,

            _ => false
        };
    }

    public static bool IsDateOrTime(this DomainPrimitiveUnderlyingType underlyingType)
    {
        return underlyingType switch
        {
            DomainPrimitiveUnderlyingType.DateTime => true,
            DomainPrimitiveUnderlyingType.DateOnly => true,
            DomainPrimitiveUnderlyingType.TimeOnly => true,
            DomainPrimitiveUnderlyingType.DateTimeOffset => true,
            DomainPrimitiveUnderlyingType.TimeSpan => true,

            _ => false
        };
    }

    public static bool IsFloatingPoint(this DomainPrimitiveUnderlyingType underlyingType)
    {
        return underlyingType switch
        {
            DomainPrimitiveUnderlyingType.Decimal => true,
            DomainPrimitiveUnderlyingType.Double => true,
            DomainPrimitiveUnderlyingType.Single => true,

            _ => false
        };
    }

    public static bool IsByteOrShort(this DomainPrimitiveUnderlyingType underlyingType)
    {
        return underlyingType switch
        {
            DomainPrimitiveUnderlyingType.Byte => true,
            DomainPrimitiveUnderlyingType.SByte => true,
            DomainPrimitiveUnderlyingType.Int16 => true,
            DomainPrimitiveUnderlyingType.UInt16 => true,

            _ => false
        };
    }

    public static object? GetDefaultValue(this DomainPrimitiveUnderlyingType underlyingType)
    {
        return underlyingType switch
        {
            DomainPrimitiveUnderlyingType.String => default(string?),
            DomainPrimitiveUnderlyingType.Boolean => false,
            DomainPrimitiveUnderlyingType.Char => default(char),
            DomainPrimitiveUnderlyingType.Guid => default(Guid),

            DomainPrimitiveUnderlyingType.Byte => default(byte),
            DomainPrimitiveUnderlyingType.SByte => default(sbyte),
            DomainPrimitiveUnderlyingType.Int16 => default(short),
            DomainPrimitiveUnderlyingType.Int32 => default(int),
            DomainPrimitiveUnderlyingType.Int64 => default(long),
            DomainPrimitiveUnderlyingType.UInt16 => default(ushort),
            DomainPrimitiveUnderlyingType.UInt32 => default(uint),
            DomainPrimitiveUnderlyingType.UInt64 => default(ulong),
            DomainPrimitiveUnderlyingType.Decimal => default(decimal),
            DomainPrimitiveUnderlyingType.Double => default(double),
            DomainPrimitiveUnderlyingType.Single => default(float),

            DomainPrimitiveUnderlyingType.DateTime => default(DateTime),
            DomainPrimitiveUnderlyingType.DateOnly => new DateTime(1, 1, 1),
            DomainPrimitiveUnderlyingType.TimeOnly => new DateTime(1, 1, 1, 0, 0, 0),
            DomainPrimitiveUnderlyingType.DateTimeOffset => default(DateTimeOffset),
            DomainPrimitiveUnderlyingType.TimeSpan => default(TimeSpan),
            _ => new DummyValueObject()
        };
    }

    private readonly struct DummyValueObject;
}