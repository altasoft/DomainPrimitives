﻿//HintName: CharValueTypeConverter.g.cs
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a AltaSoft.DomainPrimitives.Generator v1.0.0
//     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#nullable enable

using AltaSoft.DomainPrimitives;
using System;
using System.ComponentModel;
using System.Globalization;
using AltaSoft.DomainPrimitives.Abstractions;

namespace AltaSoft.DomainPrimitives.Converters;

/// <summary>
/// TypeConverter for <see cref = "CharValue"/>
/// </summary>
public sealed class CharValueTypeConverter : CharConverter
{
	/// <inheritdoc/>
	public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
	{
		var result = base.ConvertFrom(context, culture, value);
		if (result is null)
			return null;
		try
		{
			return new CharValue((char)result);
		}
		catch (InvalidDomainValueException ex)
		{
			throw new FormatException("Cannot parse CharValue", ex);
		}
	}
}