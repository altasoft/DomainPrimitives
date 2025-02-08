using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

// ReSharper disable UnusedMember.Global

namespace AltaSoft.DomainPrimitives;

/// <summary>
/// Provides extension methods for working with types related to domain primitives.
/// </summary>
public static class TypeExtensions
{
    /// <summary>
    /// Determines whether the specified type implements <see cref="IDomainValue"/>.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns><c>true</c> if the type implements <see cref="IDomainValue"/>; otherwise, <c>false</c>.</returns>
    public static bool IsDomainPrimitive(this Type type)
    {
        return typeof(IDomainValue).IsAssignableFrom(type);
    }

    /// <summary>
    /// Gets the underlying primitive type associated with the given domain value type, if applicable.
    /// </summary>
    /// <param name="type">The type to analyze.</param>
    /// <returns>
    /// The underlying primitive type if the type represents a domain value; otherwise, <c>null</c>.
    /// </returns>
    public static Type? GetUnderlyingDomainPrimitiveType(this Type type)
    {
        return TryGetUnderlyingDomainPrimitiveType(type, out var primitiveType) ? primitiveType : null;
    }

    /// <summary>
    /// Attempts to retrieve the underlying primitive type of a domain value type.
    /// </summary>
    /// <param name="type">The type to analyze.</param>
    /// <param name="primitiveType">
    /// When this method returns, contains the underlying primitive type if found; otherwise, <c>null</c>.
    /// </param>
    /// <returns>
    /// <c>true</c> if the underlying primitive type was found; otherwise, <c>false</c>.
    /// </returns>
    public static bool TryGetUnderlyingDomainPrimitiveType(this Type type, [NotNullWhen(true)] out Type? primitiveType)
    {
        var isNullableT = type.TryGetNullableUnderlyingType(out var underlyingType);
        if (isNullableT)
        {
            type = underlyingType!;
        }

        if (!type.IsDomainPrimitive())
        {
            primitiveType = null;
            return false;
        }

        primitiveType = type.GetCustomAttribute<UnderlyingPrimitiveTypeAttribute>()?.UnderlyingPrimitiveType;
        if (primitiveType is null)
        {
            return false;
        }

        if (isNullableT)
        {
            primitiveType = typeof(Nullable<>).MakeGenericType(primitiveType);
        }

        return true;
    }

    /// <summary>
    /// Determines whether the specified type is a nullable value type and retrieves its underlying type.
    /// </summary>
    /// <param name="type">The type to analyze.</param>
    /// <param name="underlyingType">
    /// When this method returns, contains the underlying type if the specified type is nullable; otherwise, <c>null</c>.
    /// </param>
    /// <returns>
    /// <c>true</c> if the type is a nullable value type; otherwise, <c>false</c>.
    /// </returns>
    private static bool TryGetNullableUnderlyingType(this Type type, [NotNullWhen(true)] out Type? underlyingType)
    {
        underlyingType = Nullable.GetUnderlyingType(type);
        return underlyingType is not null;
    }
}
