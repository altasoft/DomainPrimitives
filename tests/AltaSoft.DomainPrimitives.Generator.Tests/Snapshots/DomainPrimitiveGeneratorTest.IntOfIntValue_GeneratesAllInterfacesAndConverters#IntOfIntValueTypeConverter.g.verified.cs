﻿//HintName: IntOfIntValueTypeConverter.g.cs
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by 'AltaSoft DomainPrimitives Generator'.
//     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#nullable enable

using AltaSoft.DomainPrimitives;
using System;
using System.ComponentModel;
using System.Globalization;

namespace AltaSoft.DomainPrimitives.Converters;

/// <summary>
/// TypeConverter for <see cref = "IntOfIntValue"/>
/// </summary>
internal sealed class IntOfIntValueTypeConverter : Int32Converter
{
    /// <inheritdoc/>
    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        var result = base.ConvertFrom(context, culture, value);
        if (result is null)
            return null;
        try
        {
            return new IntOfIntValue((int)result);
        }
        catch (InvalidDomainValueException ex)
        {
            throw new FormatException("Cannot parse IntOfIntValue", ex);
        }
    }
}
