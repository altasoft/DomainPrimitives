using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AltaSoft.DomainPrimitives.Abstractions;

namespace AltaSoft.DomainPrimitives;

/// <summary>
/// <para>Type: <see cref = "bool"/></para>
///</summary>
public partial struct YesNoIndicator : IDomainValue<bool>
{
	/// <inheritdoc/>
	public static void Validate(bool value)
	{
		//ValidationHelper.Validate(value);
	}

	/// <inheritdoc/>
	public static bool Default => default;
}

/// <inheritdoc/>
public readonly partial struct X1 : IDomainValue<SByte>
{
	/// <inheritdoc/>
	public static void Validate(SByte value)
	{
		if (value < 10 || value > 20)
			throw new InvalidDomainValueException("Value must be between 10 and 20");
	}

	/// <inheritdoc/>
	public static SByte Default => default;
}

/// <inheritdoc/>
public readonly partial struct X2 : IDomainValue<int>
{
	/// <inheritdoc/>
	public static void Validate(int value)
	{
		if (value < 10)
			throw new InvalidDomainValueException("Value must be between 10 and 20");
		if (value > 20)
			throw new InvalidDomainValueException("Value must be between 10 and 20");
	}

	/// <inheritdoc/>
	public static int Default => 0;
}

/// <inheritdoc/>
public readonly partial struct X3 : IDomainValue<int>
{
	/// <inheritdoc/>
	public static void Validate(int value)
	{
	}

	/// <inheritdoc/>
	public static int Default => 1 - 1;
}

/// <inheritdoc/>
public readonly partial struct X4 : IDomainValue<int>
{
	/// <inheritdoc/>
	public static void Validate(int value)
	{
	}

	/// <inheritdoc/>
	public static int Default
	{
		get { return 0; }
	}
}

/// <inheritdoc/>
public readonly partial struct X5 : IDomainValue<int>
{
	/// <inheritdoc/>
	public static void Validate(int value)
	{
	}

	/// <inheritdoc/>
	public static int Default
	{
		get
		{
			if (true)
				return 0;
			return 1;
		}
	}
}