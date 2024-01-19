//using System;

//namespace AltaSoft.DomainPrimitives;

//public readonly partial struct GDay2 : IDomainValue<GDay>
//{
//	public static GDay Default => default;

//	public static void Validate(GDay value)
//	{
//	}
//}

//public readonly partial struct GDay3 : IDomainValue<GDay2>
//{
//	public static GDay2 Default => default;

//	public static void Validate(GDay2 value)
//	{
//	}
//}

///// <summary>
///// <para>Type: <see cref = "bool"/></para>
/////</summary>
//public readonly partial struct YesNoIndicator : IDomainValue<bool>
//{
//	/// <inheritdoc/>
//	public static void Validate(bool value)
//	{
//		//ValidationHelper.Validate(value);
//	}

//	/// <inheritdoc/>
//	public static bool Default => default;
//}

///// <inheritdoc/>
//public readonly partial struct X1 : IDomainValue<SByte>
//{
//	/// <inheritdoc/>
//	public static void Validate(SByte value)
//	{
//		if (value < 10 || value > 20)
//			throw new InvalidDomainValueException("Value must be between 10 and 20");
//	}

//	/// <inheritdoc/>
//	public static SByte Default => default;
//}

///// <inheritdoc/>
//public readonly partial struct X2 : IDomainValue<int>
//{
//	/// <inheritdoc/>
//	public static void Validate(int value)
//	{
//		if (value < 10)
//			throw new InvalidDomainValueException("Value must be between 10 and 20");
//		if (value > 20)
//			throw new InvalidDomainValueException("Value must be between 10 and 20");
//	}

//	/// <inheritdoc/>
//	public static int Default => 0;
//}

///// <inheritdoc/>
//public readonly partial struct X3 : IDomainValue<int>
//{
//	/// <inheritdoc/>
//	public static void Validate(int value)
//	{
//	}

//	/// <inheritdoc/>
//	public static int Default => 1 - 1;
//}

///// <inheritdoc/>
//public readonly partial struct X4 : IDomainValue<int>
//{
//	/// <inheritdoc/>
//	public static void Validate(int value)
//	{
//	}

//	/// <inheritdoc/>
//	public static int Default
//	{
//		get { return 0; }
//	}
//}

///// <inheritdoc/>
//public readonly partial struct X5 : IDomainValue<int>
//{
//	/// <inheritdoc/>
//	public static void Validate(int value)
//	{
//	}

//	/// <inheritdoc/>
//	public static int Default
//	{
//		get
//		{
//			return default(int);
//		}
//	}
//}