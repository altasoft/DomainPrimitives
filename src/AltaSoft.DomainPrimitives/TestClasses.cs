using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AltaSoft.DomainPrimitives.Abstractions;

namespace AltaSoft.DomainPrimitives;

public readonly partial struct X1 : IDomainValue<SByte>
{
	public static void Validate(SByte value)
	{
	}

	public static SByte Default => default;
}

public readonly partial struct X2 : IDomainValue<int>
{
	public static void Validate(int value)
	{
	}

	public static int Default => 0;
}

public readonly partial struct X3 : IDomainValue<int>
{
	public static void Validate(int value)
	{
	}

	public static int Default => 1 - 1;
}

public readonly partial struct X4 : IDomainValue<int>
{
	public static void Validate(int value)
	{
	}

	public static int Default
	{
		get { return 0; }
	}
}

public readonly partial struct X5 : IDomainValue<int>
{
	public static void Validate(int value)
	{
	}

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