using AltaSoft.DomainPrimitives.Generator.Models;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace AltaSoft.DomainPrimitives.Generator.Tests;

public class DomainPrimitiveGeneratorTest
{
	[Fact]
	public Task StringValue_GeneratesAllInterfacesAndConverters()
	{
		const string source = """
		             using System;
		             using System.Collections.Generic;
		             using System.Linq;
		             using System.Text;
		             using System.Threading.Tasks;
		             using AltaSoft.DomainPrimitives.Abstractions;

		             namespace AltaSoft.DomainPrimitives;

		             /// <inheritdoc/>
		             public readonly partial struct StringValue : IDomainValue<string>
		             {
		             	/// <inheritdoc/>
		             	public static void Validate(string value)
		             	{
		             		if (value=="Test")
		             			throw new InvalidDomainValueException("Invalid Value");
		             	}

		             	/// <inheritdoc/>
		             	public static string Default => default;
		             }
		             """;

		return TestHelper.Verify(source, (_, x, _) => Assert.Equal(4, x.Count));
	}

	[Fact]
	public Task GuidValue_GeneratesAllInterfacesAndConverters()
	{
		const string source = """
		             using System;
		             using System.Collections.Generic;
		             using System.Linq;
		             using System.Text;
		             using System.Threading.Tasks;
		             using AltaSoft.DomainPrimitives.Abstractions;

		             namespace AltaSoft.DomainPrimitives;

		             /// <inheritdoc/>
		             public readonly partial struct GuidValue : IDomainValue<Guid>
		             {
		             	/// <inheritdoc/>
		             	public static void Validate(Guid value)
		             	{
		             		if (value==default)
		             			throw new InvalidDomainValueException("Invalid Value");
		             	}

		             	/// <inheritdoc/>
		             	public static Guid Default => default;
		             }
		             """;

		return TestHelper.Verify(source, (_, x, _) => Assert.Equal(4, x.Count));
	}

	[Fact]
	public Task BoolValue_GeneratesAllInterfacesAndConverters()
	{
		const string source = """
		             using System;
		             using System.Collections.Generic;
		             using System.Linq;
		             using System.Text;
		             using System.Threading.Tasks;
		             using AltaSoft.DomainPrimitives.Abstractions;

		             namespace AltaSoft.DomainPrimitives;

		             /// <inheritdoc/>
		             public readonly partial struct BoolValue : IDomainValue<bool>
		             {
		             	/// <inheritdoc/>
		             	public static void Validate(bool value)
		             	{
		             		if (!value)
		             			throw new InvalidDomainValueException("Invalid Value");
		             	}

		             	/// <inheritdoc/>
		             	public static bool Default => default;
		             }
		             """;

		return TestHelper.Verify(source, (_, x, _) => Assert.Equal(4, x.Count));
	}

	[Fact]
	public Task SByteValue_GeneratesAllInterfacesAndConverters()
	{
		const string source = """
		             using System;
		             using System.Collections.Generic;
		             using System.Linq;
		             using System.Text;
		             using System.Threading.Tasks;
		             using AltaSoft.DomainPrimitives.Abstractions;

		             namespace AltaSoft.DomainPrimitives;

		             /// <inheritdoc/>
		             public readonly partial struct SByteValue : IDomainValue<SByte>
		             {
		             	/// <inheritdoc/>
		             	public static void Validate(SByte value)
		             	{
		             		if (value < 10 || value > 20)
		             			throw new InvalidDomainValueException("Invalid Value");
		             	}

		             	/// <inheritdoc/>
		             	public static SByte Default => default;
		             }
		             """;

		return TestHelper.Verify(source, (_, x, _) => Assert.Equal(4, x.Count));
	}

	[Fact]
	public Task ByteValue_GeneratesAllInterfacesAndConverters()
	{
		const string source = """
		             using System;
		             using System.Collections.Generic;
		             using System.Linq;
		             using System.Text;
		             using System.Threading.Tasks;
		             using AltaSoft.DomainPrimitives.Abstractions;

		             namespace AltaSoft.DomainPrimitives;

		             /// <inheritdoc/>
		             public readonly partial struct ByteValue : IDomainValue<byte>
		             {
		             	/// <inheritdoc/>
		             	public static void Validate(byte value)
		             	{
		             		if (value < 0)
		             			throw new InvalidDomainValueException("Invalid Value");
		             	}

		             	/// <inheritdoc/>
		             	public static byte Default => default;
		             }
		             """;

		return TestHelper.Verify(source, (_, x, _) => Assert.Equal(4, x.Count));
	}

	[Fact]
	public Task Int16Value_GeneratesAllInterfacesAndConverters()
	{
		const string source = """
		             using System;
		             using System.Collections.Generic;
		             using System.Linq;
		             using System.Text;
		             using System.Threading.Tasks;
		             using AltaSoft.DomainPrimitives.Abstractions;

		             namespace AltaSoft.DomainPrimitives;

		             /// <inheritdoc/>
		             public readonly partial struct ShortValue : IDomainValue<short>
		             {
		             	/// <inheritdoc/>
		             	public static void Validate(short value)
		             	{
		             		if (value < 0)
		             			throw new InvalidDomainValueException("Invalid Value");
		             	}

		             	/// <inheritdoc/>
		             	public static short Default => default;
		             }
		             """;

		return TestHelper.Verify(source, (_, x, _) => Assert.Equal(4, x.Count));
	}

	[Fact]
	public Task UInt16Value_GeneratesAllInterfacesAndConverters()
	{
		const string source = """
		             using System;
		             using System.Collections.Generic;
		             using System.Linq;
		             using System.Text;
		             using System.Threading.Tasks;
		             using AltaSoft.DomainPrimitives.Abstractions;

		             namespace AltaSoft.DomainPrimitives;

		             /// <inheritdoc/>
		             public readonly partial struct UShortValue : IDomainValue<ushort>
		             {
		             	/// <inheritdoc/>
		             	public static void Validate(ushort value)
		             	{
		             		if (value > 100)
		             			throw new InvalidDomainValueException("Value must be between 10 and 100");
		             	}

		             	/// <inheritdoc/>
		             	public static ushort Default => default;
		             }
		             """;

		return TestHelper.Verify(source, (_, x, _) => Assert.Equal(4, x.Count));
	}

	[Fact]
	public Task IntValue_GeneratesAllInterfacesAndConverters()
	{
		const string source = """
		             using System;
		             using System.Collections.Generic;
		             using System.Linq;
		             using System.Text;
		             using System.Threading.Tasks;
		             using AltaSoft.DomainPrimitives.Abstractions;

		             namespace AltaSoft.DomainPrimitives;

		             /// <inheritdoc/>
		             public readonly partial struct IntValue : IDomainValue<int>
		             {
		             	/// <inheritdoc/>
		             	public static void Validate(int value)
		             	{
		             		if (value < 10 || value > 20)
		             			throw new InvalidDomainValueException("Invalid Value");
		             	}

		             	/// <inheritdoc/>
		             	public static int Default => default;
		             }
		             """;

		return TestHelper.Verify(source, (_, x, _) => Assert.Equal(4, x.Count));
	}

	[Fact]
	public Task UIntValue_GeneratesAllInterfacesAndConverters()
	{
		const string source = """
		             using System;
		             using System.Collections.Generic;
		             using System.Linq;
		             using System.Text;
		             using System.Threading.Tasks;
		             using AltaSoft.DomainPrimitives.Abstractions;

		             namespace AltaSoft.DomainPrimitives;

		             /// <inheritdoc/>
		             public readonly partial struct IntValue : IDomainValue<uint>
		             {
		             	/// <inheritdoc/>
		             	public static void Validate(uint value)
		             	{
		             		if (value < 10 || value > 20)
		             			throw new InvalidDomainValueException("Invalid Value");
		             	}

		             	/// <inheritdoc/>
		             	public static uint Default => default;
		             }
		             """;

		return TestHelper.Verify(source, (_, x, _) => Assert.Equal(4, x.Count));
	}

	[Fact]
	public Task LongValue_GeneratesAllInterfacesAndConverters()
	{
		const string source = """
		             using System;
		             using System.Collections.Generic;
		             using System.Linq;
		             using System.Text;
		             using System.Threading.Tasks;
		             using AltaSoft.DomainPrimitives.Abstractions;

		             namespace AltaSoft.DomainPrimitives;

		             /// <inheritdoc/>
		             public readonly partial struct LongValue : IDomainValue<long>
		             {
		             	/// <inheritdoc/>
		             	public static void Validate(long value)
		             	{
		             		if (value < 0)
		             			throw new InvalidDomainValueException("Invalid Value");
		             	}

		             	/// <inheritdoc/>
		             	public static long Default => default;
		             }
		             """;

		return TestHelper.Verify(source, (_, x, _) => Assert.Equal(4, x.Count));
	}

	[Fact]
	public Task ULongValue_GeneratesAllInterfacesAndConverters()
	{
		const string source = """
		             using System;
		             using System.Collections.Generic;
		             using System.Linq;
		             using System.Text;
		             using System.Threading.Tasks;
		             using AltaSoft.DomainPrimitives.Abstractions;

		             namespace AltaSoft.DomainPrimitives;

		             /// <inheritdoc/>
		             public readonly partial struct LongValue : IDomainValue<ulong>
		             {
		             	/// <inheritdoc/>
		             	public static void Validate(ulong value)
		             	{
		             		if (value < 0)
		             			throw new InvalidDomainValueException("Invalid Value");
		             	}

		             	/// <inheritdoc/>
		             	public static ulong Default => default;
		             }
		             """;

		return TestHelper.Verify(source, (_, x, _) => Assert.Equal(4, x.Count));
	}

	[Fact]
	public Task DecimalValue_GeneratesAllInterfacesAndConverters()
	{
		const string source = """
		             using System;
		             using System.Collections.Generic;
		             using System.Linq;
		             using System.Text;
		             using System.Threading.Tasks;
		             using AltaSoft.DomainPrimitives.Abstractions;

		             namespace AltaSoft.DomainPrimitives;

		             public readonly partial struct DecimalValue : IDomainValue<decimal>
		             {
		             	/// <inheritdoc/>
		             	public static void Validate(decimal value)
		             	{
		             		if (value < 0)
		             			throw new InvalidDomainValueException("Invalid Value");
		             	}

		             	/// <inheritdoc/>
		             	public static decimal Default => default;
		             }
		             """;

		return TestHelper.Verify(source, (_, x, _) => Assert.Equal(4, x.Count));
	}

	[Fact]
	public Task FloatValue_GeneratesAllInterfacesAndConverters()
	{
		const string source = """
		             using System;
		             using System.Collections.Generic;
		             using System.Linq;
		             using System.Text;
		             using System.Threading.Tasks;
		             using AltaSoft.DomainPrimitives.Abstractions;

		             namespace AltaSoft.DomainPrimitives;

		             public readonly partial struct FloatValue : IDomainValue<float>
		             {
		             	/// <inheritdoc/>
		             	public static void Validate(float value)
		             	{
		             		if (value < 0)
		             			throw new InvalidDomainValueException("Invalid Value");
		             	}

		             	/// <inheritdoc/>
		             	public static float Default => default;
		             }
		             """;

		return TestHelper.Verify(source, (_, x, _) => Assert.Equal(4, x.Count));
	}

	[Fact]
	public Task DoubleValue_GeneratesAllInterfacesAndConverters()
	{
		const string source = """
		             using System;
		             using System.Collections.Generic;
		             using System.Linq;
		             using System.Text;
		             using System.Threading.Tasks;
		             using AltaSoft.DomainPrimitives.Abstractions;

		             namespace AltaSoft.DomainPrimitives;

		             public readonly partial struct DoubleValue : IDomainValue<double>
		             {
		             	/// <inheritdoc/>
		             	public static void Validate(double value)
		             	{
		             		if (value < 0)
		             			throw new InvalidDomainValueException("Invalid Value");
		             	}

		             	/// <inheritdoc/>
		             	public static double Default => default;
		             }
		             """;

		return TestHelper.Verify(source, (_, x, _) => Assert.Equal(4, x.Count));
	}

	[Fact]
	public Task DateTimeValue_GeneratesAllInterfacesAndConverters()
	{
		const string source = """
		             using System;
		             using System.Collections.Generic;
		             using System.Linq;
		             using System.Text;
		             using System.Threading.Tasks;
		             using AltaSoft.DomainPrimitives.Abstractions;

		             namespace AltaSoft.DomainPrimitives;

		             public readonly partial struct DateTimeValue : IDomainValue<DateTime>
		             {
		             	/// <inheritdoc/>
		             	public static void Validate(DateTime value)
		             	{
		             		if (value == default)
		             			throw new InvalidDomainValueException("Invalid Value");
		             	}

		             	/// <inheritdoc/>
		             	public static DateTime Default => default;
		             }

		             """;

		return TestHelper.Verify(source, (_, x, _) => Assert.Equal(4, x.Count));
	}

	[Fact]
	public Task DateOnlyValue_GeneratesAllInterfacesAndConverters()
	{
		const string source = """
		             using System;
		             using System.Collections.Generic;
		             using System.Linq;
		             using System.Text;
		             using System.Threading.Tasks;
		             using AltaSoft.DomainPrimitives.Abstractions;

		             namespace AltaSoft.DomainPrimitives;

		             public readonly partial struct DateOnlyValue : IDomainValue<DateOnly>
		             {
		             	/// <inheritdoc/>
		             	public static void Validate(DateOnly value)
		             	{
		             		if (value == default)
		             			throw new InvalidDomainValueException("Invalid Value");
		             	}

		             	/// <inheritdoc/>
		             	public static DateOnly Default => default;
		             }

		             """;

		return TestHelper.Verify(source, (_, x, _) => Assert.Equal(4, x.Count));
	}

	[Fact]
	public Task TimeOnlyValue_GeneratesAllInterfacesAndConverters()
	{
		const string source = """
		             using System;
		             using System.Collections.Generic;
		             using System.Linq;
		             using System.Text;
		             using System.Threading.Tasks;
		             using AltaSoft.DomainPrimitives.Abstractions;

		             namespace AltaSoft.DomainPrimitives;

		             public readonly partial struct TimeOnlyValue : IDomainValue<TimeOnly>
		             {
		             	/// <inheritdoc/>
		             	public static void Validate(TimeOnly value)
		             	{
		             		if (value == default)
		             			throw new InvalidDomainValueException("Invalid Value");
		             	}

		             	/// <inheritdoc/>
		             	public static TimeOnly Default => default;
		             }

		             """;

		return TestHelper.Verify(source, (_, x, _) => Assert.Equal(4, x.Count));
	}

	[Fact]
	public Task TimeSpanValue_GeneratesAllInterfacesAndConverters()
	{
		const string source = """
		             using System;
		             using System.Collections.Generic;
		             using System.Linq;
		             using System.Text;
		             using System.Threading.Tasks;
		             using AltaSoft.DomainPrimitives.Abstractions;

		             namespace AltaSoft.DomainPrimitives;

		             public readonly partial struct TimeSpanValue : IDomainValue<TimeSpan>
		             {
		             	/// <inheritdoc/>
		             	public static void Validate(TimeSpan value)
		             	{
		             		if (value == default)
		             			throw new InvalidDomainValueException("Invalid Value");
		             	}

		             	/// <inheritdoc/>
		             	public static TimeSpan Default => default;
		             }

		             """;

		return TestHelper.Verify(source, (_, x, _) => Assert.Equal(4, x.Count));
	}

	[Fact]
	public Task DateTimeOffsetValue_GeneratesAllInterfacesAndConverters()
	{
		const string source = """
		             using System;
		             using System.Collections.Generic;
		             using System.Linq;
		             using System.Text;
		             using System.Threading.Tasks;
		             using AltaSoft.DomainPrimitives.Abstractions;

		             namespace AltaSoft.DomainPrimitives;

		             public readonly partial struct DateTimeOffsetValue : IDomainValue<DateTimeOffset>
		             {
		             	/// <inheritdoc/>
		             	public static void Validate(DateTimeOffset value)
		             	{
		             		if (value == default)
		             			throw new InvalidDomainValueException("Invalid Value");
		             	}

		             	/// <inheritdoc/>
		             	public static DateTimeOffset Default => default;
		             }

		             """;

		return TestHelper.Verify(source, (_, x, _) => Assert.Equal(4, x.Count));
	}

	[Fact]
	public Task CharValue_GeneratesAllInterfacesAndConverters()
	{
		const string source = """
		                      using System;
		                      using System.Collections.Generic;
		                      using System.Linq;
		                      using System.Text;
		                      using System.Threading.Tasks;
		                      using AltaSoft.DomainPrimitives.Abstractions;

		                      namespace AltaSoft.DomainPrimitives;

		                      public readonly partial struct CharValue : IDomainValue<char>
		                      {
		                      	/// <inheritdoc/>
		                      	public static void Validate(char value)
		                      	{
		                      		if (value == default)
		                      			throw new InvalidDomainValueException("Invalid Value");
		                      	}

		                      	/// <inheritdoc/>
		                      	public static char Default => default;
		                      }

		                      """;

		return TestHelper.Verify(source, (_, x, _) => Assert.Equal(4, x.Count));
	}

	public static class TestHelper
	{
		internal static Task Verify(string source, Action<ImmutableArray<Diagnostic>, List<string>, GeneratorDriver>? additionalChecks = null, DomainPrimitiveGlobalOptions? options = null)
		{
			var (diagnostics, output, driver) = TestHelpers.GetGeneratedOutput<DomainPrimitiveGenerator>(source, options);

			additionalChecks?.Invoke(diagnostics, output, driver);
			return Verifier.Verify(driver).UseDirectory("Snapshots");
		}
	}
}